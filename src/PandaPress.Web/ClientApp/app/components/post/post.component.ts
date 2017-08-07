
import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Post } from "../../models/post";

@Component({
    selector: 'post',
    templateUrl: './post.component.html'
})
export class PostComponent {
    @Input() post: Post;    
}