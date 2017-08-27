import { Component, OnInit, OnDestroy, Input, EventEmitter, Output, ViewChild, NgZone } from '@angular/core';
import { CommentService } from "../../../services/comment.service";
import { CommentSaveRequest } from "../../../models/comment-save-request";
import { PostComment } from "../../../models/post-comment";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

declare var grecaptcha: any;

@Component({
    selector: 'comment-form',
    templateUrl: './comment-form.component.html'
})
export class CommentFormComponent implements OnInit {

    constructor(private _commentService: CommentService,
        private _formBuilder: FormBuilder, private _zone: NgZone) {
    }

    form: FormGroup;
    @Input() postId: string;

    _userReCaptcha: boolean;
    @Input() set useReCaptcha(value: boolean) {
        if (typeof window !== 'undefined') {
            this._userReCaptcha = value;
            if (value) {
                this.displayRecaptcha();
            }
        }
    }

    @Input() reCaptchaKey: string;
    @Output() commentCreated = new EventEmitter<PostComment>();
    saving: boolean = false;
    recaptchaToken: string = "";
    recaptchaCompleted: boolean = false;



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
        newComment.reCaptchaToken = this.recaptchaToken;
        this.saving = true;
        if (localStorage) {
            localStorage.setItem("comment-authorName", newComment.authorName);
            localStorage.setItem("comment-authorEmail", newComment.authorEmail);
        }
        this._commentService.saveComment(newComment).subscribe((comment: PostComment) => {
            this.saving = false;
            this.form.patchValue({ text: "" });
            if (this._userReCaptcha) {
                grecaptcha.reset();
                this._zone.run(() => {
                    this.recaptchaCompleted = false;
                    this.recaptchaToken = "";
                });
            }
            this.commentCreated.emit(comment);
        });
    }

    displayRecaptcha() {
        (<any>window).verifyCallback = this.verifyCallback.bind(this);
        var doc = <HTMLDivElement>document.getElementById('comment-form');
        var script = document.createElement('script');
        script.innerHTML = '';
        script.src = 'https://www.google.com/recaptcha/api.js';
        script.async = true;
        script.defer = true;
        doc.appendChild(script);
    }

    verifyCallback(response: any) {
        this._zone.run(() => {
            this.recaptchaCompleted = true;
            this.recaptchaToken = response;
        });
    }
}
