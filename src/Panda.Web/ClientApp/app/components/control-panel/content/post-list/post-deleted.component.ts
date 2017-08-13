import { Input, Component, Output, EventEmitter, Inject } from "@angular/core";
import { BlogPostContent } from "../../../../models/blog-content";
import { ContentService } from "../../../../services/content.service";
import { MD_SNACK_BAR_DATA } from "@angular/material";

@Component({
    selector: 'post-deleted',
    templateUrl: './post-deleted.component.html',
    styleUrls: ['./post-deleted.component.less'],
    providers: [ContentService]
})
export class PostDeletedComponent {
    constructor(private _contentService: ContentService, @Inject(MD_SNACK_BAR_DATA) public data: any) { }

    @Output() postRestored = new EventEmitter<BlogPostContent>();
    @Output() dismissed = new EventEmitter<void>();

    undo() {
        this._contentService.unDeletePost(this.data.id).subscribe(() => {
            this.postRestored.emit(this.data);
        });
    }

    dismiss() {
        this.dismissed.emit();
    }
}