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

// ***************************************************************
//
//  Each password has its own salt of 512 chars
//  The salt is stored with the hashed password in the DB
//  Salt and password are interspersed 
//  Optional random padding is attached at the beginning of the stored string
//
// ***************************************************************

using System.Security.Cryptography;
using Youbiquitous.Renoir.Infrastructure.Security.Password.Misc;

namespace Youbiquitous.Renoir.Infrastructure.Security.Password.Hashing;

public partial class DefaultPasswordHashingService 
{
    /// <summary>
    /// Creates a uniquely salted hash for the provided password
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    protected virtual string HashInternal(string password)
    {
        // Create padding and salt
        var padding = RandomNumberGenerator.GetBytes(PaddingSize);
        var salt = RandomNumberGenerator.GetBytes(SaltSize);

        // Create hash 
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, IterationCount, HashAlgorithmName.SHA512);
        var hash = pbkdf2.GetBytes(DefaultHashSize);

        // Compose into into a single stream of bytes and convert to Base64
        var bytes = ComposeParts(padding, salt, hash);
        var base64 = Convert.ToBase64String(bytes);
        return base64;
    }

    /// <summary>
    /// Decompose a stored sequence of bytes into constituent logical parts
    /// assuming the following structure [Padding][Salt][Hash]
    /// </summary>
    /// <returns></returns>
    protected virtual PasswordParts DecomposeParts(string hashedPassword)
    {
        // From Base64 yo bytes
        var bytes = Convert.FromBase64String(hashedPassword);

        // Skip padding
        //var padding = new byte[PaddingSize];
        //Array.Copy(bytes, 0, padding, 0, PaddingSize);

        var salt = new byte[SaltSize];
        Array.Copy(bytes, PaddingSize, salt, 0, SaltSize);
        

        var content = new byte[HashSize];
        Array.Copy(bytes, PaddingSize + SaltSize, content, 0, HashSize);

        var parts = new PasswordParts(salt, content);
        return parts;
    }

    /// <summary>
    /// Extract salt and password from the hashed string
    /// assuming the following structure [Padding][Salt][Hash]
    /// </summary>
    /// <returns></returns>
    protected virtual byte[] ComposeParts(byte[] padding, byte[] salt, byte[] hash)
    {
        // Preliminary checks
        if (padding is null || padding.Length != PaddingSize)
            throw new Exception("Hash issue 1023");
        if (salt is null || salt.Length != SaltSize)
            throw new Exception("Hash issue 1034");
        if (hash is null || hash.Length != HashSize)
            throw new Exception("Hash issue 1056");

        var buffer = new byte[PaddingSize + SaltSize + HashSize];
        Array.Copy(padding, 0, buffer, 0, PaddingSize);
        Array.Copy(salt, 0, buffer, PaddingSize, SaltSize);
        Array.Copy(hash, 0, buffer, PaddingSize + SaltSize, HashSize);

        return buffer;
    }
}