namespace Panda.Core.Models.Request
{
    public class CommentCreateRequest
    {
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Text { get; set; }
        public int PostId { get; set; }
    }
}
