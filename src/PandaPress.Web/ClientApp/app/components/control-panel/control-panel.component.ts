import { Component, Input } from "@angular/core";
import { LoginResponse } from "../../models/login-response";

@Component({
    selector: 'control-panel',
    templateUrl: './control-panel.component.html',
    styleUrls: ['./control-panel.component.css']
})
export class ControlPanelComponent {

    @Input() login: LoginResponse;

    openSettings() {

    }

    logOut() {

    }
}