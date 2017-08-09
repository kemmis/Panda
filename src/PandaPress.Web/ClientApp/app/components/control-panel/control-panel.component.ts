import { Component, Input, Output, EventEmitter } from "@angular/core";
import { LoginResponse } from "../../models/login-response";
import { MdDialog } from "@angular/material";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { ContentComponent } from "./content/content.component";
import { PasswordComponent } from "./password/password.component";

@Component({
    selector: 'control-panel',
    templateUrl: './control-panel.component.html',
    styleUrls: ['./control-panel.component.less']
})
export class ControlPanelComponent {
    constructor(private _dialog: MdDialog) { }
    @Input() login: LoginResponse;

    @Output() logout:EventEmitter<void> = new EventEmitter();

    openSettings() {

    }

    logOut() {
        this.logout.emit();
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

    changePassword(){
        this._dialog.open(PasswordComponent);
    }
}