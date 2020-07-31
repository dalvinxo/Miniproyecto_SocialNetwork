using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Database.Models;
using Email;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Miniproyecto_SocialNetwork.Infrastructure;
using Repository.Repository;

namespace Miniproyecto_SocialNetwork
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //connectionstring
            services.AddDbContext<SocialNetworkContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Default")));

            ///automapeer
            //services.AddAutoMapper();
            services.AddAutoMapper(typeof(Mapping).GetTypeInfo().Assembly);

            //repository
            services.AddScoped<TablaUsuarioRepository>();
            services.AddScoped<TablaPublicacionRepository>();
            services.AddScoped<TablaComentarioRepository>();
            services.AddScoped<TablaAmigoRepository>();
            services.AddScoped<SubTablaComentarioRepository>();

            ///Identity
            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 3,
                    RequireUppercase = false,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false
                };
            }


            ).AddEntityFrameworkStores<SocialNetworkContext>().AddDefaultTokenProviders();



            //Email
            var emailConfig = Configuration.GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);

            services.AddScoped<IEmailSender, GmailSender>();



        }
      

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
