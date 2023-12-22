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

namespace Youbiquitous.Renoir.Infrastructure.Security.Password.Misc;

/// <summary>
/// Container of the various parts that make up a stored/salted/hashed password
/// </summary>
public class PasswordParts
{
    public PasswordParts(byte[] salt, byte[] content)
    {
        Padding = null;
        Salt = salt;
        Content = content;
    }

    /// <summary>
    /// Optional, initial padding added to confuse things
    /// </summary>
    public byte[] Padding { get; private set; }

    /// <summary>
    /// Salt 
    /// </summary>
    public byte[] Salt { get; }

    /// <summary>
    /// Actual bytes of the hashed password
    /// </summary>
    public byte[] Content { get; }
}