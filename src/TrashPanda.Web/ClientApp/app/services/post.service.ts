import { Injectable } from "@angular/core";
import { Http, RequestOptions, Headers, URLSearchParams } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { Post } from "../models/post";
import 'rxjs/add/operator/map';
@Injectable()
export class PostService {
    constructor(private _http: Http) { }

    getPostBySlug(slug: string): Observable<Post> {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");

        const params = new URLSearchParams()
        params.set("slug", slug);

        const getOpts = new RequestOptions({
            headers: headers,
            search: params
        });

        return this._http.get("/api/post/getbyslug/", getOpts).map(res => {
            return res.json();
        });
    }
}