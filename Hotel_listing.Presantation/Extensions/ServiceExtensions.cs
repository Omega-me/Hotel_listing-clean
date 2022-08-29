using FluentValidation.AspNetCore;
using Hotel_listing.Application.Configurations;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Application.Exceptions.ValidationResponseFilters;
using Hotel_listing.Infrastructure.DatabaseManager.Context;
using Hotel_listing.Infrastructure.RepositoryManager.Command;
using Hotel_listing.Infrastructure.RepositoryManager.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Hotel_listing.Presantation.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureRepository(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IQuery,Query>();
        serviceCollection.AddTransient<ICommands,Commands>();
    }
    public static void ConfigureCors(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(option => option.AddPolicy("CorsPolicy", policy =>
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
        ));
    }
    public static void ConfigureDbContext(this IServiceCollection serviceCollection,IConfiguration configuration)
    {
        serviceCollection.AddDbContext<DatabaseContext>(
            options=>options.UseNpgsql(
                configuration.GetConnectionString("sqlConnectionPsql")
            )
        );
    }
    public static void ConfigureSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(c =>
        {
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