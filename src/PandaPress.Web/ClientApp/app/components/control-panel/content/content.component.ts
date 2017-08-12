import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { ContentService } from "../../../services/content.service";
import { BlogContent } from "../../../models/blog-content";

@Component({
    selector: 'content',
    templateUrl: './content.component.html',
    styleUrls: ['./content.component.less'],
    providers: [ContentService]
})
export class ContentComponent implements OnInit {
    constructor(private _contentService: ContentService) { }
    content: BlogContent = new BlogContent();
    @Output() editPost = new EventEmitter<string>();
    ngOnInit(): void {
        this._contentService.getContent().subscribe((content: BlogContent) => {
            this.content = content;
        });
    }

    onEditPost(postId: string) {
        this.editPost.emit(postId);
    }
}