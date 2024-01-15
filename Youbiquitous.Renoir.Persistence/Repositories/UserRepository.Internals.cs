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
    /// Physical insertion of new User records
    /// </summary>
    /// <param name="db"></param>
    /// <param name="user"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    private static CommandResponse AddNewUserInternal(RenoirDatabase db, User user, string author)
    {
        // Hash the password
        user.Password = PasswordServiceLocator.Get().Store(user.Password);

        // Init and save
        user.Init(author);
        db.Users.Add(user);
        return db.TrySaveChanges();
    }

    /// <summary>
    /// Edit of existing User records
    /// </summary>
    /// <param name="db"></param>
    /// <param name="user"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    private static CommandResponse UpdateExistingUserInternal(RenoirDatabase db, User user, string author)
    {
        return CommandResponse.Ok();
    }


    /// <summary>
    /// Physical removal of records
    /// </summary>
    /// <param name="db"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    private static CommandResponse DeleteExistingUserInternal(RenoirDatabase db, string email)
    {
        var deleted = db.Users
            .Where(u => u.Email == email && !u.Deleted)
            .ExecuteDelete();
        return deleted > 0
            ? CommandResponse.Ok()
            : CommandResponse.Fail();
    }
}