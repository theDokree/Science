using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using AutoMapper;
using App.Library.Services.Email;
using App.Library.Database.Context;
using App.Library.Entity.DAO;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Sakura.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Library.Services.Users;
using App.Library.Database.Initializers;

namespace App.Web
{
    public class Startup
    {
        public static bool EnableInitializer { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<AppIdentityDbContext>((options) =>
            {
                options.UseNpgsql(Configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly("App.Library.Database"));
            })
              .UseDbInitializer<IdentityDbInitializer>();
            //var conn = Configuration["ConnectionStrings:DefaultConnection"];

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;
                
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            //services.AddAuthentication()
            //    .AddRosgvard(options =>
            //{
            //    options.ClientId = "5";
            //    options.ClientSecret = "qwerty";
            //});

            //services.AddAuthentication()
            //     .AddRosgvard(options =>
            // {
            //     options.ClientId = "33";
            //     options.ClientSecret = "Server2008";
            // });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.ExpireTimeSpan = TimeSpan.FromDays(150);
               options.LoginPath = "/Account/Login";
               options.LogoutPath = "/Account/Logout";
           });



            services.AddTransient<IEmailSender, EmailSender>();

            //services.AddDbContext<MissionsDbContext>((options) =>
            //{
            //    options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnectionString"], b => b.MigrationsAssembly("App.Library.Database"));
            //});

            
            //services.AddScoped<IRssNewsService, RssNewsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddMvc();

            services.AddBootstrapPagerGenerator(options =>
            {
                // Use default pager options.
                options.ConfigureDefault();

                options.ExpandPageItemsForCurrentPage = 2; // Will display more 2 pager buttons before and after current page.
                options.PagerItemsForEndings = 1; // Will display 2 pager buttons for first and last pages.
                options.Layout = PagerLayouts.Default; // Layout controls which elements will be displayed in the pager. For more information, please read the documentation.

            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            InitializeApplication(provider);
        }

        private void InitializeApplication(IServiceProvider provider)
        {
            if (EnableInitializer)
            {
                provider.GetRequiredService<IdentityDbInitializer>().InitializeDatabase().Wait();
            }
            else
            {
                Console.WriteLine("Application not launched. Initialization skipped");
            }
        }

    }
}
