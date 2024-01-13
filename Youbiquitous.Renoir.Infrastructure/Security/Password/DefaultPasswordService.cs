///////////////////////////////////////////////////////////////////
//
// Youbiquitous MARTLET 
// C# services
// 2021
//
// Author: Dino Esposito
//



///////////////////////////////////////////////////////////////////
//
// Youbiquitous MARTLET 
// C# services
// 2021
//
// Author: Dino Esposito
//


using Youbiquitous.Martlet.Core.Extensions;

namespace Youbiquitous.Renoir.Infrastructure.Security.Password;

public sealed class DefaultPasswordService : IPasswordService
{
    private readonly IHashingService _hashingService;

    public DefaultPasswordService(IHashingService hashingService)
    {
        _hashingService = hashingService;
    }


    /// <summary>
    /// Check if old and new passwords can be switched 
    /// </summary>
    /// <param name="storedHash"></param>
    /// <param name="oldPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    public bool CanChangePassword(string storedHash, string oldPassword, string newPassword)
    {
        if (oldPassword.IsNullOrWhitespace() && newPassword.IsNullOrWhitespace())
            return true;

        if (oldPassword.IsNullOrWhitespace() || !_hashingService.Validate(oldPassword, storedHash))
            return false;

        if (!IsStrongPassword(newPassword))
            return false;

        return true;
    }

    /// <summary>
    /// Ensures that hash of passed password matches the given hash
    /// </summary>
    /// <param name="clearPassword"></param>
    /// <param name="hashedPassword"></param>
    /// <returns></returns>
    public bool Validate(string clearPassword, string hashedPassword)
    {
        if (clearPassword.IsNullOrWhitespace() ||
            hashedPassword.IsNullOrWhitespace())
            return false;

        return _hashingService.Validate(clearPassword, hashedPassword);
    }

    /// <summary>
    /// Returns the password string ready for storage (typically, a hash of the given string) 
    /// </summary>
    /// <param name="clearPassword"></param>
    /// <returns></returns>
    public string Store(string clearPassword)
    {
        return _hashingService.Hash(clearPassword);
    }

    /// <summary>
    /// Checks whether the provided string is considered a strong password
    /// </summary>
    /// <param name="proposedPassword"></param>
    /// <returns></returns>
    public bool IsStrongPassword(string proposedPassword)
    {
        const int MinimumLength = 8;
        var score = 0;
        if (proposedPassword.Length >= MinimumLength)
            score++;

        // Doesnt work !!!
        //if (Regex.Match(proposedPassword, @"/\d+/", RegexOptions.ECMAScript).Success)
        //    score++;
        //if (Regex.Match(proposedPassword, @"/[a-z]/", RegexOptions.ECMAScript).Success &&
        //    Regex.Match(proposedPassword, @"/[A-Z]/", RegexOptions.ECMAScript).Success)
        //    score++;
        //if (Regex.Match(proposedPassword, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
        //    score++;

        // To be changed after fixing RegEx things
        return score > 0;
    }

    /// <summary>
    /// Generates a random password using the internal rules of strong passwords
    /// </summary>
    /// <param name="minimumLength"></param>
    /// <returns></returns>
    public static string Generate(int minimumLength)
    {
        var pswd = "";
        var possible = "ABCDEFGHI3456JKLMNO$PQRSTUVWXYZabcdefghijklmnopqrstu£vwxyz12789";
        var random = new Random(DateTime.Now.Second);
        for (var i = 0; i < minimumLength; i++)
        {
            var index = random.Next(0, possible.Length - 1);
            pswd += possible[index].ToString();
        }

        return pswd;
    }
}