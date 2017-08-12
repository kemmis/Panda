import { Component, OnInit, OnDestroy } from '@angular/core';
import { PostService } from "../../../services/post.service";
import { PostList } from "../../../models/post-list";
import { PostListRequest } from "../../../models/post-list-request";
import { ActivatedRoute } from "@angular/router";
import { Subscription } from "rxjs/Subscription";
import { Title } from "@angular/platform-browser";

@Component({
    selector: 'category-page',
    templateUrl: './category.component.html'
})
export class CategoryPageComponent implements OnInit, OnDestroy {

    constructor(private route: ActivatedRoute, private _postService: PostService, private _titleService: Title) {
    }

    private paramSub: Subscription;
    list: PostList = new PostList();
    slug: string;
    get navPath(): string {
        return "/category/" + this.slug + "/";
    }
    ngOnInit(): void {
        this.paramSub = this.route.params.subscribe(params => {
            this.slug = params['slug'];
            var index = params['index'];
            let request: PostListRequest = {
                pageIndex: index || 0,
                categorySlug: this.slug
            };
            this._postService.getCategoryListBySlug(request).subscribe((list: PostList) => {
                this.list = list;
                this._titleService.setTitle(list.pageTitle);
            });
        });
    }

    ngOnDestroy(): void {
        this.paramSub.unsubscribe();
    }
}
