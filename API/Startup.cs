using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.AutoMapper;
using AutoMapper;
using Database.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repository.Repository;

namespace API
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
            services.AddControllers();

            //connectionstring
            services.AddDbContext<SocialNetworkContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Default")));


            //services.AddAutoMapper();
            services.AddAutoMapper(typeof(ConfigurationProfile).GetTypeInfo().Assembly);

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


            //Repository
            services.AddScoped<UsuarioRepositoryAPI>();
            services.AddScoped<TablaUsuarioRepository>();
            services.AddScoped<TablaPublicacionRepository>();
            services.AddScoped<TablaComentarioRepository>();
            services.AddScoped<TablaAmigoRepository>();
            services.AddScoped<SubTablaComentarioRepository>();


            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();


                app.UseSwagger();

                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    c.RoutePrefix = String.Empty;

                });


            }

          


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
