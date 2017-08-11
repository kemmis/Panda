export class BlogSettings {
    blogName: string;
    blogId: number;
    description: string;
    postsPerPage: string;

    static pageSizeOptions: number[] = [1, 5, 10, 25, 100];
}

