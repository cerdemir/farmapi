﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace farmapi
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
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(Filters.ModelValidateActionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            services.AddLogging();

            var connectionString = Environment.GetEnvironmentVariable("STORE_DB_CONNECTION_STRING") ??
                Configuration.GetConnectionString("STORE_DB_CONNECTION_STRING");

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<Context.FarmApiContext>(opt => opt.UseNpgsql(connectionString));

            ConfigureJWT(services);

            ConfigureSwagger(services);

            services.AddScoped<Services.IUserService, Services.UserService>();
            services.AddScoped<Services.IProductService, Services.ProductService>();
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            var apiDescription = Swagger.DescriptionLoader.LoadApiDescription();
            services.AddMemoryCache();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                        Title = "farma API",
                        Description = apiDescription
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                { { "Bearer", new string[] { } }
                });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "Authorization header using the Bearer scheme.",
                        Name = "Authorization",
                        In = "header",
                        Type = "apiKey",
                });

                var basePath = AppContext.BaseDirectory;;
                var xmlPath = System.IO.Path.Combine(basePath, "farmapi.xml");
                if (System.IO.File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });
        }

        private void ConfigureJWT(IServiceCollection services)
        {
            var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY") ??
                Configuration.GetConnectionString("SECRET_KEY");
            var key = Encoding.ASCII.GetBytes(secretKey);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            using(var sScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = sScope.ServiceProvider.GetRequiredService<Context.FarmApiContext>();
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "farma API");
                });

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}