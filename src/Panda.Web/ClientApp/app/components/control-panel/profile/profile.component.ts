import { Component, OnInit, EventEmitter, Output, ViewChild, AfterViewInit } from "@angular/core";
import { ProfileService } from "../../../services/profile.service";
import { ProfileSettings } from "../../../models/profile-settings";
import { MatDialogRef, MatSnackBar } from "@angular/material";
import { TinyMceProfileComponent } from "./tiny-mce-component";

@Component({
    selector: 'profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.less'],
    providers: [ProfileService]
})
export class ProfileComponent implements AfterViewInit {

    ngAfterViewInit(): void {
        this._profileService.getProfileSettings().subscribe((settings: ProfileSettings) => {
            this.settings = settings;
            this.tmce.content = settings.about;
        });
    }

    constructor(private _profileService: ProfileService,
        private _dialog: MatDialogRef<ProfileComponent>,
        private _snackBar: MatSnackBar) { }

    @Output() settingsUpdated = new EventEmitter<ProfileSettings>();
    @ViewChild("file") file: any;
    @ViewChild("tmce") tmce: TinyMceProfileComponent;

    settings = new ProfileSettings();
    saving:boolean = false;

    save() {
        this.saving = true;
        this.settings.about = this.tmce.content;

        this._profileService.saveProfileSettings(this.settings).subscribe((settings: ProfileSettings) => {
            this.saving = false;
            this.settingsUpdated.emit(settings);
            this._dialog.close();
            this._snackBar.open("Profile settings saved.", "", { duration: 2000 });
        });
    }

    uploadPhoto(event: any) {       
        let fileList: FileList = event.target.files;
        if (fileList.length > 0) {
            this.saving = true;
            this._profileService.savePhoto(fileList[0]).subscribe((settings: ProfileSettings) => {
                this.saving = false;
                this.settings.profilePicture = settings.profilePicture;
                this.file.nativeElement.value = "";
            });
        }
    }

    removePhoto() {
        this.saving = true;
        this._profileService.removePhoto().subscribe((settings: ProfileSettings) => {
            this.saving = false;
            this.settings.profilePicture = settings.profilePicture;
        });
    }
}
