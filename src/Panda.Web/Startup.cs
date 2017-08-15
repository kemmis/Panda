using AutoMapper;
using cloudscribe.MetaWeblog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Panda.Core.Contracts;
using Panda.Core.Models.Data;
using Panda.Data.SqlServer;
using Panda.Data.SqlServer.Seed;
using Panda.Service;

namespace Panda.Web
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
            services.AddDataProtection();

            #region EF / SqlServer

            services.AddDbContext<PandaDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            #endregion


            #region Identity

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<PandaDbContext>()
                .AddDefaultTokenProviders();

            #endregion

            #region register dependencies

            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IPandaDataProvider, SqlServerPandaDataProvider>();
            services.AddTransient<DbInitializer>();
            services.AddTransient<IMediaStorageService, FileSystemMediaStorageService>();
            services.AddTransient<ISlugService, SlugService>();
            services.AddTransient<IEmailService, EmailService>();

            #region MetaWeblog dependencies

            services.AddCloudscribeMetaWeblog();
            services.AddTransient<IMetaWeblogService, MetaWeblogService>();
            services.AddTransient<IMetaWeblogSecurity, MetaWeblogSecurity>();

            #endregion

            #endregion

            // uncomment for node debugging
            //services.AddNodeServices(options => {
            //    options.LaunchWithDebugging = true;
            //    options.DebuggingPort = 9229;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IPandaDataProvider pandaPressDataProvider)
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
            app.UseIdentity();

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
