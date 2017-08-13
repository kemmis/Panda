export class EditPost {
    id: string;
    title: string;
    content: string;
    publishDate: string;
    slug: string;
    published: boolean;
    categories: string[];

    constructor() {
        this.categories = [];
    }
}