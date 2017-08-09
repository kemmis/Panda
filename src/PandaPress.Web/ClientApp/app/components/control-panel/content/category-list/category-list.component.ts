
import { Component, Input } from "@angular/core";
import { BlogPostContent, BlogCategoryContent } from "../../../../models/blog-content";
import { DataSource } from "@angular/cdk/table";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { CategoryService } from "../../../../services/category.service";

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
    newCategory: BlogCategoryContent = new BlogCategoryContent();

    displayedColumns = ['id', 'title', 'postCount'];

    dataChange: BehaviorSubject<BlogCategoryContent[]> = new BehaviorSubject<BlogCategoryContent[]>([]);
    cateogriesArray: BlogCategoryContent[];

    saveCategory() {
        this._categoryService.addCategory(this.newCategory.title, this.newCategory.description)
            .subscribe((category: BlogCategoryContent) => {
                this.cateogriesArray.push(category);
                this.dataChange.next(this.cateogriesArray);
            });
    }

    connect() {
        return this.dataChange;
    }

    getThis() {
        return this;
    }
    disconnect() { }
}