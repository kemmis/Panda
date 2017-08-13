namespace Panda.Core.Models.Data
{
    public class BlogApplicationUser
    {
        public int BlogId { get; set; }
        public string ApplicationUserId { get; set; }
        public Blog Blog { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
