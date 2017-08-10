import { Component, OnInit, EventEmitter, Output } from "@angular/core";
import { ProfileService } from "../../../services/profile.service";
import { ProfileSettings } from "../../../models/profile-settings";
import { MdDialogRef, MdSnackBar } from "@angular/material";

@Component({
    selector: 'profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.less'],
    providers: [ProfileService]
})
export class ProfileComponent implements OnInit {
    ngOnInit(): void {
        this._profileService.getProfileSettings().subscribe((settings: ProfileSettings) => {
            this.settings = settings;
        });
    }

    constructor(private _profileService: ProfileService,
        private _dialog: MdDialogRef<ProfileComponent>,
        private _snackBar: MdSnackBar) { }

    @Output() settingsUpdated = new EventEmitter<ProfileSettings>();
    
    settings = new ProfileSettings();

    save() {
        this._profileService.saveProfileSettings(this.settings).subscribe((settings: ProfileSettings) => {
            this.settingsUpdated.emit(settings);
            this._dialog.close();
            this._snackBar.open("Profile settings saved.", "", { duration: 2000 });
        });
    }
}
