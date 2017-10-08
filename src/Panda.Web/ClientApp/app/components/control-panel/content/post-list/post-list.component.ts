
import { Component, Input, ViewChild, EventEmitter, Output } from "@angular/core";
import { BlogPostContent } from "../../../../models/blog-content";
import { DataSource } from "@angular/cdk/table";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { MatPaginator, MatSnackBar } from "@angular/material";
import { Observable } from "rxjs/Observable";
import { ContentService } from "../../../../services/content.service";
import { PostDeletedComponent } from "./post-deleted.component";

@Component({
    selector: 'post-content-list',
    templateUrl: './post-list.component.html',
    styleUrls: ['./post-list.component.less']
})
export class PostContentListComponent {
    constructor(private _contentService: ContentService,
        private _snackBar: MatSnackBar) { }

    @Input() set posts(posts: BlogPostContent[]) {
        this.postsArray = posts;
        this.dataChange.next(this.postsArray);
    }
    @ViewChild("paginator") paginator: MatPaginator;

    @Output() editPost = new EventEmitter<string>();

    displayedColumns = ['title', 'published', 'edit', 'delete'];

    postsArray: BlogPostContent[] = [];

    dataChange: BehaviorSubject<BlogPostContent[]> = new BehaviorSubject<BlogPostContent[]>([]);

    get totalNum() {
        if (this.postsArray)
            return this.postsArray.length;
        return 0;
    }

    connect() {
        const displayDataChanges = [
            this.dataChange,
            this.paginator.page,
        ];
        return Observable.merge(...displayDataChanges).map(() => {
            if (!this.postsArray) return [];
            const data = this.postsArray.slice();

            // Grab the page's slice of data.
            const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
            return data.splice(startIndex, this.paginator.pageSize);
        });
    }

    getThis() {
        return this;
    }
    disconnect() { }

    delete(post: BlogPostContent) {
        this._contentService.deletePost(post.id).subscribe(() => {
            var postIndex = this.postsArray.indexOf(post)
            this.postsArray.splice(postIndex, 1);
            this.dataChange.next(this.postsArray);
            let snackbar = this._snackBar.openFromComponent(PostDeletedComponent, { data: post });
            snackbar.instance.postRestored.subscribe((post: BlogPostContent) => {
                this.postsArray.splice(postIndex, 0, post);
                this.dataChange.next(this.postsArray);
                this._snackBar.open("Post Restored Successfully!", "", { duration: 5000 });
            });
            snackbar.instance.dismissed.subscribe(() => {
                this._snackBar.dismiss();
            });
        });
    }

    edit(post: BlogPostContent) {
        this.editPost.emit(post.id);
    }
}