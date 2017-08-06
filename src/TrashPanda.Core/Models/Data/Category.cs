using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Models.Data
{
    public class Category
    {
        public int Id { get; set; }
        public List<PostCategory> PostCategories { get; set; }
    }
}
