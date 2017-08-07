using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Models.View
{
    public class LoginResultViewModel
    {
        public bool Succeeded { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
    }
}
