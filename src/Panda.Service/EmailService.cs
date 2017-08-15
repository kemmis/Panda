using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Panda.Core.Models.View;
using Panda.Core.Contracts;
using Panda.Core.Models.Request;
using Microsoft.AspNetCore.DataProtection;

namespace Panda.Service
{
    public class EmailService : IEmailService
    {
        private readonly IPandaDataProvider _dataProvider;
        private readonly IDataProtector _protector;

        public EmailService(IPandaDataProvider dataProvider, IDataProtectionProvider dataProtectionProvider)
        {
            _dataProvider = dataProvider;
            _protector = dataProtectionProvider.CreateProtector("Panda.BlogService");
        }

        public async Task<bool> SendTestEmail(SettingsViewModel settings, string userId)
        {
            var response = true;
            try
            {
                var user = _dataProvider.GetUserById(userId);

                using (var client = new SmtpClient
                {
                    Credentials = new NetworkCredential(settings.SmtpUsername, settings.SmtpPassword),
                    EnableSsl = settings.SmtpUseSsl,
                    Host = settings.SmtpHost,
                    Port = int.Parse(settings.SmtpPort),
                    Timeout = 10000
                })
                {

                    var sendMailTask = client.SendMailAsync(user.Email, user.Email, "Test message from Panda!",
                        "Congrats! Your email settings are working!");


                    if (await Task.WhenAny(sendMailTask, Task.Delay(5000)) == sendMailTask)
                    {
                        response = true;
                    }
                    else
                    {
                        // sendMailTask task timed out
                        client.SendAsyncCancel();
                        response = false;
                    }
                }
            }
            catch
            {
                response = false;
            }
            return response;
        }

        public async Task<bool> SendCommentEmail(CommentCreateRequest request)
        {
            var post = _dataProvider.GetPostById(request.PostId);
            var user = _dataProvider.GetUserById(post.UserId);
            var blog = _dataProvider.GetBlog();

            if (!string.IsNullOrWhiteSpace(blog.SmtpPassword))
            {
                blog.SmtpPassword = _protector.Unprotect(blog.SmtpPassword);
            }

            var response = true;
            try
            {
                using (var client = new SmtpClient
                {
                    Credentials = new NetworkCredential(blog.SmtpUsername, blog.SmtpPassword),
                    EnableSsl = blog.SmtpUseSsl,
                    Host = blog.SmtpHost,
                    Port = int.Parse(blog.SmtpPort),
                    Timeout = 10000
                })
                {
                    var sendMailTask = client.SendMailAsync(user.Email, user.Email,
                        $"{blog.EmailPrefix} Comment on post '{post.Title}'",
                        $"Comment by: {request.AuthorName} - {request.AuthorEmail}\n" +
                        $"Comment: {request.Text}");

                    if (await Task.WhenAny(sendMailTask, Task.Delay(5000)) == sendMailTask)
                    {
                        response = true;
                    }
                    else
                    {
                        // sendMailTask task timed out
                        client.SendAsyncCancel();
                        response = false;
                    }
                }
            }
            catch
            {
                response = false;
            }
            return response;
        }
    }
}
