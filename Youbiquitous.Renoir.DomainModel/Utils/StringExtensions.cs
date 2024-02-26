using Youbiquitous.Martlet.Core.Extensions;

namespace Youbiquitous.Renoir.DomainModel.Utils;

public static class StringExtensions
{
    /// <summary>
    /// Whether two strings are equals or both null
    /// </summary>
    /// <param name="theString"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool NullOrEquals(this string theString, string other)
    {
        if (theString == null && other == null)
            return true;

        return theString.EqualsAny(other);
    }
}