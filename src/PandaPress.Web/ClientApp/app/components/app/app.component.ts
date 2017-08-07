import { Component, OnInit } from '@angular/core';
import { PostService } from "../../services/post.service";
import { MdDialog } from "@angular/material";
import { LoginComponent } from "../login/login.component";
import { AccountService } from "../../services/account.service";
import { LoginResponse } from "../../models/login-response";

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    providers: [PostService, MdDialog, AccountService]
})
export class AppComponent {
    constructor(private _dialog: MdDialog) { }

    openLogin(): void {
        var ref = this._dialog.open(LoginComponent);
        ref.componentInstance.loginSuccess.subscribe((res: LoginResponse) => {
            ref.close();
        });
    }
}
