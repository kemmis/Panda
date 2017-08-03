import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { PostComponent } from "./components/post/post.component";

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        PostComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent },
            { path: 'post/:slug', component: PostComponent },
            { path: '**', redirectTo: '' }
        ])
    ]
})
export class AppModuleShared {
}
