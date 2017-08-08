import { Component, Input } from "@angular/core";
import { LoginResponse } from "../../models/login-response";
import { MdDialog } from "@angular/material";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { ContentComponent } from "./content/content.component";

@Component({
    selector: 'control-panel',
    templateUrl: './control-panel.component.html',
    styleUrls: ['./control-panel.component.less']
})
export class ControlPanelComponent {
    constructor(private _dialog: MdDialog) { }
    @Input() login: LoginResponse;

    openSettings() {

    }

    logOut() {

    }

    openDashboard() {
        this._dialog.open(DashboardComponent, {
            width: "800px",
        });
    }

    openContent(){
        this._dialog.open(ContentComponent, {
            width: "800px",
        });
    }
}