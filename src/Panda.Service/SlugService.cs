using System.Text.RegularExpressions;
using Panda.Core.Contracts;

namespace Panda.Service
{
    public class SlugService : ISlugService
    {
        private readonly IPandaDataProvider _dataProvider;

        public SlugService(IPandaDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public string CreateSlugFromCategoryTitle(string title)
        {
            var baseSlug = title.GenerateSlug();
            var currentSlug = baseSlug;

            var categoryWithSlug = _dataProvider.GetCategoryBySlug(currentSlug);

            var i = 1;

            while (categoryWithSlug != null)
            {
                currentSlug = $"{baseSlug}-{i}";
                categoryWithSlug = _dataProvider.GetCategoryBySlug(currentSlug);
                i++;
            }

            return currentSlug;
        }

        public string CreateSlugFromPostTitle(string title)
        {
            var baseSlug = title.GenerateSlug();
            var currentSlug = baseSlug;

            var postWithSlug = _dataProvider.GetPostBySlug(currentSlug);

            var i = 1;

            while (postWithSlug != null)
            {
                currentSlug = $"{baseSlug}-{i}";
                postWithSlug = _dataProvider.GetPostBySlug(currentSlug);
                i++;
            }

            return currentSlug;
        }


    }

    public static class StringSlugExtentions
    {
        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.RemoveAccent().ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
