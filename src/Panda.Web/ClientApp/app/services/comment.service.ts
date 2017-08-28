import { Injectable, Inject } from "@angular/core";
import { Http, Headers, RequestOptions, URLSearchParams } from "@angular/http";
import { CommentSaveRequest } from "../models/comment-save-request";
import { Observable } from "rxjs/Observable";
import { PostComment } from "../models/post-comment";

@Injectable()
export class CommentService {
    constructor(private _http: Http, @Inject('BASE_URL') private originUrl: string) {
    }

    saveComment(request: CommentSaveRequest): Observable<PostComment> {
        const body = JSON.stringify(request);
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });

        return this._http
            .post(`${this.originUrl}api/comment/save/`, body, options)
            .map(res => res.json());
    }

    deleteComment(commentId: string): Observable<PostComment> {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");

        const params = new URLSearchParams()
        params.set("commentId", commentId);

        const getOpts = new RequestOptions({
            headers: headers,
            search: params
        });

        return this._http.get(`${this.originUrl}api/comment/delete/`, getOpts).map(res => {
            return res.json();
        });
    }

    unDeleteComment(commentId: string): Observable<PostComment> {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");

        const params = new URLSearchParams()
        params.set("commentId", commentId);

        const getOpts = new RequestOptions({
            headers: headers,
            search: params
        });

        return this._http.get(`${this.originUrl}api/comment/undelete/`, getOpts).map(res => {
            return res.json();
        });
    }
}