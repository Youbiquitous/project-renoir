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
    public interface IPasswordService
    {
        /// <summary>
        /// Ensures that hash of passed password matches the given hash
        /// </summary>
        /// <param name="clearPassword"></param>
        /// <param name="hashedPassword"></param>
        /// <returns></returns>
        bool Validate(string clearPassword, string hashedPassword);

        /// <summary>
        /// Returns the password string ready for storage (typically, a hash of the given string) 
        /// </summary>
        /// <param name="clearPassword"></param>
        /// <returns></returns>
        string Store(string clearPassword);

        /// <summary>
        /// Check if old and new passwords can be switched 
        /// </summary>
        /// <param name="storedHash"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool CanChangePassword(string storedHash, string oldPassword, string newPassword);

        /// <summary>
        /// Checks whether the provided string is considered a strong password
        /// </summary>
        /// <param name="proposedPassword"></param>
        /// <returns></returns>
        bool IsStrongPassword(string proposedPassword);

        /// <summary>
        /// Generates a random password using the internal rules of strong passwords
        /// </summary>
        /// <param name="minimumLength"></param>
        /// <returns></returns>
        string Generate(int minimumLength);
    }
}