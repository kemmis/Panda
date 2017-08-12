import { Component, OnInit, EventEmitter, Output, AfterViewInit, Inject, ViewChild } from "@angular/core";
import { CategoryService } from "../../../services/category.service";
import { BlogCategoryContent } from "../../../models/blog-content";
import * as _ from 'underscore';
import { MD_DIALOG_DATA, MdSnackBar } from "@angular/material";
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
        @Inject(MD_DIALOG_DATA) public data: any,
        private _postEditService: PostEditService,
        private _snackBar: MdSnackBar) {
    }
    @Output() navigate = new EventEmitter<string>();

    @ViewChild("tmce") tmce: TinyMceComponent;

    post: EditPost = new EditPost();

    allCategories: string[] = [];

    ngAfterViewInit(): void {
        this._categoryService.getCategories().subscribe((categories: BlogCategoryContent[]) => {
            this.allCategories = _.map(categories, (c) => { return c.title; });
        });

        if (this.data.postId > 0) {
            this._postEditService.getPostById("1").subscribe((post: EditPost) => {
                this.post = post;
                this.tmce.content = this.post.content;
            });
        }
        else {
            this.post = new EditPost();
            this.tmce.content = "";
        }
    }

    save(successMessage: string) {
        this.post.content = this.tmce.content;
        this._postEditService.savePost(this.post).subscribe((post: EditPost) => {
            this.post = post;
            this.tmce.content = this.post.content;
            this._snackBar.open(successMessage, "", { duration: 3000 });
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
}
