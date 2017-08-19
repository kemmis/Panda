import { Component, OnInit, OnDestroy, Input, EventEmitter, Output, ViewChild } from '@angular/core';
import { CommentService } from "../../../services/comment.service";
import { CommentSaveRequest } from "../../../models/comment-save-request";
import { PostComment } from "../../../models/post-comment";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

@Component({
    selector: 'comment-form',
    templateUrl: './comment-form.component.html'
})
export class CommentFormComponent implements OnInit {

    constructor(private _commentService: CommentService,
        private _formBuilder: FormBuilder) { }
    form: FormGroup;
    @Input() postId: string;
    @Output() commentCreated = new EventEmitter<PostComment>();
    saving: boolean = false;

    ngOnInit(): void {
        var authorName: any = null;
        var authorEmail: any = null;
        if (typeof localStorage !== 'undefined') {
            authorName = localStorage ? localStorage.getItem("comment-authorName") : "";
            authorEmail = localStorage ? localStorage.getItem("comment-authorEmail") : "";
        }
        this.form = this._formBuilder.group({
            authorName: [authorName, Validators.required],
            authorEmail: [authorEmail, Validators.required],
            text: ['', Validators.required]
        });
    }

    save() {
        var newComment = this.form.value;
        newComment.postId = this.postId;
        this.saving = true;
        if (localStorage) {
            localStorage.setItem("comment-authorName", newComment.authorName);
            localStorage.setItem("comment-authorEmail", newComment.authorEmail);
        }
        this._commentService.saveComment(newComment).subscribe((comment: PostComment) => {
            this.saving = false;
            this.form.patchValue({ text: "" });
            this.commentCreated.emit(comment);
        });
    }
}
