
import { Component, Output, EventEmitter } from '@angular/core';
import { LoginRequest } from "../../models/login-request";
import { AccountService } from "../../services/account.service";
import { LoginResponse } from "../../models/login-response";

@Component({
    selector: 'login',
    templateUrl: './login.component.html'
})
export class LoginComponent {
    constructor(private _accountService: AccountService) { }
    @Output() loginSuccess: EventEmitter<LoginResponse> = new EventEmitter();
    @Output() loginFailure: EventEmitter<LoginResponse> = new EventEmitter();

    model: LoginRequest = new LoginRequest();

    login() {
        this._accountService.login(this.model).subscribe(res => {
            if (res.succeeded) {
                this.loginSuccess.emit(res);
            }
            else{
                this.loginFailure.emit(res);
            }
        });
    }
}