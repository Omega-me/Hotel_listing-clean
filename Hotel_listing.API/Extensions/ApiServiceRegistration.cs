using FluentValidation.AspNetCore;
using Hotel_listing.Application.Common;
using Hotel_listing.Application.Exceptions.ValidationResponseFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Hotel_listing.API;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(option => option.AddPolicy("CorsPolicy", policy =>
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
        ));
    }
    public static void ConfigureSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "Hotel Listing", Version = "v1",Contact = new OpenApiContact()
            {
                Name = "Olken",
                Email = "olkenmerxira@gmail.com"
            }});
        });
    }
    public static void ConfigureControllers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddControllers(o =>
            {
                o.Filters.Add(typeof(BaseModelStateFilter));
            })
            .AddFluentValidation(v =>
            {
                v.RegisterValidatorsFromAssemblyContaining<RegisterValidator>();
            })
            .AddNewtonsoftJson(options => 
                options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);
    }
    public static void ConfigureApiBehavior(this IServiceCollection serviceCollection)
    {
        serviceCollection.Configure<ApiBehaviorOptions>(
            o=>o.SuppressModelStateInvalidFilter=true
        );
    }
}