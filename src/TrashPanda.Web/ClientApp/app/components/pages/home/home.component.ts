import { Component, OnInit, OnDestroy } from '@angular/core';
import { PostService } from "../../../services/post.service";
import { PostList } from "../../../models/post-list";
import { PostListRequest } from "../../../models/post-list-request";

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomePageComponent implements OnInit {

    constructor(private _postService: PostService) {

    }
    
    list: PostList = new PostList();

    ngOnInit(): void {
        let request: PostListRequest = {
            pageIndex: 0,
            pageSize: 5
        };

        this._postService.getList(request).subscribe(response => {
            this.list = response;
        });
    }
}
