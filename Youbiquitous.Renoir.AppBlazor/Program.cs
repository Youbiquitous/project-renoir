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

using Youbiquitous.Renoir.AppBlazor.Common.Extensions.Startup;
using Youbiquitous.Renoir.Application.Settings;

namespace Youbiquitous.Renoir.AppBlazor;


public class Program
{
    /// <summary>
    /// Root entry point in the web app
    /// </summary>
    /// <param name="args">CLI arguments (if any)</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication
            .CreateBuilder(args)
            .SetupSettings<RenoirSettings>()
            .SetupCookieAuthentication("/login", RenoirSettings.AuthCookieName)
            .SetupAdditionalServices()
            .SetupLoggers()
            .SetupBlazor()
            .SetupMvc()
            .SetupBootstrap();

        builder.WebHost.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");

        // Build the app instance
        var app = builder
            .Build()
            .SetupErrorHandling("/error", ExceptionHandlingMode.Production)
            .SetupSecurity()
            .SetupDatabase();

        // Auth stuff
        app.UseAuthentication();


        // Blazor engine setup
        app.SetupRouting()      // and authorization
            .SetupControllerEndpoints()
            .SetupBlazorRouting<Components.App>();

        app.Run();
    }
}