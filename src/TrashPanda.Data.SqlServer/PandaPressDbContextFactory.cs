using System;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace PandaPress.Data.SqlServer
{
    public class PandaPressDbContextFactory : IDbContextFactory
    {
        private readonly IServiceProvider serviceProvider;

        public PandaPressDbContextFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        TDbContext IDbContextFactory.CreateDbContext<TDbContext>()
        {
            return serviceProvider.GetService<TDbContext>();
        }
    }
}
