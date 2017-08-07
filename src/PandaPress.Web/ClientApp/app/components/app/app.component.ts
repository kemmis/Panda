import { Component, OnInit } from '@angular/core';
import { PostService } from "../../services/post.service";
import { MdDialog } from "@angular/material";
import { LoginComponent } from "../login/login.component";

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    providers: [PostService, MdDialog]
})
export class AppComponent {
    constructor(private _dialog: MdDialog) { }

    openLogin(): void {
        var ref = this._dialog.open(LoginComponent, {
           
        });
    }
}
