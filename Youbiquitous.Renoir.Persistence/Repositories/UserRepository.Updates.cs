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
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Renoir.DomainModel.Management;
using Youbiquitous.Renoir.Infrastructure.Security;
using Youbiquitous.Renoir.Infrastructure.Security.Password;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.Persistence.Repositories;

/// <summary>
/// Repository for User entity
/// </summary>
public partial class UserRepository
{
    /// <summary>
    /// Add a new user (no updates)
    /// </summary>
    /// <param name="email"></param>
    /// <param name="clearPassword"></param>
    /// <param name="role"></param>
    /// <param name="display"></param>
    /// <returns></returns>
    public static CommandResponse CreateNew(string email, string clearPassword, string role, string display)
    {
        using var db = new RenoirDatabase();
        var found = db.Users.FirstOrDefault(u => u.Email == email && !u.Deleted);
        if (found != null) 
            return CommandResponse.Fail().AddMessage(AppStrings.Err_EmailAlreadyExists);

        var hashedPassword = PasswordServiceLocator.Get().Store(clearPassword);
        var user = new User(email, hashedPassword, role);
        if (!display.IsNullOrWhitespace())
            user.DisplayName = display;
        user.SignedUp = DateTime.UtcNow;
        db.Users.Add(user);

        var response = db.TrySaveChanges();
        return response;
    }

    /// <summary>
    /// Change email and other profile data of given user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="email"></param>
    /// <param name="display"></param>
    /// <returns></returns>
    public static CommandResponse UpdateProfile(long userId, string email, string display)
    {
        using var db = new Youbiquitous.Renoir.Persistence.RenoirDatabase();
        var user = db.Users.SingleOrDefault(u => u.UserId == userId && !u.Deleted);
        if (user == null)
            return CommandResponse.Fail().AddMessage(AppStrings.Err_UserNotFound);

        user.Email = email;
        user.DisplayName = display;
        var response = db.TrySaveChanges();
        return response;
    }

    /// <summary>
    /// Edit profile AND password of given user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="email"></param>
    /// <param name="display"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static CommandResponse UpdateProfileAndPassword(long userId, string email, string display, string password)
    {
        using var db = new Youbiquitous.Renoir.Persistence.RenoirDatabase();
        var user = db.Users.SingleOrDefault(u => u.UserId == userId && !u.Deleted);
        if (user == null)
            return CommandResponse.Fail().AddMessage(AppStrings.Err_UserNotFound);

        user.Email = email;
        user.DisplayName = display;
        user.Password = PasswordServiceLocator.Get().Store(password);

        var response = db.TrySaveChanges();
        return response;
    }
}