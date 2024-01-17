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
using Youbiquitous.Renoir.DomainModel.Management;

namespace Youbiquitous.Renoir.Persistence.Repositories;

/// <summary>
/// Repository for User entity
/// </summary>
public partial class UserRepository
{
    /// <summary>
    /// Edit/add a new user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    public static CommandResponse Upsert(User user, string author)
    {
        using var db = new RenoirDatabase();
        var found = db.Users.FirstOrDefault(u => u.UserId == user.UserId);

        var response = found is null
            ? AddNewUserInternal(db, user, author)
            : UpdateExistingUserInternal(db, found, user, author);

        return response;
    }


    /// <summary>
    /// Delete a user
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static CommandResponse Delete(string email)
    {
        using var db = new RenoirDatabase();
        return DeleteExistingUserInternal(db, email);
    }
}