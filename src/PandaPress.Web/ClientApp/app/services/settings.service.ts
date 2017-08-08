import { Injectable, Inject } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { BlogSettings } from "../models/blog-settings";

Injectable()
export class SettingsService {
    constructor(private _http: Http, @Inject('BASE_URL') private originUrl: string) {

    }

    getSettings(): Observable<BlogSettings> {
        return this._http.get(`${this.originUrl}api/settings/get/`).map(res => {
            return res.json();
        });
    }

    // saveSettings() {

    // }
}