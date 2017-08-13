namespace Panda.Core.Models.Request
{
    public class PostListRequest
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string CategorySlug { get; set; }
    }
}
