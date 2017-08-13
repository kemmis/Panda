import { Injectable, Inject } from "@angular/core";
import { Http, RequestOptions, Headers, URLSearchParams } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { ProfileSettings } from "../models/profile-settings";

@Injectable()
export class ProfileService {
    constructor(private _http: Http, @Inject('BASE_URL') private originUrl: string) {
    }

    getProfileSettings(): Observable<ProfileSettings> {
        return this._http.get(`${this.originUrl}api/profile/getprofilesettings/`).map(res => {
            return res.json();
        });
    }

    saveProfileSettings(settings: ProfileSettings): Observable<ProfileSettings> {
        const body = JSON.stringify(settings);
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });

        return this._http
            .post(`${this.originUrl}api/profile/saveprofilesettings/`, body, options)
            .map(res => res.json());
    }

    savePhoto(file: File): Observable<ProfileSettings> {
        let formData: FormData = new FormData();
        formData.append('file', file, file.name);

        let headers = new Headers();
        headers.append('Accept', 'application/json');
        headers.append('enctype', 'multipart/form-data');
        
        let options = new RequestOptions({ headers: headers });

        return this._http
            .post(`${this.originUrl}api/profile/savephoto/`, formData, options)
            .map(res => res.json());
    }
}