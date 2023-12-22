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

namespace Youbiquitous.Renoir.AppBlazor.Common.Extensions.Startup;


public enum ExceptionHandlingMode
{
    Auto = 0,
    Development = 1,
    Production = 2
}

/// <summary>
/// Ad hoc extension methods
/// </summary>
public static class ExceptionHandlingModeExtensions
{
    public static bool IsAuto(this ExceptionHandlingMode mode)
    {
        return mode == ExceptionHandlingMode.Auto;
    }
    public static bool IsDevelopment(this ExceptionHandlingMode mode)
    {
        return mode == ExceptionHandlingMode.Development;
    }
    public static bool IsProduction(this ExceptionHandlingMode mode)
    {
        return mode == ExceptionHandlingMode.Production;
    }
    public static bool Is(this ExceptionHandlingMode mode, ExceptionHandlingMode other)
    {
        return mode == other;
    }
}