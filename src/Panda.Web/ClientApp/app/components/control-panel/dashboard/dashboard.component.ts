import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
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
    @Output() navigate = new EventEmitter<string>();

    loading: boolean = false;
    dashboardData: DashboardData = new DashboardData();

    ngOnInit(): void {
        this.loading = true;
        this._dashboardService.getDashboardData().subscribe((data: DashboardData) => {
            this.dashboardData = data;
            this.loading = false;
        });
    }

    navigateToPost(slug: string) {
        this.navigate.emit(slug);
    }
}