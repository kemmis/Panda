using System.Collections.Generic;

namespace Panda.Core.Models.View
{
    public class DashboardDataViewModel
    {
        public int NumPosts { get; set; }
        public int NumDrafts { get; set; }
        public List<DashboardDataCommentViewModel> RecentComments { get; set; }
    }

    public class DashboardDataCommentViewModel
    {
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Text { get; set; }
        public string CreatedDateTime { get; set; }
        public int Id { get; set; }
        public bool Removed { get; set; }
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostSlug { get; set; }
    }
}
