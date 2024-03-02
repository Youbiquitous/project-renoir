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


using System.Text;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Renoir.DomainModel.Documents;
using Youbiquitous.Renoir.DomainModel.Documents.Core;
using Youbiquitous.Renoir.Resources;

namespace Youbiquitous.Renoir.Application.Renderers;

public static class PlainTextRenderer
{
    private static readonly string DoubleSep = new('=', 80);
    private static readonly string SingleSep = new('-', 80);

    /// <summary>
    /// Serialize the relevant content of the Release Note object as plain text
    /// </summary>
    /// <param name="rnotes"></param>
    /// <returns></returns>
    public static string Get(ReleaseNote rnotes)
    {
        var builder = new StringBuilder();

        // Header
        builder.AppendJoin('\n', $"{rnotes.RelatedProduct}", DoubleSep);
        builder.AppendLine().AppendLine();
        builder.AppendLine($"{AppStrings.Text_ReleaseNote.ToUpper()} {rnotes.Version},  {rnotes.ReleaseDate.ToStringOrEmpty("d MMM yyyy")}");
        builder.AppendLine();
        
        // Actual content
        foreach (var item in rnotes.Items.OrderBy(i => i.Order))
        {
            if (item.ItemType.IsDivider())
            {
                var filler = new string(' ', 12);
                builder
                    .AppendFormat("\n{0}\t{1}\n", filler, item.Description)
                    .AppendLine(SingleSep);
                continue;
            }

            var prefix = $"{item.Category}".PadRight(12);
            builder
                .AppendFormat("{0}\t{1}", prefix, item.Description)
                .AppendLine();
        }

        return builder.ToString();
    }

    /// <summary>
    /// Serialize the relevant content of the Roadmap object as plain text
    /// </summary>
    /// <param name="rmap"></param>
    /// <returns></returns>
    public static string Get(Roadmap rmap)
    {
        var builder = new StringBuilder();

        // Header
        builder.AppendJoin('\n', $"{rmap.RelatedProduct}", DoubleSep);
        builder.AppendLine().AppendLine();
        builder.AppendLine($"{AppStrings.Text_Roadmap.ToUpper()} {rmap.Version},  {rmap.ReleaseDate.ToStringOrEmpty("d MMM yyyy")}");
        builder.AppendLine();

        // Actual content
        foreach (var item in rmap.Items.OrderBy(i => i.Order))
        {
            if (item.ItemType.IsDivider())
            {
                var filler = new string(' ', 12);
                builder
                    .AppendFormat("\n{0}\t{1}\n", filler, item.Description)
                    .AppendLine(SingleSep);
                continue;
            }

            var prefix = $"{item.Category}".PadRight(12);
            builder
                .AppendFormat("{0}\t{1}", prefix, item.Description)
                .AppendLine();
        }

        return builder.ToString();
    }
}