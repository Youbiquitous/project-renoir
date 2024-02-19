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


namespace Youbiquitous.Renoir.Application.Auth.Dto;

public class AuthenticationResponse 
{
    /// <summary>
    /// Resulting status of the action
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Resulting message of the action
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Return URL
    /// </summary>
    public string ReturnUrl { get; set; }

    /// <summary>
    /// Whether the authentication cookie is expected to live longer
    /// </summary>
    public bool StayConnected { get; set; }

    #region Relevant claims
    /// <summary>
    /// User email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// User role
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// Display name
    /// </summary>
    public string DisplayName { get; set; }

    #endregion

    /// <summary>
    /// Add a visual message
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public AuthenticationResponse AddMessage(string message)
    {
        Message = message;
        return this;
    }

    /// <summary>
    /// Set the redirect URL
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public AuthenticationResponse Redirect(string url)
    {
        ReturnUrl = url;
        return this;
    }

    /// <summary>
    /// Set the detected user's email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public AuthenticationResponse AddClaimEmail(string email)
    {
        Email = email;
        return this;
    }

    /// <summary>
    /// Set the detected user's role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public AuthenticationResponse AddClaimRole(string role)
    {
        Role = role;
        return this;
    }
    
    /// <summary>
    /// Set the user's name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public AuthenticationResponse AddClaimName(string name)
    {
        DisplayName = name;
        return this;
    }

    /// <summary>
    /// Set expected level of persistence of the actual authentication cookie
    /// </summary>
    /// <param name="stayConnected"></param>
    /// <returns></returns>
    public AuthenticationResponse SetPersistenceFlag(bool stayConnected)
    {
        StayConnected = stayConnected;
        return this;
    }



    /// <summary>
    /// Success factory method
    /// </summary>
    /// <returns></returns>
    public static AuthenticationResponse Ok()
    {
        return new AuthenticationResponse { Success = true };
    }
    
    /// <summary>
    /// Success factory method
    /// </summary>
    /// <returns></returns>
    public static AuthenticationResponse Fail()
    {
        return new AuthenticationResponse { Success = false };
    }
}
