import { Component, Input } from "@angular/core";
import { LoginResponse } from "../../models/login-response";

@Component({
    selector: 'control-panel',
    templateUrl: './control-panel.component.html',
})
export class ControlPanelComponent {

    @Input() login: LoginResponse;

    openSettings() {

    }

    logOut() {

    }
}
