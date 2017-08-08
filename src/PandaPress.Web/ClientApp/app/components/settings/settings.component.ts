import { Component, OnInit } from '@angular/core';
import { SettingsService } from "../../services/settings.service";
import { BlogSettings } from "../../models/blog-settings";


@Component({
    selector: 'settings',
    templateUrl: './settings.component.html'
})
export class SettingsComponent implements OnInit {

    constructor(private _settingsService: SettingsService) { }

    settings: BlogSettings;

    ngOnInit(): void {
        this._settingsService.getSettings().subscribe((settings: BlogSettings) => {
            this.settings = settings;
        });
    }
}