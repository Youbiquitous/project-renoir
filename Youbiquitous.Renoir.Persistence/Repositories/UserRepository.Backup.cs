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

using Microsoft.EntityFrameworkCore;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Renoir.DomainModel.Management;
using Youbiquitous.Renoir.Infrastructure.Security.Password;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.Persistence.Repositories;

/// <summary>
/// Internal methods to CRUD on the User entity
/// </summary>
public partial class UserRepository
{
    /// <summary>
    /// Change email and other profile data of given user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="email"></param>
    /// <param name="display"></param>
    /// <returns></returns>
    //public static CommandResponse UpdateProfile(long userId, string email, string display)
    //{
    //    using var db = new Youbiquitous.Renoir.Persistence.RenoirDatabase();
    //    var user = db.Users.SingleOrDefault(u => u.UserId == userId && !u.Deleted);
    //    if (user == null)
    //        return CommandResponse.Fail().AddMessage(AppMessages.Err_UserNotFound);

    //    user.Email = email;
    //    user.DisplayName = display;
    //    var response = db.TrySaveChanges();
    //    return response;
    //}

    /// <summary>
    /// Edit profile AND password of given user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="email"></param>
    /// <param name="display"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    //public static CommandResponse UpdateProfileAndPassword(long userId, string email, string display, string password)
    //{
    //    using var db = new Youbiquitous.Renoir.Persistence.RenoirDatabase();
    //    var user = db.Users.SingleOrDefault(u => u.UserId == userId && !u.Deleted);
    //    if (user == null)
    //        return CommandResponse.Fail().AddMessage(AppMessages.Err_UserNotFound);

    //    user.Email = email;
    //    user.DisplayName = display;
    //    user.Password = PasswordServiceLocator.Get().Store(password);

    //    var response = db.TrySaveChanges();
    //    return response;
    //}
}