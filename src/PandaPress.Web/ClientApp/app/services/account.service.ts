import { LoginResponse } from "../models/login-response";
import { LoginRequest } from "../models/login-request";
import { RequestOptions, Http, Headers } from "@angular/http";
import { Injectable, Inject } from "@angular/core";
import { Observable } from "rxjs/Observable";

@Injectable()
export class AccountService {
    constructor(private _http: Http, @Inject('BASE_URL') _originUrl: string) { 
        this.originUrl = _originUrl;
    }
originUrl:any;
    login(request: LoginRequest, ): Observable<LoginResponse> {
        const body = JSON.stringify(request);
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });

        return this._http
            .post(`${this.originUrl}api/account/login`, body, options)
            .map(res => res.json());
    }

    isLoggedIn():Observable<LoginResponse> {
        return this._http.get(`${this.originUrl}api/account/isloggedin/`).map(res => {
            return res.json();
        });
    }

    logOut(): Observable<boolean> {
        return this._http.get(`${this.originUrl}api/account/logout/`).map(res => {
            return res.json();
        });
    }
}