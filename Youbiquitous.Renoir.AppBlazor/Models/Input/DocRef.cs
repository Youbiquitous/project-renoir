///////////////////////////////////////////////////////////////////
//
// Project RENOIR
// Release Notes Instant Reporter
//
// Reference application presented in
// Clean Architecture with .NET (MS Press) 2024
// Author: Dino Esposito
// 


using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Renoir.DomainModel;
using Youbiquitous.Renoir.DomainModel.Documents;
using Youbiquitous.Renoir.DomainModel.Documents.Core;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.AppBlazor.Models.Input;


/// <summary>
/// DTO to bring data to and from the DocumentEditor form
/// </summary>
public class DocRef : DtoBase
{
    public DocRef()
    {
    }
    public DocRef(long refId, string version, DateTime? release, string notes)
    {
        RefId = refId;
        Version = version;
        Notes = notes;
        ReleaseDate = release;
    }

    public long RefId { get; set; }
    public string Version { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string Notes { get; set; }
    public TimeStamp LastUpdated { get; set; }

    /// <summary>
    /// Whether data is acceptable for a User reference
    /// </summary>
    /// <returns></returns>
    public override bool IsValid()
    {
        return !Version.IsNullOrWhitespace();
    }

    /// <summary>
    /// Whether data is acceptable for a User reference
    /// </summary>
    /// <returns></returns>
    public override CommandResponse Validate()
    {
        if (Version.IsNullOrWhitespace())
            return CommandResponse.Fail().AddMessage(AppMessages.Err_MissingVersion);

        return CommandResponse.Ok();
    }

    /// <summary>
    /// Static ctor for ReleaseNote and Roadmap
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    public static DocRef Import(ReleaseNote doc)
    {
        var dr = new DocRef(doc.RefId, doc.Version, doc.ReleaseDate, doc.Notes)
        {
            LastUpdated = doc.LastUpdated
        };
        return dr;
    }
    public static DocRef Import(Roadmap doc)
    {
        var dr = new DocRef(doc.RefId, doc.Version, doc.ReleaseDate, doc.Notes)
        {
            LastUpdated = doc.LastUpdated
        };
        return dr;
    }
}