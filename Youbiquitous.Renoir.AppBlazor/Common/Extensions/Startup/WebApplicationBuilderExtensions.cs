///////////////////////////////////////////////////////////////////
//
// Project RENOIR
// Release Notes Instant Reporter
//
// Reference application presented in
// Clean Architecture with .NET (MS Press) 2024
// Author: Dino Esposito
// 
//


using Microsoft.AspNetCore.Authentication.Cookies;

namespace Youbiquitous.Renoir.AppBlazor.Common.Extensions.Startup;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Contains the code to set up the configuration tree for application settings
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    /// <param name="fileName">JSON extension assumed</param>
    /// <param name="devExtension"></param>
    /// <returns></returns>
    public static WebApplicationBuilder SetupSettings<T>(this WebApplicationBuilder builder,
        string fileName = "app-settings", string devExtension = "-dev")
        where T : class, new()
    {
        var env = builder.Environment;
        var settingsFileName = env.IsDevelopment()
            ? $"{fileName}{devExtension}.json"
            : $"{fileName}.json";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile(settingsFileName, optional: true)
            .AddEnvironmentVariables()
            .Build();

        var settings = new T();
        configuration.Bind(settings);
        builder.Services.AddSingleton(settings);

        return builder;
    }

    /// <summary>
    /// Contains the code to add and configure the cookie-based authentication service 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="loginPath"></param>
    /// <param name="cookieName"></param>
    /// <param name="minutesBeforeExpiration"></param>
    /// <returns></returns>
    public static WebApplicationBuilder SetupCookieAuthentication(this WebApplicationBuilder builder,
        string loginPath, string cookieName, int minutesBeforeExpiration = 60)
    {
        // Authentication setup
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = cookieName;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(minutesBeforeExpiration);
                options.LoginPath = new PathString(loginPath);
                options.SlidingExpiration = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

        return builder;
    }

    /// <summary>
    /// Contains the code to add and configure loggers
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder SetupLoggers(this WebApplicationBuilder builder)
    {
        var env = builder.Environment;

        // Clear out default loggers added by the default web host builder (program.cs)
        builder.Services.AddLogging(config =>
        {
            config.ClearProviders();
            if (env.IsDevelopment())
            {
                config.AddDebug();
                config.AddConsole();
            }
        });

        return builder;
    }


    /// <summary>
    /// Contains the code to add further miscellaneous runtime services
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder SetupAdditionalServices(this WebApplicationBuilder builder)
    {
        // HTTP accessor
        builder.Services.AddHttpContextAccessor();

        return builder;
    }

    /// <summary>
    /// Contains the code to set up Blazor runtime services  
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder SetupBlazor(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        return builder;
    }

    /// <summary>
    /// Contains the code to setup MVC controllers (at least for login)
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder SetupMvc(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();  
        return builder;
    }

    /// <summary>
    /// Contains the code to enable Havit UI components
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder SetupBootstrap(this WebApplicationBuilder builder)
    {
        // For Havit UI components
        builder.Services.AddBlazorBootstrap(); 
        
        return builder;
    }
}