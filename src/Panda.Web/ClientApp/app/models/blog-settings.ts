export class BlogSettings {
    blogName: string;
    blogId: number;
    description: string;
    postsPerPage: string;
    smtpUsername: string;
    smtpPassword: string;
    smtpHost: string;
    smtpPort: string;
    emailPrefix: string;
    smtpUseSsl: boolean;
    sendCommentEmail: boolean;
    static pageSizeOptions: number[] = [1, 2, 3, 4, 5, 10, 25, 100];
    captchaKey: string;
    captchaSecret: string;
    useReCaptcha: boolean;
}

