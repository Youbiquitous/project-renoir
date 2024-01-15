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

using Youbiquitous.Martlet.Core.Types;

namespace Youbiquitous.Renoir.AppBlazor.Models;

/// <summary>
/// App-specific base class for modeling custom pages
/// </summary>
public class DtoBase  
{
    /// <summary>
    /// Whether the object is in valid state
    /// </summary>
    /// <returns></returns>
    public virtual bool IsValid()
    {
        return true;
    }

    /// <summary>
    /// Detailed information about the state of the class
    /// </summary>
    /// <returns></returns>
    public virtual CommandResponse Validate()
    {
        return CommandResponse.Ok();
    }
}
