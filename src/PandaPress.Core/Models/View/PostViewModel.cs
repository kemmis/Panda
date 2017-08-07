﻿using System;

namespace PandaPress.Core.Models.View
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string Slug { get; set; }
        public string UserDisplayName { get; set; }
        public bool Published { get; set; }
    }
}