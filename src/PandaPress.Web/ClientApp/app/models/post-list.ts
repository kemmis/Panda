import { Post } from "./post";

export class PostList {
    posts: Post[];
    totalPosts: number;
    pageSize: number;
    pageIndex: number;
}