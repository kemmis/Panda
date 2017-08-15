using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Panda.Core.Models.Request;
using Panda.Core.Models.View;

namespace Panda.Core.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendTestEmail(SettingsViewModel settings, string userId);
        Task<bool> SendCommentEmail(CommentCreateRequest request);
    }
}
