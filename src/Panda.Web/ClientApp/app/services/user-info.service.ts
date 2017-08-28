import { Injectable, EventEmitter } from "@angular/core";
import { LoginResponse } from "../models/login-response";

@Injectable()
export class UserInfoService {
    login: LoginResponse = new LoginResponse();
    isLoggedIn: boolean = false;
    userLogin = new EventEmitter<LoginResponse>();
    userLogOut = new EventEmitter();
}