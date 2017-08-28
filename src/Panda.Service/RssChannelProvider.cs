using cloudscribe.Syndication.Models.Rss;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Panda.Core.Contracts;
using Panda.Core.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Http;
using cloudscribe.SimpleContent.Models;
using System.Linq;

namespace Panda.Service
{
    public class RssChannelProvider : IChannelProvider
    {
        private readonly IBlogService _blogService;
        private readonly IActionContextAccessor _actionContextAccesor;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHtmlProcessor _htmlProcessor;

        public RssChannelProvider(IBlogService blogService,
            IActionContextAccessor actionContextAccesor,
            IHttpContextAccessor contextAccessor,
            IUrlHelperFactory urlHelperFactory,
            IHtmlProcessor htmlProcessor)
        {
            _blogService = blogService;
            _actionContextAccesor = actionContextAccesor;
            _urlHelperFactory = urlHelperFactory;
            _contextAccessor = contextAccessor;
            _htmlProcessor = htmlProcessor;
        }

        public string Name => "Panda.Service.RssChannelProvider";
        private int maxFeedItems = 20;

        public async Task<RssChannel> GetChannel(CancellationToken cancellationToken = default(CancellationToken))
        {
            var blog = _blogService.GetBlogSettings();
            var posts = _blogService.GetPostList(new PostListRequest
            {
                PageIndex = 0,
                PageSize = maxFeedItems
            }).Posts;

            var categories = _blogService.GetCategories();

            var channel = new RssChannel();
            channel.Title = blog.BlogName;
            channel.Description = blog.Description;
            channel.Generator = Name;

            foreach (var cat in categories)
            {
                channel.Categories.Add(new RssCategory(cat.Title));
            }

            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccesor.ActionContext);
            var baseUrl = string.Concat(
                       _contextAccessor.HttpContext.Request.Scheme,
                       "://",
                       _contextAccessor.HttpContext.Request.Host.ToUriComponent()
                       );

            var indexUrl = urlHelper.RouteUrl("spa-fallback");
            if (indexUrl.StartsWith("/"))
            {
                indexUrl = string.Concat(baseUrl, indexUrl);
            }
            channel.Link = new Uri(indexUrl);

            channel.TimeToLive = 60;

            var items = new List<RssItem>();
            foreach (var post in posts)
            {
                var rssItem = new RssItem();
                rssItem.Author = post.UserDisplayName;

                foreach (var c in post.Categories)
                {
                    rssItem.Categories.Add(new RssCategory(c.Title));
                }

                rssItem.Description = _htmlProcessor.ConvertUrlsToAbsolute(baseUrl, post.Content);

                var postUrl = $"{baseUrl}/post/{post.Slug}";
                rssItem.Link = new Uri(postUrl);

                rssItem.Guid = new RssGuid(postUrl);
                rssItem.PublicationDate = DateTime.Parse(post.PublishDate);
                rssItem.Title = post.Title;
                items.Add(rssItem);
            }

            channel.PublicationDate = DateTime.Parse(posts.First().PublishDate);
            channel.Items = items;

            return channel;
        }
    }
}
