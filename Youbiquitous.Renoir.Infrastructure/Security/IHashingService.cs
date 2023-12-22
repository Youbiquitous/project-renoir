///////////////////////////////////////////////////////////////////
//
// Youbiquitous MARTLET 
// C# services
// 2021
//
// Author: Dino Esposito
//

namespace Youbiquitous.Renoir.Infrastructure.Security
{
    public interface IHashingService
    {
        bool Validate(string clearPassword, string hashedPassword);
        string Hash(string clearPassword);
    }
}