import { Component, OnInit, ViewChild } from '@angular/core';
import { PostService } from "../../services/post.service";
import { MdDialog, MdSidenav, MdSnackBar } from "@angular/material";
import { LoginComponent } from "../login/login.component";
import { AccountService } from "../../services/account.service";
import { LoginResponse } from "../../models/login-response";
import { SettingsComponent } from "../settings/settings.component";
import { SettingsService } from "../../services/settings.service";

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    providers: [PostService, MdDialog, AccountService, MdSnackBar, SettingsService]
})
export class AppComponent implements OnInit {

    constructor(private _dialog: MdDialog, private _snackBar: MdSnackBar, private _accountService: AccountService) { }

    login: LoginResponse = new LoginResponse();
    @ViewChild("adminNav") adminNav: MdSidenav;


    ngOnInit(): void {
        this._accountService.isLoggedIn().subscribe((login: LoginResponse) => {
            if (login.succeeded) {
                this.login = login;
            }
        });
    }

    openLogin(): void {
        var ref = this._dialog.open(LoginComponent);
        ref.componentInstance.loginSuccess.subscribe((login: LoginResponse) => {
            this.login = login;
            ref.close();
        });
        ref.componentInstance.loginFailure.subscribe((login: LoginResponse) => {
            this._snackBar.open("Login Failed. Please try again.", "", { duration: 1500 })
        });
    }

    openSettings(): void {
        var ref = this._dialog.open(SettingsComponent, { width: "400px" });
    }

    logOut() {
        this._accountService.logOut().subscribe(res => {
            this.login = new LoginResponse();
            this.adminNav.close();
        });
    }
}
