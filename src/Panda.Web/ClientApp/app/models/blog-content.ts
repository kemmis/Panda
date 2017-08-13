export class BlogContent {
    posts: BlogPostContent[];
    categories: BlogCategoryContent[];
}

export class BlogPostContent {
    title: string;
    id: string;
    published: boolean;
}

export class BlogCategoryContent {
    id: string;
    title: string;
    numPosts: number;
    description: string;
}