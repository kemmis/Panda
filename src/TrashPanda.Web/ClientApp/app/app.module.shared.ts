import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { HomePageComponent } from "./components/pages/home/home.component";
import { PostPageComponent } from "./components/pages/post/post.component";
import { PostComponent } from "./components/post/post.component";

@NgModule({
    declarations: [
        AppComponent,
        HomePageComponent,
        PostPageComponent,
        PostComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: HomePageComponent },
            { path: 'post/:slug', component: PostPageComponent },
            { path: '**', redirectTo: '' }
        ])
    ]
})
export class AppModuleShared {
}
