import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { PostComment } from "../../../models/post-comment";

@Component({
    selector: 'comment-list',
    templateUrl: './comment-list.component.html',
    styleUrls: ['./comment-list.component.less']
})
export class CommentListComponent {
    @Input() comments: PostComment[];
}
