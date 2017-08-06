using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using cloudscribe.MetaWeblog;

namespace PandaPress.Service
{
    public class MetaWeblogSecurity : IMetaWeblogSecurity
    {
        public Task<MetaWeblogSecurityResult> ValiatePermissions(MetaWeblogRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
    