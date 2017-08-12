import { Component, OnInit, OnDestroy } from '@angular/core';
import { PostService } from "../../../services/post.service";
import { PostList } from "../../../models/post-list";
import { PostListRequest } from "../../../models/post-list-request";
import { ActivatedRoute } from "@angular/router";
import { Subscription } from "rxjs/Subscription";
import { Title } from "@angular/platform-browser";

@Component({
    selector: 'home-page',
    templateUrl: './home.component.html'
})
export class HomePageComponent implements OnInit {

    constructor(private _postService: PostService, private route: ActivatedRoute, private _titleService: Title) {

    }
    private paramSub: Subscription;
    list: PostList = new PostList();

    ngOnInit(): void {
        this.paramSub = this.route.params.subscribe(params => {
            var index = params['index'];
            let request: PostListRequest = {
                pageIndex: index || 0,
                categorySlug: ""
            };

            this._postService.getList(request).subscribe(response => {
                this.list = response;
                this._titleService.setTitle(response.pageTitle);
            });
        });
    }
}
