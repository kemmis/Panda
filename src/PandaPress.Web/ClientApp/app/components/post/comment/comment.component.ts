import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { PostComment } from "../../../models/post-comment";

@Component({
    selector: 'comment',
    templateUrl: './comment.component.html'
})
export class CommentComponent {
    @Input() comment: PostComment;    
}