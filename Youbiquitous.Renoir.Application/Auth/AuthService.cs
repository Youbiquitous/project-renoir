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


using Youbiquitous.Renoir.Application.Auth.Dto;
using Youbiquitous.Renoir.Application.Settings;
using Youbiquitous.Renoir.Infrastructure.Security.Password;
using Youbiquitous.Renoir.Persistence.Repositories;

namespace Youbiquitous.Renoir.Application.Auth;

public class AuthService : ApplicationServiceBase
{
    public AuthService(RenoirSettings settings) 
        : base(settings)
    {
    }

    /// <summary>
    /// Attempt to validate credentials 
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public AuthenticationResponse TrySignIn(AuthenticationRequest login)
    {
        if (login == null) 
            return AuthenticationResponse.Fail();
        if (!login.IsValid())
            return AuthenticationResponse.Fail();

        var user = UserRepository.FindById(login.Email);
        if (user == null)
            return AuthenticationResponse.Fail().AddMessage("Credentials dont match");
        if (user.Locked)
            return AuthenticationResponse.Fail().AddMessage("Account currently locked down");

        // Placeholder for credentials validation
        var passwordService = PasswordServiceLocator.Get();

        return passwordService.Validate(login.Password, user.Password)
            ? AuthenticationResponse.Ok()
                .AddClaimEmail(login.Email)
                .AddClaimRole(user.RoleId)
                .AddClaimName(user.DisplayName)
                .SetPersistenceFlag(login.StayConnected)
            : AuthenticationResponse.Fail().AddMessage("Credentials dont match");
    }


}