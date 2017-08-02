import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { Post } from "../models/post";
import 'rxjs/add/operator/map';
@Injectable()
export class PostService {
    constructor(private _http: Http) { }

    getPostBySlug(slug: string): Observable<Post> {
        let params = new URLSearchParams();
        params.append("slug", slug);
        return this._http.get("/api/post/getbyslug/", { params: params }).map(res=>{
            return res.json();
        });
    }
}