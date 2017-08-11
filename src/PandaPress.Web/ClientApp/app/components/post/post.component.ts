
import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Post } from "../../models/post";
import { PostComment } from "../../models/post-comment";

@Component({
    selector: 'post',
    templateUrl: './post.component.html'
})
export class PostComponent {
    @Input() post: Post;
    @Input() showComments: boolean = false;
    onCommentCreated(comment: PostComment) {
        this.post.comments.push(comment);
    }
}   