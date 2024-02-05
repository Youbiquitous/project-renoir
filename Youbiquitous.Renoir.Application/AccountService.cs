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
using Youbiquitous.Renoir.Application.Settings;
using Youbiquitous.Renoir.DomainModel.Management;
using Youbiquitous.Renoir.Persistence.Repositories;

namespace Youbiquitous.Renoir.Application;

public class AccountService : ApplicationServiceBase
{
    public AccountService(RenoirSettings settings) 
        : base(settings)
    {
    }

    /// <summary>
    /// All accounts
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<User> Accounts()
    {
        return UserRepository.FindAll();
    }

    /// <summary>
    /// One particular account
    /// </summary>
    /// <param name="email"></param>
    /// <param name="includeProducts"></param>
    /// <returns></returns>
    public static User Find(string email, bool includeProducts = false)
    {
        return UserRepository.FindById(email, includeProducts);
    }

    /// <summary>
    /// Edit/create a new account
    /// </summary>
    /// <param name="id"></param>
    /// <param name="email"></param>
    /// <param name="role"></param>
    /// <param name="name"></param>
    /// <param name="password"></param>
    /// <param name="author"></param>
    /// <returns></returns>
    public static CommandResponse SaveUser(long id, string name, string email, string role, string password, string author)
    {
        var user = new User(id, email, role, name)
        {
            Password = password
        };
        var response = UserRepository.Upsert(user, author);

        // Send email with auto-generated password
        // ...

        return response;
    }

    /// <summary>
    /// Delete an existing account
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static CommandResponse Delete(string email)
    {
        return UserRepository.Delete(email); 
    }
}