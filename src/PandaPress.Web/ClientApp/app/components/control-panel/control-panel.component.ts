import { Component, Input, Output, EventEmitter } from "@angular/core";
import { LoginResponse } from "../../models/login-response";
import { MdDialog } from "@angular/material";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { ContentComponent } from "./content/content.component";
import { PasswordComponent } from "./password/password.component";
import { ProfileComponent } from "./profile/profile.component";
import { ProfileSettings } from "../../models/profile-settings";
import { SettingsComponent } from "./settings/settings.component";
import { PostEditorComponent } from "./post-editor/post-editor.component";
import { Router } from "@angular/router";

@Component({
    selector: 'control-panel',
    templateUrl: './control-panel.component.html',
    styleUrls: ['./control-panel.component.less']
})
export class ControlPanelComponent {
    constructor(private _dialog: MdDialog, private router: Router) { }
    @Input() login: LoginResponse;

    @Output() logout: EventEmitter<void> = new EventEmitter();
    @Output() closeSidenav = new EventEmitter();

    openSettings() {
        this._dialog.open(SettingsComponent, {
            width: "400px"
        });
    }

    logOut() {
        this.logout.emit();
    }

    openDashboard() {
        this._dialog.open(DashboardComponent, {
            width: "800px",
        });
    }

    openContent() {
        this._dialog.open(ContentComponent, {
            width: "800px",
        }).componentInstance.editPost.subscribe((postId: string) => {
            this.openEditorFor(postId);
        }); 
    }

    changePassword() {
        this._dialog.open(PasswordComponent);
    }

    editProfile() {
        this._dialog.open(ProfileComponent, {
            width: "400px"
        }).componentInstance.settingsUpdated.subscribe((settings: ProfileSettings) => {
            this.login.displayName = settings.displayName;
        });
    }

    openPostEditor() {
        this.openEditorFor("0");
    }

    openEditorFor(postId: string) { 
        var editorDialog = this._dialog.open(PostEditorComponent, {
            width: window.innerWidth - 200 + "px",
            height: window.innerHeight - 200 + "px",
            data: { postId: postId }
        });
        editorDialog.componentInstance.navigate.subscribe((slug: string) => {
            editorDialog.close();
            this.closeSidenav.emit();
            this.router.navigate(['/post', slug]);
        });
    }
}