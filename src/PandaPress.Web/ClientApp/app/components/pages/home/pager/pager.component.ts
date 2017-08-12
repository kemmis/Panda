import { PostList } from "../../../../models/post-list";
import { Input, Component } from "@angular/core";
import { Router } from "@angular/router";

@Component({
    selector: 'pager',
    templateUrl: './pager.component.html'
})
export class PagerComponent {
    constructor(private _router: Router) { }
    @Input() pageList: PostList;

    get showNext() {
        return this.pageList.totalPosts > (this.pageList.pageIndex + 1) * this.pageList.pageSize;
    }

    get showPrev() {
        return this.pageList.pageIndex > 0;
    }

    nextPage() {
        this._router.navigate(['/page', this.pageList.pageIndex + 1]);
    }

    prevPage() {
        this._router.navigate(['/page', this.pageList.pageIndex - 1]);
    }
}
