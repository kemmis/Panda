import { Component, OnInit } from '@angular/core';
import { BlogSettings } from "../../models/blog-settings";
import { PostService } from "../../services/post.service";
import { MdDialogRef } from "@angular/material";


@Component({
    selector: 'settings',
    templateUrl: './settings.component.html'
})
export class SettingsComponent implements OnInit {

    constructor(private _postService: PostService, private _dialog:MdDialogRef<SettingsComponent>) { }

    settings: BlogSettings = new BlogSettings();
    saving:boolean = false;

    ngOnInit(): void {
        this._postService.getSettings().subscribe((settings: BlogSettings) => {            
            this.settings = settings;
        });
    }

    save(){
        this.saving = true;
        this._postService.saveSettings(this.settings).subscribe(s=>{
            this.saving = false;
            this._dialog.close();
        });
    }
}