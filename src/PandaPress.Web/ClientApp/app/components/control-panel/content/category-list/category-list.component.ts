
import { Component, Input, ViewChild } from "@angular/core";
import { BlogPostContent, BlogCategoryContent } from "../../../../models/blog-content";
import { DataSource } from "@angular/cdk/table";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { CategoryService } from "../../../../services/category.service";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/observable/merge';
import { MdPaginator } from "@angular/material";

@Component({
    selector: 'category-content-list',
    templateUrl: './category-list.component.html',
    styleUrls: ['./category-list.component.less'],
    providers: [CategoryService]
})
export class CategoryContentListComponent {
    constructor(private _categoryService: CategoryService) { }
    @Input() set categories(categories: BlogCategoryContent[]) {
        this.cateogriesArray = categories;
        this.dataChange.next(this.cateogriesArray);
    }

    @ViewChild(MdPaginator) paginator: MdPaginator;

    newCategory: BlogCategoryContent = new BlogCategoryContent();

    displayedColumns = ['id', 'title', 'description', 'numPosts', 'delete'];

    dataChange: BehaviorSubject<BlogCategoryContent[]> = new BehaviorSubject<BlogCategoryContent[]>([]);
    cateogriesArray: BlogCategoryContent[] = [];

    get totalNum() {
        if (this.cateogriesArray)
            return this.cateogriesArray.length;
        return 0;
    }

    saveCategory() {
        this._categoryService.addCategory(this.newCategory.title, this.newCategory.description)
            .subscribe((category: BlogCategoryContent) => {
                this.cateogriesArray.push(category);
                this.dataChange.next(this.cateogriesArray);
                this.newCategory = new BlogCategoryContent();
            });
    }

    connect() {
        const displayDataChanges = [
            this.dataChange,
            this.paginator.page,
        ];
        return Observable.merge(...displayDataChanges).map(() => {
            if (!this.cateogriesArray) return [];
            const data = this.cateogriesArray.slice();

            // Grab the page's slice of data.
            const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
            return data.splice(startIndex, this.paginator.pageSize);
        });
    }

    getThis() {
        return this;
    }
    disconnect() { }
    delete(row: BlogCategoryContent) {
        this._categoryService.deleteCategory(row.id).subscribe(() => {
            this.cateogriesArray.splice(this.cateogriesArray.indexOf(row), 1);
            this.dataChange.next(this.cateogriesArray);
        });
    }
}