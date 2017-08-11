using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Models.View
{
    public class SettingsViewModel
    {
        public string BlogName { get; set; }
        public int BlogId { get; set; }
        public string Description { get; set; }
        public int PostsPerPage { get; set; }
    }
}
