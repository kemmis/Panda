import { Injectable, Inject } from "@angular/core";
import { Http } from "@angular/http";
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
}