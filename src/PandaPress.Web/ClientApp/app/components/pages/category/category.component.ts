import { Component, OnInit, OnDestroy } from '@angular/core';
import { PostService } from "../../../services/post.service";
import { PostList } from "../../../models/post-list";
import { PostListRequest } from "../../../models/post-list-request";
import { ActivatedRoute } from "@angular/router";
import { Subscription } from "rxjs/Subscription";

@Component({
    selector: 'category-page',
    templateUrl: './category.component.html'
})
export class CategoryPageComponent implements OnInit, OnDestroy {

    constructor(private route: ActivatedRoute, private _postService: PostService) {
    }

    private paramSub: Subscription;
    list: PostList = new PostList();

    ngOnInit(): void {
        this.paramSub = this.route.params.subscribe(params => {
            var slug = params['slug'];
            let request: PostListRequest = {
                pageIndex: 0,
                pageSize: 5,
                categorySlug: slug
            };
            this._postService.getCategoryListBySlug(request).subscribe((list: PostList) => {
                this.list = list;
            });
        });
    }

    ngOnDestroy(): void {
        this.paramSub.unsubscribe();
    }
}
