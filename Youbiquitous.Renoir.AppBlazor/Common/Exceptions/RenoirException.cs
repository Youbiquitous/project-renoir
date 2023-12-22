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


namespace Youbiquitous.Renoir.AppBlazor.Common.Exceptions;

/// <summary>
/// App-specific class to throw and handle exceptions
/// </summary>
public class RenoirException : Exception
{
    public RenoirException(string message) 
        : base(message)
    {
    }
}
