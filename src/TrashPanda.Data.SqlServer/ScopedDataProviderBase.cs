using EntityFramework.DbContextScope.Interfaces;

namespace PandaPress.Data.SqlServer
{
    public class ScopedDataProviderBase
    {
        private readonly ScopedDataProviderBaseDependencies baseDependencies;

        public ScopedDataProviderBase(ScopedDataProviderBaseDependencies baseDependencies)
        {
            this.baseDependencies = baseDependencies;
        }

        public PandaPressDbContext PandaPressDbContext => baseDependencies.dbContextLocator.Get<PandaPressDbContext>();

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
