
import { Component, Input, ViewChild } from "@angular/core";
import { BlogPostContent } from "../../../../models/blog-content";
import { DataSource } from "@angular/cdk/table";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { MdPaginator } from "@angular/material";
import { Observable } from "rxjs/Observable";

@Component({
    selector: 'post-content-list',
    templateUrl: './post-list.component.html',
    styleUrls: ['./post-list.component.less']
})
export class PostContentListComponent {
    @Input() set posts(posts: BlogPostContent[]) {
        this.postsArray = posts;
        this.dataChange.next(this.postsArray);
    }
    @ViewChild("paginator") paginator: MdPaginator;

    displayedColumns = ['id', 'title', 'published'];

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
}