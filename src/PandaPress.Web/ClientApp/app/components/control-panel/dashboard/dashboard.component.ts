import { Component, OnInit, Input } from "@angular/core";
import { DashboardService } from "../../../services/dashboard.service";
import { DashboardData } from "../../../models/dashboard-data";

@Component({
    selector: 'dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.less'],
    providers: [DashboardService]
})
export class DashboardComponent implements OnInit {
    constructor(private _dashboardService: DashboardService) { }

    loading:boolean = false;
    dashboardData:DashboardData = new DashboardData();

    ngOnInit(): void {
        this.loading = true;
        this._dashboardService.getDashboardData().subscribe((data:DashboardData)=>{
            this.dashboardData = data;
            this.loading = false;
        });
    }
}