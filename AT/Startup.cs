using System.Net.Mime;
using AT.Domain;
using CountriesApi.Services;
using FriendsAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace CountriesApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                    options.Filters.Add<HttpResponseExceptionFilter>();
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                        new BadRequestObjectResult(context.ModelState)
                        {
                            ContentTypes =
                            {
                                // using static System.Net.Mime.MediaTypeNames;
                                MediaTypeNames.Application.Json,
                                MediaTypeNames.Application.Xml
                            }
                        };
                })
                .AddXmlSerializerFormatters();

            services.AddCors(setupAction =>
            {
                setupAction.AddPolicy("countries-policy",
                    builder =>
                    {
                        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                    });
            });

            services.AddEndpointsApiExplorer();
            
            services.ConfigureSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CountriesAPI",
                    Version = "v1"
                });
            });
            services.AddSwaggerGen();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IFriendsService, FriendsService>();

            //AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.Configure<ConnectionStrings>(
            //    Configuration.GetSection(ConnectionStrings.Name));

            //Configure Swagger
            services.ConfigureSwaggerGen(c =>
            {
                c.SwaggerDoc("v3", new OpenApiInfo
                {
                    Title = "GTrackAPI",
                    Version = "v3"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseExceptionHandler("/error-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseCors("policy");

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CountriesAPI");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();
        }
    }
}
