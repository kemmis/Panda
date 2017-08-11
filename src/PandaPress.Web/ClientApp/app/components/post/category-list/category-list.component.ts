
import { Component, Input } from '@angular/core';
import { Post } from "../../../models/post";
import { PostCategory } from "../../../models/post-category";


@Component({
    selector: 'category-list',
    templateUrl: './category-list.component.html'
})
export class CategoryListComponent {
    @Input() post: Post;
    commaFor(category: PostCategory): string {
        // if last category in list, then don't output a ,
        if(this.post.categories.indexOf(category) == this.post.categories.length-1){
            return '';
        }
        return ', ';
    }
}   