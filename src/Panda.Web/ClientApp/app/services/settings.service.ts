import { Injectable, Inject } from "@angular/core";
import { Http, Headers, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { BlogSettings } from "../models/blog-settings";

@Injectable()
export class SettingsService {
    constructor(private _http: Http, @Inject('BASE_URL') private originUrl: string) {
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

    sendTestEmail(settings:BlogSettings):Observable<boolean>{
        const body = JSON.stringify(settings);
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });

        return this._http
            .post(`${this.originUrl}api/settings/sendtestemail`, body, options)
            .map(res => res.json());
    }
}