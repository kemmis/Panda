using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrashPanda.Core.Models.Data;

namespace TrashPanda.Data.SqlServer.Seed
{
    public static class SeedExt
    {
        public static void EnsureSeeded(this TrashPandaDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any posts.
            if (context.Posts.Any())
            {
                return;   // DB has been seeded
            }

            var posts = new Post[]
            {
                new Post
                {
                    Slug="derp-derp-derp",
                    Content="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    PublishDate= DateTime.Now,
                    Title="Node Blows My Mind!"
                }
            };

            context.Posts.AddRange(posts);
        }
    }
}
