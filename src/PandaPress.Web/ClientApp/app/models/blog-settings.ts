export class BlogSettings {
    blogName: string;
    blogId: number;
    description: string;
    postsPerPage: string;

    static pageSizeOptions: number[] = [1, 2, 3, 4, 5, 10, 25, 100];
}

