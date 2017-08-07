import { LoginResponse } from "../models/login-response";
import { LoginRequest } from "../models/login-request";
import { RequestOptions, Http, Headers } from "@angular/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";

@Injectable()
export class AccountService {
    constructor(private _http: Http) { }

    login(request: LoginRequest): Observable<LoginResponse> {
        const body = JSON.stringify(request);
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });

        return this._http
            .post("/api/account/login", body, options)
            .map(res => res.json());
    }

    logout(): Observable<boolean> {
        return this._http.get("/api/account/logout/").map(res => {
            return res.json();
        });
    }
}