import { Component, OnInit } from '@angular/core';
import { MdDialogRef, MdSnackBar } from "@angular/material";
import { PostService } from "../../../services/post.service";
import { BlogSettings } from "../../../models/blog-settings";
import { SettingsService } from '../../../services/settings.service';


@Component({
    selector: 'settings',
    templateUrl: './settings.component.html',
    styleUrls: ['./settings.component.less'],
    providers: [SettingsService]
})
export class SettingsComponent implements OnInit {

    constructor(private _settingsService: SettingsService,
        private _dialog: MdDialogRef<SettingsComponent>,
        private _snackBar: MdSnackBar) { }

    settings: BlogSettings = new BlogSettings();
    saving: boolean = false;

    get pageSizes(): number[] {
        return BlogSettings.pageSizeOptions;
    }

    ngOnInit(): void {
        this._settingsService.getSettings().subscribe((settings: BlogSettings) => {
            this.settings = settings;
        });
    }

    save() {
        this.saving = true;
        this._settingsService.saveSettings(this.settings).subscribe(s => {
            this.saving = false;
            this._dialog.close();
            this._snackBar.open("Settings Saved!", "", { duration: 2000 });
        });
    }

    sendTestEmail() {
        this.saving = true;
        this._settingsService.sendTestEmail(this.settings).subscribe((success: boolean) => {
            this.saving = false;
            if (success) {
                this._snackBar.open("Test email sent successfully!", "", { duration: 3000 });
            }
            else {
                this._snackBar.open("Test email failed. Please check your settings.", "", { duration: 5000 });
            }
        });
    }
}