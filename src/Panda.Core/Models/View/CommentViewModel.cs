namespace Panda.Core.Models.View
{
    public class CommentViewModel
    {
        public string AuthorName { get; set; }
        public string Text { get; set; }
        public string CreatedDateTime { get; set; }
        public int Id { get; set; }
        public bool Removed { get; set; }
        public string Gravatar { get; set; }
        public bool Deleted { get; set; }
        public bool FromAdmin { get; set; }
    }
}
