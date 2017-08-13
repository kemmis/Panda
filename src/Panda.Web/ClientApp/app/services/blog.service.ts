import { Injectable, Inject } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { BlogInfo } from "../models/blog-info";

@Injectable()
export class BlogService {
    constructor(private _http: Http, @Inject('BASE_URL') private originUrl: string) {
    }
    getBlogInfo(): Observable<BlogInfo> {
        return this._http.get(`${this.originUrl}api/blog/getinfo/`).map(res => {
            return res.json();
        });
    }
}