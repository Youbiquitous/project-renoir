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

namespace Youbiquitous.Renoir.Infrastructure.Security.Password;

public static class PasswordServiceLocator
{
    public static IPasswordService Get()
    {
        return new DefaultPasswordService(new Hashing.DefaultPasswordHashingService());
    }
}