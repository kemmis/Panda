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
import { MdDialogModule, OverlayModule, MdInputModule, MdButtonModule, MdCheckboxModule, MdSidenavModule, MdSnackBarModule, MdProgressBarModule, MdCardModule, MdListModule, MdTabsModule, MdTableModule, MdTooltipModule, MdPaginatorModule, MdGridListModule } from "@angular/material";
import { SettingsComponent } from "./components/settings/settings.component";
import { ControlPanelComponent } from "./components/control-panel/control-panel.component";
import { DashboardComponent } from "./components/control-panel/dashboard/dashboard.component";
import { ContentComponent } from "./components/control-panel/content/content.component";
import { PostContentListComponent } from "./components/control-panel/content/post-list/post-list.component";
import { CdkTableModule } from "@angular/cdk/table";
import { PasswordComponent } from "./components/control-panel/password/password.component";
import { CategoryContentListComponent } from "./components/control-panel/content/category-list/category-list.component";

@NgModule({
    declarations: [
        AppComponent,
        HomePageComponent,
        PostPageComponent,
        PostComponent,
        LoginComponent,
        SettingsComponent,
        ControlPanelComponent,
        DashboardComponent,
        ContentComponent,
        PostContentListComponent,
        PasswordComponent,
        CategoryContentListComponent
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
        MdCardModule,
        MdListModule,
        MdTabsModule,
        MdTableModule,
        CdkTableModule,
        MdTooltipModule,
        MdPaginatorModule,
        MdGridListModule,
        RouterModule.forRoot([
            { path: '', component: HomePageComponent },
            { path: 'post/:slug', component: PostPageComponent },
            { path: '**', redirectTo: '' }
        ])
    ],
    entryComponents: [LoginComponent, SettingsComponent, DashboardComponent, ContentComponent, PasswordComponent]
})
export class AppModuleShared {
}

