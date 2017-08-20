using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Panda.Core.Contracts;
using Panda.Core.Models.Data;
using Panda.Data.SqlServer.Seed;

namespace Panda.Data.SqlServer
{
    public class SqlServerPandaDataProvider : IPandaDataProvider
    {
        private readonly PandaDbContext _db;
        private readonly DbInitializer _dbInitializer;

        public SqlServerPandaDataProvider(PandaDbContext pandaPressDbContext, DbInitializer dbInitializer)
        {
            _db = pandaPressDbContext;
            _dbInitializer = dbInitializer;
        }

        public IEnumerable<Blog> GetBlogsForUser(string username)
        {
            return _db.Blogs.Where(b =>
                b.BlogApplicationUsers.Any(ba => String.Equals(ba.ApplicationUser.UserName, username, StringComparison.CurrentCultureIgnoreCase)));
        }

        public IEnumerable<Category> GetCategories()
        {
            return _db.Categories;
        }

        public Post GetPostBySlug(string slug)
        {
            return _db.Posts.Where(p => !p.Deleted)
                .Include(p => p.User)
                .Include(p => p.Comments)
                .Include(p => p.PostCategories).ThenInclude(pc => pc.Category)
                .FirstOrDefault(p => p.Slug == slug);
        }

        public Post GetPostById(int postId)
        {
            return _db.Posts.Where(p => !p.Deleted)
                .Include(p => p.User).Include(p => p.PostCategories).ThenInclude(pc => pc.Category)
                .FirstOrDefault(p => p.Id == postId);
        }

        public (IEnumerable<Post> posts, int totalPosts) GetPosts(int pageSize, int pageIndex)
        {
            var totalPosts = _db.Posts.Where(p => !p.Deleted).Count(p => p.Published);

            var posts = _db.Posts.Where(p => !p.Deleted && p.Published)
                .Include(p => p.User)
                .Include(p => p.Comments)
                .Include(p => p.PostCategories).ThenInclude(pc => pc.Category)
                .OrderByDescending(p => p.PublishDate).Skip(pageIndex * pageSize)
                .Take(pageSize).ToList();
            return (posts, totalPosts);
        }

        public (IEnumerable<Post> posts, int totalPosts) GetPostsByCategorySlug(int pageSize, int pageIndex, string slug)
        {
            var query = _db.Posts.Where(p => !p.Deleted && p.Published)
                .Where(p => p.PostCategories.Any(pc => pc.Category.Slug.ToLower() == slug));

            var totalPosts = query.Count();
            var posts = query.Include(p => p.User)
                .Include(p => p.Comments)
                .Include(p => p.PostCategories).ThenInclude(pc => pc.Category)
                .OrderByDescending(p => p.PublishDate).Skip(pageIndex * pageSize)
                .Take(pageSize).ToList();
            return (posts, totalPosts);
        }

        public Post CreatePost(string title, string content, List<string> categories, string slug, string username, bool publish, int blogId)
        {
            var user = _db.Users.FirstOrDefault(u => String.Equals(u.UserName, username, StringComparison.CurrentCultureIgnoreCase));
            var blog = _db.Blogs.FirstOrDefault(b => b.Id == blogId);

            var post = new Post
            {
                Blog = blog,
                User = user,
                Title = title,
                Content = content,
                Published = publish,
                PublishDate = DateTime.Now,
                Slug = slug
            };

            _db.Posts.Add(post);

            foreach (var category in categories)
            {
                var catToAdd = _db.Categories.FirstOrDefault(c => String.Equals(c.Title, category, StringComparison.CurrentCultureIgnoreCase));
                if (catToAdd != null)
                {
                    post.PostCategories.Add(new PostCategory
                    {
                        Category = catToAdd,
                        Post = post
                    });
                }
            }

            _db.SaveChanges();

            return post;
        }

        public void UpdatePost(int postId, string title, string content, List<string> categories, bool publish)
        {
            var post = _db.Posts.Include(p => p.PostCategories).ThenInclude(pc => pc.Category).FirstOrDefault(p => p.Id == postId);
            if (post != null)
            {
                post.Title = title;
                post.Content = content;
                post.Published = publish;

                var catsToRemove = post.PostCategories.Where(pc =>
                    !categories.Exists(c =>
                        String.Equals(c, pc.Category.Title, StringComparison.CurrentCultureIgnoreCase))).ToList();
                var catsToAdd = categories.Where(c => post.PostCategories.All(pc =>
                    !String.Equals(pc.Category.Title, c, StringComparison.CurrentCultureIgnoreCase))).ToList();

                foreach (var c in catsToRemove)
                {
                    post.PostCategories.Remove(c);
                }

                foreach (var c in catsToAdd)
                {
                    var catToAdd = _db.Categories.FirstOrDefault(c2 => String.Equals(c2.Title, c, StringComparison.CurrentCultureIgnoreCase));
                    if (catToAdd != null)
                    {
                        post.PostCategories.Add(new PostCategory
                        {
                            Category = catToAdd,
                            Post = post
                        });
                    }
                }

                _db.SaveChanges();
            }
        }
        public async Task Init()
        {
            await _db.Database.MigrateAsync(); // run migrations

            await _dbInitializer.EnsureSeededAsync();
        }

