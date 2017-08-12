import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { HomePageComponent } from "./components/pages/home/home.component";
import { PostPageComponent } from "./components/pages/post/post.component";
import { PostComponent } from "./components/post/post.component";
import { LoginComponent } from "./components/login/login.component";
import { MdDialogModule, OverlayModule, MdInputModule, MdButtonModule, MdCheckboxModule, MdSidenavModule, MdSnackBarModule, MdProgressBarModule, MdCardModule, MdListModule, MdTabsModule, MdTableModule, MdTooltipModule, MdPaginatorModule, MdGridListModule, MdSelectModule } from "@angular/material";
import { ControlPanelComponent } from "./components/control-panel/control-panel.component";
import { DashboardComponent } from "./components/control-panel/dashboard/dashboard.component";
import { ContentComponent } from "./components/control-panel/content/content.component";
import { PostContentListComponent } from "./components/control-panel/content/post-list/post-list.component";
import { CdkTableModule } from "@angular/cdk/table";
import { PasswordComponent } from "./components/control-panel/password/password.component";
import { CategoryContentListComponent } from "./components/control-panel/content/category-list/category-list.component";
import { ProfileComponent } from "./components/control-panel/profile/profile.component";
import { SettingsComponent } from "./components/control-panel/settings/settings.component";
import { CommentComponent } from "./components/post/comment/comment.component";
import { CommentListComponent } from "./components/post/comment/comment-list.component";
import { CommentFormComponent } from "./components/post/comment/comment-form.component";
import { PostDeletedComponent } from "./components/control-panel/content/post-list/post-deleted.component";
import { CategoryListComponent } from "./components/post/category-list/category-list.component";
import { CategoryPageComponent } from "./components/pages/category/category.component";
import { PostEditorComponent } from "./components/control-panel/post-editor/post-editor.component";
import { TinyMceComponent } from "./components/control-panel/post-editor/tiny-mce-component";

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
        CategoryContentListComponent,
        ProfileComponent,
        CommentComponent,
        CommentListComponent,
        CommentFormComponent,
        PostDeletedComponent,
        CategoryListComponent,
        CategoryPageComponent,
        PostEditorComponent,
        TinyMceComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
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
        MdSelectModule,
        RouterModule.forRoot([
            { path: '', component: HomePageComponent },
            { path: 'post/:slug', component: PostPageComponent },
            { path: 'category/:slug', component: CategoryPageComponent },
            { path: '**', redirectTo: '' }
        ])
    ],
    entryComponents: [LoginComponent,
        SettingsComponent,
        DashboardComponent,
        ContentComponent,
        PasswordComponent,
        ProfileComponent,
        PostDeletedComponent,
        PostEditorComponent]
})
export class AppModuleShared {
}

