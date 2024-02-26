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


using System.ComponentModel.DataAnnotations;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Renoir.DomainModel.Utils;

namespace Youbiquitous.Renoir.DomainModel;

/// <summary>
/// Helper class to track creation/updates of individual records
/// </summary>
public class TimeStamp
{
    public TimeStamp()
    {
        When = DateTime.UtcNow;
    }

    /// <summary>
    /// Time of the mark
    /// </summary>
    public DateTime? When { get; private set; }

    /// <summary>
    /// Author of the mark
    /// </summary>
    [MaxLength(50)]
    public string By { get; private set; }

    /// <summary>
    /// Logs time stamp
    /// </summary>
    public void Mark(string author = null)
    {
        MarkInternal(DateTime.UtcNow, author);
    }

    /// <summary>
    /// Logs time stamp (internal use)
    /// </summary>
    public void MarkInternal(DateTime? dt, string author = null)
    {
        When = dt;
        if (!author.IsNullOrWhitespace())
            By = author;
    }


    public string DateForDisplay(string format = "d MMM yyyy")
    {
        return When.ToStringOrEmpty(format);
    }

    public string AuthorForDisplay()
    {
        return By.IsNullOrWhitespace() ? InternalStrings.Text_User_Unknown : By;
    }

    /// <summary>
    /// Official display string
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var author = By.IsNullOrWhitespace() ? InternalStrings.Text_User_Unknown : By;
        return $"{When:d MMM yyyy HH:mm} - ({author})";
    }
}