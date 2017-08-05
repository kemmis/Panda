using AutoMapper;
using EntityFramework.DbContextScope;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PandaPress.Core.Contracts;
using PandaPress.Core.Models.Data;
using PandaPress.Data.SqlServer;
using PandaPress.Data.SqlServer.Seed;
using PandaPress.Service;

namespace PandaPress.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutoMapper();

            #region EF / SqlServer

            services.AddDbContext<PandaPressDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            #endregion


            #region Identity

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<PandaPressDbContext>()
                .AddDefaultTokenProviders();

            #endregion

            #region register dependencies

            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPandaPressDataProvider, SqlServerPandaPressDataProvider>();
            services.AddTransient<IAmbientDbContextLocator, AmbientDbContextLocator>();
            services.AddTransient<IDbContextScopeFactory, DbContextScopeFactory>();
            services.AddTransient<ScopedDataProviderBaseDependencies>();
            services.AddTransient<IDbContextFactory, PandaPressDbContextFactory>();
            services.AddTransient<DbInitializer>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IPandaPressDataProvider pandaPressDataProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            pandaPressDataProvider.Init().Wait();
        }
    }
}
