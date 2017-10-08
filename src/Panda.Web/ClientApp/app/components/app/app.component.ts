import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { PostService } from "../../services/post.service";
import { MatDialog, MatSidenav, MatSnackBar } from "@angular/material";
import { LoginComponent } from "../login/login.component";
import { AccountService } from "../../services/account.service";
import { LoginResponse } from "../../models/login-response";
import { CommentService } from "../../services/comment.service";
import { BlogInfo } from "../../models/blog-info";
import { BlogService } from "../../services/blog.service";
import { UserInfoService } from "../../services/user-info.service";

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.less'],
    encapsulation: ViewEncapsulation.None,
    providers: [PostService, MatDialog, AccountService, MatSnackBar, CommentService, BlogService]
})
export class AppComponent implements OnInit {

    constructor(private _dialog: MatDialog, private _snackBar: MatSnackBar,
        private _accountService: AccountService, private _blogService: BlogService, private _userInfoService: UserInfoService) { }

    info: BlogInfo = new BlogInfo();
    login: LoginResponse = new LoginResponse();
    @ViewChild("adminNav") adminNav: MatSidenav;

    ngOnInit(): void {
        this._accountService.isLoggedIn().subscribe((login: LoginResponse) => {
            if (login.succeeded) {
                this.login = login;
                this._userInfoService.isLoggedIn = true;
                this._userInfoService.login = login;
                this._userInfoService.userLogin.emit(login);    
            }
        });

        this._blogService.getBlogInfo().subscribe((info:BlogInfo)=>{
            this.info = info;
        });
    }

    openLogin(): void {
        var ref = this._dialog.open(LoginComponent);
        ref.componentInstance.loginSuccess.subscribe((login: LoginResponse) => {
            this.login = login;
            this._userInfoService.isLoggedIn = true;
            this._userInfoService.login = login;
            this._userInfoService.userLogin.emit(login);
            ref.close();
        });
        ref.componentInstance.loginFailure.subscribe((login: LoginResponse) => {
            this._snackBar.open("Login Failed. Please try again.", "", { duration: 1500 })
        });
    }

    logOut() {
        this._accountService.logOut().subscribe(res => {
            this.login = new LoginResponse();
            this._userInfoService.isLoggedIn = false;
            this._userInfoService.userLogOut.emit();
            this._userInfoService.login = new LoginResponse();
            this.adminNav.close();
        });
    }

    closeSidenav(){
        this.adminNav.close();
    }
}
