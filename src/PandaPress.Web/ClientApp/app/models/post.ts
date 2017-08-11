import { PostComment } from "./post-comment";

export class Post {
    title: string;
    content: string;
    userDisplayName: string;
    slug: string;
    comments: PostComment[];
    commentCount: number;
    id: string;
    publishDate: string;
}