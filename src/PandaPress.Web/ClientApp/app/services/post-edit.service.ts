import { Injectable, Inject } from "@angular/core";
import { Http, Headers, URLSearchParams, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { EditPost } from "../models/edit-post";

@Injectable()
export class PostEditService {
    constructor(private _http: Http, @Inject('BASE_URL') private originUrl: string) {
    }

    getPostById(postId: string): Observable<EditPost> {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");

        const params = new URLSearchParams()
        params.set("postId", postId);

        const getOpts = new RequestOptions({
            headers: headers,
            search: params
        });

        return this._http.get(`${this.originUrl}api/posteditor/getbyid/`, getOpts).map(res => {
            return res.json();
        });
    }

    savePost(post:EditPost):Observable<EditPost>{
        const body = JSON.stringify(post);
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });

        return this._http
            .post(`${this.originUrl}api/posteditor/save/`, body, options)
            .map(res => res.json());
    }
}