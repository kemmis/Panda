using EntityFramework.DbContextScope.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrashPanda.Data.SqlServer
{
    public class ScopedDataProviderBase
    {
        private readonly ScopedDataProviderBaseDependencies baseDependencies;

        public ScopedDataProviderBase(ScopedDataProviderBaseDependencies baseDependencies)
        {
            this.baseDependencies = baseDependencies;
        }

        public TrashPandaDbContext TrashPandaDbContext => baseDependencies.dbContextLocator.Get<TrashPandaDbContext>();

        public IDbContextReadOnlyScope ReadOnlyScope => baseDependencies.dbContextScopeFactory.CreateReadOnly();

        public IDbContextScope Scope => baseDependencies.dbContextScopeFactory.Create();
    }

    public class ScopedDataProviderBaseDependencies
    {
        private readonly IAmbientDbContextLocator _dbContextLocator;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        public ScopedDataProviderBaseDependencies(IAmbientDbContextLocator dbContextLocatory, IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextLocator = dbContextLocatory;
            _dbContextScopeFactory = dbContextScopeFactory;
        }
        public IAmbientDbContextLocator dbContextLocator => _dbContextLocator;
        public IDbContextScopeFactory dbContextScopeFactory => _dbContextScopeFactory;
    }
}
