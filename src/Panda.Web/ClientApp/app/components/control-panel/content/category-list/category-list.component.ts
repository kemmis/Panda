
import { Component, Input, ViewChild, ElementRef, OnInit } from "@angular/core";
import { BlogPostContent, BlogCategoryContent } from "../../../../models/blog-content";
import { DataSource } from "@angular/cdk/table";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { CategoryService } from "../../../../services/category.service";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/observable/merge';
import { MatPaginator, MatSnackBar } from "@angular/material";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

@Component({
    selector: 'category-content-list',
    templateUrl: './category-list.component.html',
    styleUrls: ['./category-list.component.less'],
    providers: [CategoryService]
})
export class CategoryContentListComponent implements OnInit {

    constructor(private _categoryService: CategoryService,
        private _snackBar: MatSnackBar,
        private _formBuilder: FormBuilder) { }

    @Input() set categories(categories: BlogCategoryContent[]) {
        this.cateogriesArray = categories;
        this.dataChange.next(this.cateogriesArray);
    }

    @ViewChild("paginator") paginator: MatPaginator;
    @ViewChild("title") titleInput: ElementRef;

    displayedColumns = ['title', 'description', 'numPosts', 'delete'];

    dataChange: BehaviorSubject<BlogCategoryContent[]> = new BehaviorSubject<BlogCategoryContent[]>([]);
    cateogriesArray: BlogCategoryContent[] = [];
    form: FormGroup

    ngOnInit(): void {
        this.form = this._formBuilder.group({
            title: ['', Validators.required],
            description: ''
        });
    }

    get totalNum() {
        if (this.cateogriesArray)
            return this.cateogriesArray.length;
        return 0;
    }

    saveCategory() {
        var newCategory = this.form.value;
        this._categoryService.addCategory(newCategory.title, newCategory.description)
            .subscribe((category: BlogCategoryContent) => {
                this.cateogriesArray.push(category);
                this.dataChange.next(this.cateogriesArray);
                this.titleInput.nativeElement.focus();
                this.form.reset();
                this.form.controls['title'].clearAsyncValidators();
                this.form.controls['title'].clearValidators();
                this.form.controls['title'].updateValueAndValidity();                
                this.form.controls['title'].setValidators(Validators.required);
                this._snackBar.open(`Category "${category.title}" added!`, "", { duration: 2000 });
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
            this._snackBar.open(`Category "${row.title}" deleted.`, "", { duration: 2000 });
        });
    }
}