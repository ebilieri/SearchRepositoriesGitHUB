﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchRepositoriesGitHUB.Repositories;
using SearchRepositoriesGitHUB.Services;
using SearchRepositoriesGitHUB.WebApp.ServiceApi;

namespace SearchRepositoriesGitHUB.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appstting.json", optional: false, reloadOnChange: true)
                .AddJsonFile("config.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // String Connection
            var sqlConnection = Configuration.GetConnectionString("DefaultConnection");

            // Context
            services.AddDbContext<SearchRepositoriesGitHUBDBContext>(options =>
                options.UseSqlServer(sqlConnection));

            // Repositoties
            services.AddScoped<IItemsRepository, ItemsRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<ILicenseRepository, LicenseRepository>();

            //Services
            services.AddScoped<IItemsService, ItemsService>();

            services.AddScoped<IGitHUBApi, GitHUBApi>();
            services.AddSingleton<IConfiguration>(Configuration);


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Search}/{action=Index}/{id?}");
            });
        }
    }
}