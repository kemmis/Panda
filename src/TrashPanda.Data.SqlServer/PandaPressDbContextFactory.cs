using EntityFramework.DbContextScope.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrashPanda.Data.SqlServer
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
