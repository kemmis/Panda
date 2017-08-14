export class DashboardData {
    numPosts: number;
    numDrafts: number;
    recentComments: RecentComment[];
}

export class RecentComment {
    authorName: string;
    authorEmail: string;
    text: string;
    createdDateTime: string;
    id: string;
    removed: void;
    postId: string;
    postTitle: string;
    postSlug: string;
}