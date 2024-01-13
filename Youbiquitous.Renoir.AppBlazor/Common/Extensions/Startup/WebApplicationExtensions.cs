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


using Youbiquitous.Renoir.AppBlazor.Components;
using Youbiquitous.Renoir.Application.Settings;
using Youbiquitous.Renoir.Application.System;

namespace Youbiquitous.Renoir.AppBlazor.Common.Extensions.Startup;

public static class WebApplicationExtensions
{
    /// <summary>
    /// Contains the code to configure error handling
    /// </summary>
    /// <param name="app"></param>
    /// <param name="errorUrl"></param>
    /// <param name="exceptionMode"></param>
    /// <returns></returns>
    public static WebApplication SetupErrorHandling(this WebApplication app,
        string errorUrl, ExceptionHandlingMode exceptionMode = ExceptionHandlingMode.Auto)
    {
        var isDevelopment = app.Environment.IsDevelopment();

        // Global error handler
        if (exceptionMode.IsAuto() && isDevelopment || exceptionMode.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
            app.UseExceptionHandler(errorUrl);

        // Handle 404 not-found errors
        app.Use(async (context, next) =>
        {
            await next();       // let it go
            if (context.Response.StatusCode == 404)
            {
                context.Request.Path = errorUrl;
                await next();
            }
        });

        return app;
    }

    /// <summary>
    /// Contains configuration for cookie policy and other HTTP security-related aspects
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication SetupSecurity(this WebApplication app)
    {
        app.UseCookiePolicy();
        app.UseHsts();
        app.UseHttpsRedirection();

        // Append security headers
        app.Use(async (context, next) =>
        {
            if (!context.Response.Headers.ContainsKey("X-Frame-Options"))
                context.Response.Headers.Append("X-Frame-Options", "DENY");
            if (!context.Response.Headers.ContainsKey("X-Xss-Protection"))
                context.Response.Headers.Append("X-Xss-Protection", "1; mode=block");
            if (!context.Response.Headers.ContainsKey("Referrer-Policy"))
                context.Response.Headers.Append("Referrer-Policy", "no-referrer");

            await next();
        });

        return app;
    }

    /// <summary>
    /// Contains the code to set static files and basic routing
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication SetupRouting(this WebApplication app)
    {
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAntiforgery();

        return app;
    }

    /// <summary>
    /// Contains the code to configure Blazor routing services
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication SetupBlazorRouting<T>(this WebApplication app)
    {
        app.MapRazorComponents<T>()
            .AddInteractiveServerRenderMode();

        return app;
    }

    /// <summary>
    /// Contains the code to recognize controller methods
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication SetupControllerEndpoints(this WebApplication app)
    {
#pragma warning disable ASP0014 // Suggest using top level route registrations
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=home}/{action=index}/{id?}");
        });
#pragma warning restore ASP0014 // Suggest using top level route registrations

        return app;
    }

    /// <summary>
    /// Contains the code 
    /// </summary>
    /// <param name="app"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static WebApplication SetupDatabase(this WebApplication app)
    {
        var settings = app.Services.GetService<RenoirSettings>();
        if (settings == null)
            return app;

        SystemService.ConfigureDatabases(settings);
        return app;
    }
}