        public Blog GetBlog()
        {
            return _db.Blogs.FirstOrDefault();
        }

        public Blog UpdateBlog(int blogId, string blogName, string description, int postsPerPage, string smtpUsername,
            string smtpPassword, string smtpHost, string smtpPort, string emailPrefix, bool smtpUseSsl, bool sendCommentEmail)
        {
            var blog = _db.Blogs.FirstOrDefault(b => b.Id == blogId);
            if (blog != null)
            {
                blog.Name = blogName;
                blog.Description = description;
                blog.PostsPerPage = postsPerPage;
                blog.SmtpUsername = smtpUsername;
                blog.SmtpPassword = smtpPassword;
                blog.SmtpHost = smtpHost;
                blog.SmtpPort = smtpPort;
                blog.EmailPrefix = emailPrefix;
                blog.SmtpUseSsl = smtpUseSsl;
                blog.SendCommentEmail = sendCommentEmail;

                _db.SaveChanges();
            }
            return blog;
        }

        public int GetNumPublishedPosts()
        {
            var blog = _db.Blogs.Include(b => b.Posts).FirstOrDefault();
            if (blog != null)
            {
                return blog.Posts.Where(p => !p.Deleted).Count(p => p.Published);
            }
            return 0;
        }

        public int GetNumDrafts()
        {
            var blog = _db.Blogs.Include(b => b.Posts).FirstOrDefault();
            if (blog != null)
            {
                return blog.Posts.Where(p => !p.Deleted).Count(p => !p.Published);
            }
            return 0;
        }

        public List<Comment> GetRecentComments()
        {
            return _db.Comments.OrderByDescending(c => c.CreatedDateTime)
                .Take(10).Include(c => c.Post).ToList();
        }

        public IEnumerable<Post> GetPosts()
        {
            return _db.Posts.Where(p => !p.Deleted).ToList();
        }

        public IEnumerable<Category> GetCategoriesWithPostCategories()
        {
            return _db.Categories.Include(c => c.PostCategories).ToList();
        }

        public Category AddCategory(string title, string description, string slug)
        {
            var newCategory = new Category
            {
                Title = title,
                Description = description,
                Slug = slug
            };
            _db.Categories.Add(newCategory);
            _db.SaveChanges();
            return newCategory;
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == categoryId);
            _db.Categories.Remove(category);
            _db.SaveChanges();
        }

        public ApplicationUser GetUserById(string userId)
        {
            return _db.Users.FirstOrDefault(u => u.Id == userId);
        }

        public ApplicationUser UpdateUser(string userId, string displayName, string about, string email)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.DisplayName = displayName;
                user.About = about;
                user.Email = email;
                _db.SaveChanges();
            }
            return user;
        }

        public Comment CreateComment(int postId, string authorName, string authorEmail, string text, string gravatarHash)
        {
            var post = _db.Posts.FirstOrDefault(p => p.Id == postId);
            if (post != null)
            {
                var comment = new Comment()
                {
                    Post = post,
                    AuthorEmail = authorEmail,
                    AuthorName = authorName,
                    CreatedDateTime = DateTime.UtcNow,
                    Removed = false,
                    Text = text,
                    Gravatar = gravatarHash
                };
                _db.Comments.Add(comment);
                _db.SaveChanges();
                return comment;
            }
            return null;
        }

        public void DeleteComment(int commentId)
        {
            var commentToDelete = _db.Comments.FirstOrDefault(c => c.Id == commentId);
            if (commentToDelete != null)
            {
                commentToDelete.Deleted = true;
                _db.SaveChanges();
            }
        }

        public void UnDeleteComment(int commentId)
        {
            var commentToUnDelete = _db.Comments.FirstOrDefault(c => c.Id == commentId);
            if (commentToUnDelete != null)
            {
                commentToUnDelete.Deleted = false;
                _db.SaveChanges();
            }
        }

        public Comment GetCommentById(int commentId)
        {
            return _db.Comments.FirstOrDefault(c => c.Id == commentId);
        }

        public void DeletePost(int postId)
        {
            var post = _db.Posts.FirstOrDefault(p => p.Id == postId);
            if (post != null)
            {
                post.Deleted = true;
                _db.SaveChanges();
            }
        }

        public void UnDeletePost(int postId)
        {
            var post = _db.Posts.FirstOrDefault(p => p.Id == postId);
            if (post != null)
            {
                post.Deleted = false;
                _db.SaveChanges();
            }
        }

        public Category GetCategoryBySlug(string slug)
        {
            return _db.Categories.FirstOrDefault(c =>
                String.Equals(c.Slug, slug, StringComparison.CurrentCultureIgnoreCase));
        }

        public void SaveProfilePicture(string userId, string profilePicture)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                user.ProfilePicture = profilePicture;
                _db.SaveChanges();
            }
        }

        public void RemoveProfilePhoto(string userId)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                user.ProfilePicture = "";
                _db.SaveChanges();
            }
        }
    }
}

