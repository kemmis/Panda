using System.Collections.Generic;

namespace Panda.Core.Models.Data
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PostsPerPage { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpPort { get; set; }
        public string EmailPrefix { get; set; }
        public bool SmtpUseSsl { get; set; }
        public bool SendCommentEmail { get; set; }
        public bool useReCaptcha { get; set; }
        public string captchaKey { get; set; }
        public string captchaSecret { get; set; }
        public List<BlogApplicationUser> BlogApplicationUsers { get; set; } = new List<BlogApplicationUser>();
        public List<Post> Posts { get; set; }
    }
}
