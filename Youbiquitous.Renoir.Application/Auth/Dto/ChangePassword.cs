//////////////////////////////////////////////////////////////////////////
//
// Building Enterprise ASP.NET Core 6 Blazor Applications
// Source code
//
// Dino Esposito 
// 2023
// 

using Youbiquitous.Martlet.Core.Extensions;

namespace Youbiquitous.Renoir.Application.Auth.Dto;

/// <summary>
/// Wrapper for password change information
/// </summary>
public class ChangePassword
{
    /// <summary>
    /// Old password to change
    /// </summary>
    public string CurrentPassword { get; set; }

    /// <summary>
    /// New password to set
    /// </summary>
    public string NewPassword { get; set; }

    /// <summary>
    /// Repeated new password
    /// </summary>
    public string NewPasswordRepeat { get; set; }

    /// <summary>
    /// Whether the request for a new password can be submitted to the backend
    /// </summary>
    /// <returns></returns>
    public bool ShouldIgnoreRequest()
    {
        // Ignore request if current password is not set
        if (CurrentPassword.IsNullOrWhitespace())
            return true; 

        // New password fields must be equal and non-empty (and valid passwords according to rules)
        if (NewPassword.IsNullOrWhitespace() || NewPasswordRepeat.IsNullOrWhitespace())
            return true;

        return NewPassword != NewPasswordRepeat;
    }
}
