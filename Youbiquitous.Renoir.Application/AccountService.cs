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
using Youbiquitous.Renoir.Infrastructure.Security.Password;
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
    /// <returns></returns>
    public static User Find(string email)
    {
        return UserRepository.FindById(email);
    }

    /// <summary>
    /// Create a new account
    /// </summary>
    /// <param name="email"></param>
    /// <param name="role"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static CommandResponse CreateNew(string email, string role, string name)
    {
        var password = DefaultPasswordService.Generate(8);
        var response = UserRepository.CreateNew(email, password, role, name);

        // Send email with auto-generated password
        // ...

        return response;
    }
}