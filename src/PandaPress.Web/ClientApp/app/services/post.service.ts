import { Injectable, Inject } from "@angular/core";
import { Http, RequestOptions, Headers, URLSearchParams } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { Post } from "../models/post";
import 'rxjs/add/operator/map';
import { PostListRequest } from "../models/post-list-request";
import { PostList } from "../models/post-list";
import { BlogSettings } from "../models/blog-settings";
@Injectable()
export class PostService {

    constructor(private _http: Http, @Inject('BASE_URL') private originUrl: string) {
    }

    getPostBySlug(slug: string): Observable<Post> {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");

        const params = new URLSearchParams()
        params.set("slug", slug);

        const getOpts = new RequestOptions({
            headers: headers,
            search: params
        });

        return this._http.get(`${this.originUrl}api/post/getbyslug/`, getOpts).map(res => {
            return res.json();
        });
    }

    getList(request: PostListRequest): Observable<PostList> {
        const body = JSON.stringify(request);
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });

        return this._http
            .post(`${this.originUrl}api/post/getlist`, body, options)
            .map(res => res.json() || 0);
    }

    getSettings(): Observable<BlogSettings> {
        return this._http.get(`${this.originUrl}api/settings/get/`).map(res => {
            return res.json();
        });
    }

    saveSettings(settings:BlogSettings):Observable<BlogSettings>{
        const body = JSON.stringify(settings);
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });

        return this._http
            .post(`${this.originUrl}api/settings/save`, body, options)
            .map(res => res.json());
    }
}