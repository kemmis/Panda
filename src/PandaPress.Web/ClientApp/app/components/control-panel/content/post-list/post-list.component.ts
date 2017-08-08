
import { Component, Input } from "@angular/core";
import { BlogPostContent } from "../../../../models/blog-content";
import { DataSource } from "@angular/cdk/table";
import { BehaviorSubject } from "rxjs/BehaviorSubject";

@Component({
    selector: 'post-content-list',
    templateUrl: './post-list.component.html',
    styleUrls: ['./post-list.component.less']
})
export class PostContentListComponent {
    @Input() set posts(posts: BlogPostContent[]) {
        this.dataChange.next(posts);
    }

    displayedColumns = ['id', 'title', 'published'];

    dataChange: BehaviorSubject<BlogPostContent[]> = new BehaviorSubject<BlogPostContent[]>([]);

    connect() {
        return this.dataChange;
    }
    
    getThis(){
        return this;
    }
    disconnect() { }
}