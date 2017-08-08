import { Component, OnInit } from '@angular/core';
import { BlogSettings } from "../../models/blog-settings";
import { PostService } from "../../services/post.service";


@Component({
    selector: 'settings',
    templateUrl: './settings.component.html'
})
export class SettingsComponent implements OnInit {

    constructor(private _postService: PostService) { }

    settings: BlogSettings;

    ngOnInit(): void {
        this._postService.getSettings().subscribe((settings: BlogSettings) => {
            this.settings = settings;
        });
    }
}