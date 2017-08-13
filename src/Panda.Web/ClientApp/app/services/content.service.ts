import { Injectable, Inject } from "@angular/core";
import { Http, RequestOptions, Headers, URLSearchParams } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { BlogContent } from "../models/blog-content";

@Injectable()
export class ContentService {
    constructor(private _http: Http, @Inject('BASE_URL') private originUrl: string) {
    }
    getContent(): Observable<BlogContent> {
        return this._http.get(`${this.originUrl}api/content/getcontent/`).map(res => {
            return res.json();
        });
    }

    deletePost(postId: string): Observable<void> {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");

        const params = new URLSearchParams()
        params.set("postId", postId);

        const getOpts = new RequestOptions({
            headers: headers,
            search: params
        });

        return this._http.get(`${this.originUrl}api/content/deletepost/`, getOpts).map(res => {
            return;
        });
    }

    unDeletePost(postId: string): Observable<void> {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");

        const params = new URLSearchParams()
        params.set("postId", postId);

        const getOpts = new RequestOptions({
            headers: headers,
            search: params
        });

        return this._http.get(`${this.originUrl}api/content/undeletepost/`, getOpts).map(res => {
            return;
        });
    }

}