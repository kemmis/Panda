import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { Subscription } from "rxjs/Subscription";
import { Post } from "../../../models/post";
import { PostService } from "../../../services/post.service";
import { Title } from "@angular/platform-browser";

@Component({
    selector: 'post-page',
    templateUrl: './post.component.html'
})
export class PostPageComponent implements OnInit, OnDestroy {
    private paramSub: Subscription;
    post: Post = new Post();

    constructor(private route: ActivatedRoute, private _postService: PostService, private _titleService: Title) {

    }

    ngOnInit(): void {
        this.paramSub = this.route.params.subscribe(params => {
            var slug = params['slug'];
            this._postService.getPostBySlug(slug).subscribe(post => {
                this.post = post;
                this._titleService.setTitle(post.title);
            });
        });
    }

    ngOnDestroy(): void {
        this.paramSub.unsubscribe();
    }
}