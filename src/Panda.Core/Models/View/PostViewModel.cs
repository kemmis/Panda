using System.Collections.Generic;

namespace Panda.Core.Models.View
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublishDate { get; set; }
        public string Slug { get; set; }
        public string UserDisplayName { get; set; }
        public string UserAbout { get; set; }
        public string ProfilePicture { get; set; }
        public bool Published { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public int CommentCount { get; set; }
        public bool UseReCaptcha { get; set; }
        public string CaptchaKey { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}
