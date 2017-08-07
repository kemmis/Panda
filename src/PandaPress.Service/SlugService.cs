﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using PandaPress.Core.Contracts;

namespace PandaPress.Service
{
    public class SlugService : ISlugService
    {
        private readonly IPandaPressDataProvider _dataProvider;

        public SlugService(IPandaPressDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public string CreateSlugFromTitle(string title)
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
