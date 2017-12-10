import { Component, OnInit, EventEmitter, Output, AfterViewInit, Inject, ViewChild, HostListener, NgZone } from "@angular/core";
import { CategoryService } from "../../../services/category.service";
import { BlogCategoryContent } from "../../../models/blog-content";
import * as _ from 'underscore';
import { MAT_DIALOG_DATA, MatSnackBar } from "@angular/material";
import { TinyMceComponent } from "./tiny-mce-component";
import { PostEditService } from "../../../services/post-edit.service";
import { EditPost } from "../../../models/edit-post";

@Component({
    selector: 'post-editor',
    templateUrl: './post-editor.component.html',
    styleUrls: ['./post-editor.component.less'],
    providers: [CategoryService, PostEditService]
})
export class PostEditorComponent implements AfterViewInit {
    constructor(private _categoryService: CategoryService,
        @Inject(MAT_DIALOG_DATA) public data: any,
        private _postEditService: PostEditService,
        private _snackBar: MatSnackBar, private _zone: NgZone) {
    }
    @Output() navigate = new EventEmitter<string>();

    @ViewChild("tmce") tmce: TinyMceComponent;

    post: EditPost = new EditPost();
    saving: boolean = false;
    allCategories: string[] = [];

    ngAfterViewInit(): void {
        this._categoryService.getCategories().subscribe((categories: BlogCategoryContent[]) => {
            this.allCategories = _.map(categories, (c: BlogCategoryContent) => { return c.title; });
        });

        if (this.data.postId != "0") {
            this.saving = true;
            this._postEditService.getPostById(this.data.postId).subscribe((post: EditPost) => {
                this.saving = false;
                this.post = post;
                this.tmce.content = this.post.content;
            });
        }
        else {
            this.post = new EditPost();
            //this.tmce.content = "";
        }
    }

    save(successMessage: string) {
        this._zone.run(() => {
            this.saving = true;
            this.post.content = this.tmce.content;
            this._postEditService.savePost(this.post).subscribe((post: EditPost) => {
                this.saving = false;
                this.post = post;
                this.tmce.content = this.post.content;
                this._snackBar.open(successMessage, "", { duration: 3000 });
            });
        });

    }

    publish() {
        this.post.published = true;
        this.save("Post Published!");
    }
    unpublish() {
        this.post.published = false;
        this.save("Post Unpublished!");
    }

    goToPost() {
        this.navigate.emit(this.post.slug);
    }

    @HostListener('document:keydown', ['$event'])
    handleKeyboardEvent(event: any) {
        if (event.ctrlKey) {
            let charCode = String.fromCharCode(event.which).toLowerCase();
            if (charCode === 's') {
                event.preventDefault();
                this.save("Post Saved!");
                return false;
            }
        }
    }
}
