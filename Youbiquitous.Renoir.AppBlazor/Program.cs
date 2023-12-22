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
            .SetupCookieAuthentication("/account/login", RenoirSettings.AuthCookieName)
            .SetupAdditionalServices()
            .SetupLoggers()
            .SetupBlazor()
            .SetupMvc()
            .SetupBootstrap();

        // Build the app instance
        var app = builder
            .Build()
            .SetupErrorHandling("/app/error")
            .SetupSecurity()
            .SetupDatabase();

        // Auth stuff
        app.UseAuthentication();
        app.UseAuthorization();

        // Blazor engine setup
        app.SetupRouting()
            .SetupControllerEndpoints()
            .SetupBlazorRouting<Components.App>();

        app.Run();
    }
}