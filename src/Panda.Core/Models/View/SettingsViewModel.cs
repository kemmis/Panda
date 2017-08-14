namespace Panda.Core.Models.View
{
    public class SettingsViewModel
    {
        public string BlogName { get; set; }
        public int BlogId { get; set; }
        public string Description { get; set; }
        public int PostsPerPage { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpPort { get; set; }
        public string EmailPrefix { get; set; }
        public bool SmtpUseSsl { get; set; }
        public bool SendCommentEmail { get; set; }
    }
}
