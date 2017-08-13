namespace Panda.Core.Contracts
{
    public interface ISlugService
    {
        string CreateSlugFromPostTitle(string title);
        string CreateSlugFromCategoryTitle(string title);
    }
}
