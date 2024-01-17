///////////////////////////////////////////////////////////////////
//
// Project RENOIR
// Release Notes Instant Reporter
//
// Reference application presented in
// Clean Architecture with .NET (MS Press) 2024
// Author: Dino Esposito
// 


using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Renoir.DomainModel.Management;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.AppBlazor.Models.Input;


/// <summary>
/// DTO to bring data to and from the UserEditor form
/// </summary>
public class UserRef : DtoBase
{
    public long UserId { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Password { get; set; }

    /// <summary>
    /// Whether data is acceptable for a User reference
    /// </summary>
    /// <returns></returns>
    public override bool IsValid()
    {
        return !DisplayName.IsNullOrWhitespace() &&
               Email.IsValidEmail();
    }

    /// <summary>
    /// Whether data is acceptable for a User reference
    /// </summary>
    /// <returns></returns>
    public override CommandResponse Validate()
    {
        if (DisplayName.IsNullOrWhitespace())
            return CommandResponse.Fail().AddMessage(AppMessages.Err_MissingName);
        //if (Password.IsNullOrWhitespace())
        //    return CommandResponse.Fail().AddMessage(AppMessages.Err_MissingPassword);
        if (Role.IsNullOrWhitespace())
            return CommandResponse.Fail().AddMessage(AppMessages.Err_MissingRole);
        if (!Email.IsValidEmail())
            return CommandResponse.Fail().AddMessage(AppMessages.Err_InvalidEmail);

        return CommandResponse.Ok();
    }

    /// <summary>
    /// Fill data from a domain entity
    /// </summary>
    /// <param name="user"></param>
    public UserRef Import(User user)
    {
        UserId = user?.UserId ?? 0;
        Email = user?.Email;
        DisplayName = user?.DisplayName;
        Role = user?.Role;
        return this;
    }

    /// <summary>
    /// Fill data from another same entity 
    /// </summary>
    /// <param name="user"></param>
    public UserRef Import(UserRef user)
    {
        UserId = user?.UserId ?? 0;
        Email = user?.Email;
        DisplayName = user?.DisplayName;
        Role = user?.Role;
        Password = user?.Password;
        return this;
    }

    /// <summary>
    /// Empty object
    /// </summary>
    public void Clear()
    {
        UserId = 0;
        Email = null;
        DisplayName = null;
        Role = null;
        Password = null;
    }
}