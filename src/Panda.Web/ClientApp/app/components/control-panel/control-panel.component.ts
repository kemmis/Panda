import { Component, Input, Output, EventEmitter, OnInit } from "@angular/core";
import { LoginResponse } from "../../models/login-response";
import { MatDialog } from "@angular/material";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { ContentComponent } from "./content/content.component";
import { PasswordComponent } from "./password/password.component";
import { ProfileComponent } from "./profile/profile.component";
import { ProfileSettings } from "../../models/profile-settings";
import { SettingsComponent } from "./settings/settings.component";
import { PostEditorComponent } from "./post-editor/post-editor.component";
import { Router } from "@angular/router";
import { EventService } from "../../services/event.service";
import { Post } from "../../models/post";

@Component({
    selector: 'control-panel',
    templateUrl: './control-panel.component.html',
    styleUrls: ['./control-panel.component.less']
})
export class ControlPanelComponent implements OnInit {
    ngOnInit(): void {
        this._events.editPost.subscribe((post: Post) => {
            this.openEditorFor(post.id);
        });
    }
    constructor(private _dialog: MatDialog, private router: Router, private _events: EventService) { }
    @Input() login: LoginResponse;

    @Output() logout: EventEmitter<void> = new EventEmitter();
    @Output() closeSidenav = new EventEmitter();

    openSettings() {
        this._dialog.open(SettingsComponent, {
            width: "600px"
        });
    }

    logOut() {
        this.logout.emit();
    }

    openDashboard() {
        var dashboardDialog = this._dialog.open(DashboardComponent, {
            width: "800px",
        });
        dashboardDialog.componentInstance.navigate.subscribe((slug: string) => {
            dashboardDialog.close();
            this.closeSidenav.emit();
            this.router.navigate(['/post', slug]);
        });;
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
            width: "700px"
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
            data: { postId: postId },
            disableClose: true
        });
        editorDialog.componentInstance.navigate.subscribe((slug: string) => {
            this._dialog.closeAll();
            this.closeSidenav.emit();
            this.router.navigate(['/post', slug]);
        });
    }
}