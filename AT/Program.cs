using System.Net.Mime;
using AT.Domain;
using FriendsAPI;
using FriendsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<HttpResponseExceptionFilter>();
//})
//    .ConfigureApiBehaviorOptions(options =>
//    {
//        options.InvalidModelStateResponseFactory = context =>
//            new BadRequestObjectResult(context.ModelState)
//            {
//                ContentTypes =
//                {
//                    // using static System.Net.Mime.MediaTypeNames;
//                    MediaTypeNames.Application.Json,
//                    MediaTypeNames.Application.Xml
//                }
//            };
//    })
//    .AddXmlSerializerFormatters();
builder.Services.AddControllers();

builder.Services.AddCors(setupAction =>
{
    setupAction.AddPolicy("policy",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<MvcOptions>(c =>
    c.Conventions.Add(new SwaggerApplicationConvention()));

// Register generator and it's dependencies
builder.Services.AddTransient<ISwaggerProvider, SwaggerGenerator>();
builder.Services.AddTransient<ISchemaGenerator, SchemaGenerator>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FriendsAPI",
        Version = "v1"
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<ConnectionStrings>(
    builder.Configuration.GetSection(ConnectionStrings.Name));

builder.Services.AddScoped<IFriendsService, FriendsService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FriendsAPI");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerUI();

app.Run();
