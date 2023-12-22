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


using Youbiquitous.Martlet.Core.Extensions;

namespace Youbiquitous.Renoir.Application.Auth.Dto;

public class AuthenticationRequest
{
    /// <summary>
    /// Username/Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Clear-text password
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Whether to stay connected for longer time
    /// </summary>
    public bool StayConnected { get; set; }

    /// <summary>
    /// Optional return URL to track
    /// </summary>
    public string ReturnUrl { get; set; }



    /// <summary>
    /// Whether submitted authentication request is acceptable (ie, non nulls)
    /// </summary>
    /// <returns></returns>
    public bool IsValid()
    {
        return !HasInvalidEmail() &&
               !HasInvalidPassword();
    }

    /// <summary>
    /// Whether the email is valid
    /// </summary>
    /// <returns></returns>
    public bool HasInvalidEmail()
    {
        // Check it is an email
        var isEmail = Email.IsValidEmail();
        return string.IsNullOrWhiteSpace(Email) || !isEmail;
    }

    /// <summary>
    /// Whether the password is valid
    /// </summary>
    /// <returns></returns>
    public bool HasInvalidPassword()
    {
        // Check the (expected) level of security of the password
        // ...

        return string.IsNullOrWhiteSpace(Password);
    }
}
