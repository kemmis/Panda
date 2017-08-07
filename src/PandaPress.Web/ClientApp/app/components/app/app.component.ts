import { Component } from '@angular/core';
import { PostService } from "../../services/post.service";

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    providers: [PostService]
})
export class AppComponent {
}
