import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { PostComment } from "../../../models/post-comment";
import { UserInfoService } from "../../../services/user-info.service";
import { CommentService } from "../../../services/comment.service";

@Component({
    selector: 'comment',
    templateUrl: './comment.component.html'
})
export class CommentComponent {
    constructor(public _userInfoService: UserInfoService, private _commentService: CommentService) { }
    @Input() comment: PostComment;

    delete() {
        this._commentService.deleteComment(this.comment.id).subscribe((comment: PostComment) => {
            this.comment = comment;
            // show snackbar
            // mark comment visually as deleted
        });
    }

    unDelete() {
        this._commentService.unDeleteComment(this.comment.id).subscribe((comment: PostComment) => {
            this.comment = comment;
            // show snackbar
        });
    }
}