import { PostComment } from "./post-comment";
import { PostCategory } from "./post-category";

export class Post {
    constructor() {
        this.comments = new Array();
        this.categories = new Array();
    }
    title: string;
    content: string;
    userDisplayName: string;
    userAbout: string;
    profilePicture: string;
    slug: string;
    comments: PostComment[];
    commentCount: number;
    id: string;
    publishDate: string;
    categories: PostCategory[];
}