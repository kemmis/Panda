
import { Component, OnInit, OnDestroy, Input, AfterViewChecked } from '@angular/core';
import { Post } from "../../models/post";
import { PostComment } from "../../models/post-comment";
import { ActivatedRoute } from "@angular/router";
import { Subscription } from "rxjs/Subscription";

@Component({
    selector: 'post',
    templateUrl: './post.component.html'
})
export class PostComponent implements AfterViewChecked {

    constructor(private activatedRoute: ActivatedRoute) { }
    @Input() post: Post;
    @Input() showComments: boolean = false;
    private scrollExecuted: boolean = false;
    ngAfterViewChecked(): void {
        if (!this.scrollExecuted && this.showComments && this.post.id) {
            let routeFragmentSubscription: Subscription;

            // Automatic scroll
            routeFragmentSubscription =
                this.activatedRoute.fragment
                    .subscribe(fragment => {
                        if (fragment) {
                            let element = document.getElementById(fragment);
                            if (element) {
                                element.scrollIntoView();
                                this.scrollExecuted = true;

                                // Free resources
                                setTimeout(
                                    () => {
                                        routeFragmentSubscription.unsubscribe();
                                    }, 1000);
                            }
                        }
                    });
        }
    }

    onCommentCreated(comment: PostComment) {
        this.post.comments.push(comment);
    }
}       