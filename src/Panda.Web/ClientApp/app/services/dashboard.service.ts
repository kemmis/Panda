import { Injectable, Inject } from "@angular/core";
import { Http, RequestOptions, Headers, URLSearchParams } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { DashboardData } from "../models/dashboard-data";

@Injectable()
export class DashboardService {
    constructor(private _http: Http, @Inject('BASE_URL') private originUrl: string) {
    }

    getDashboardData(): Observable<DashboardData> {
        return this._http.get(`${this.originUrl}api/dashboard/getdata/`).map(res => {
            return res.json();
        });
    }
}