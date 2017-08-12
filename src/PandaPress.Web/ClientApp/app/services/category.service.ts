import { Injectable, Inject } from "@angular/core";
import { Http, Headers, URLSearchParams, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { BlogCategoryContent } from "../models/blog-content";

@Injectable()
export class CategoryService {
    constructor(private _http: Http, @Inject('BASE_URL') private originUrl: string) {
    }

    getCategories():Observable<BlogCategoryContent[]>{
        return this._http.get(`${this.originUrl}api/category/getall/`).map(res => {
            return res.json();
        });
    }

    addCategory(title: string, description: string): Observable<BlogCategoryContent> {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");

        const params = new URLSearchParams()
        params.set("title", title);
        params.set("description", description);

        const getOpts = new RequestOptions({
            headers: headers,
            search: params
        });

        return this._http.get(`${this.originUrl}api/category/add/`, getOpts).map(res => {
            return res.json();
        });
    }

    deleteCategory(categoryId: string): Observable<void> {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");

        const params = new URLSearchParams()
        params.set("categoryId", categoryId);

        const getOpts = new RequestOptions({
            headers: headers,
            search: params
        });

        return this._http.get(`${this.originUrl}api/category/delete/`, getOpts).map(res => {
            return;
        });
    }
}