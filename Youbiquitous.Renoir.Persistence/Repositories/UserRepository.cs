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
using Youbiquitous.Renoir.DomainModel.Management;

namespace Youbiquitous.Renoir.Persistence.Repositories;

/// <summary>
/// Repository for User entity
/// </summary>
public static partial class UserRepository
{
    /// <summary>
    /// Retrieves the (single) user matching the email provided
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static User FindById(string email)
    {
        using var db = new RenoirDatabase();
        var user = db.Users.SingleOrDefault(u => u.Email == email && !u.Deleted);
        return user;
    }

    /// <summary>
    /// Retrieves the (filtered) list of users
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static IList<User> FindAll(string filter = "")
    {
        using var db = new RenoirDatabase();
        var query = db.Users.Where(u => !u.Deleted);
        if (!filter.IsNullOrWhitespace())
            query = query.Where(u => EF.Functions.Like(u.Email, $"%{filter}%") ||
                                     EF.Functions.Like(u.DisplayName, $"%{filter}%"));
        return query.ToList();
    }
}