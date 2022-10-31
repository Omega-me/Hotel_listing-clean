using AspNetCoreRateLimit;
using FluentValidation.AspNetCore;
using Hotel_listing.Application.Common;
using Hotel_listing.Application.Exceptions.ValidationResponseFilters;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

namespace Hotel_listing.API.Extensions;

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
        serviceCollection.AddControllers(config =>
            {
                config.Filters.Add(typeof(BaseModelStateFilter));
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.CacheProfiles.Add("120SecondsDuration", new CacheProfile { Duration =
                    120 });
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
    public static void ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
        });
    }
    public static void ConfigureRateLimitingOptions(this IServiceCollection services)
    {
        var rateLimitRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "*",
                Limit = 100,
                Period = "1m"
            }
        };
        services.Configure<IpRateLimitOptions>(opt => { opt.GeneralRules =
            rateLimitRules; });
        services.AddSingleton<IRateLimitCounterStore,
            MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
    }
    public static void ConfigureResponseCaching(this IServiceCollection services) => services.AddResponseCaching();
    public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
    {
        services.AddHttpCacheHeaders((expirationOpt) =>
            {
                expirationOpt.MaxAge = 60;
                expirationOpt.CacheLocation = CacheLocation.Private;
            },
            (validationOpt) =>
            {
                validationOpt.MustRevalidate = true;
            });
    } 
}