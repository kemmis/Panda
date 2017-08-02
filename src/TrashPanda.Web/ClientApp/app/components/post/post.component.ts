import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { Subscription } from "rxjs/Subscription";
import { PostService } from "../../services/post.service";
import { Post } from "../../models/post";

@Component({
    selector: 'post',
    templateUrl: './post.component.html'
})
export class PostComponent implements OnInit, OnDestroy {
    private paramSub: Subscription;
    post: Post;

    constructor(private route: ActivatedRoute, private _postService: PostService) {

    }

    ngOnInit(): void {
        this.paramSub = this.route.params.subscribe(params => {
            var slug = params['slug'];
            this._postService.getPostBySlug(slug).subscribe(post => {
                this.post = post;
            });
        });
    }

    ngOnDestroy(): void {
        this.paramSub.unsubscribe();
    }
}
