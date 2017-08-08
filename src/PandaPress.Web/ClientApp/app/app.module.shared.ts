import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { HomePageComponent } from "./components/pages/home/home.component";
import { PostPageComponent } from "./components/pages/post/post.component";
import { PostComponent } from "./components/post/post.component";
import { LoginComponent } from "./components/login/login.component";
import { MdDialogModule, OverlayModule, MdInputModule, MdButtonModule, MdCheckboxModule, MdSidenavModule, MdSnackBarModule, MdProgressBarModule } from "@angular/material";
import { SettingsComponent } from "./components/settings/settings.component";
import { ControlPanelComponent } from "./components/control-panel/control-panel.component";

@NgModule({
    declarations: [
        AppComponent,
        HomePageComponent,
        PostPageComponent,
        PostComponent,
        LoginComponent,
        SettingsComponent,
        ControlPanelComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        MdDialogModule,
        MdInputModule,
        MdButtonModule,
        MdCheckboxModule,
        MdSidenavModule,
        MdSnackBarModule,
        MdProgressBarModule,
        RouterModule.forRoot([
            { path: '', component: HomePageComponent },
            { path: 'post/:slug', component: PostPageComponent },
            { path: '**', redirectTo: '' }
        ])
    ],
    entryComponents: [LoginComponent, SettingsComponent]
})
export class AppModuleShared {
}
