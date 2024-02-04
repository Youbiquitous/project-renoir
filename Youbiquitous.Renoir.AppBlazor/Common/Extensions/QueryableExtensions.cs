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


using Youbiquitous.Renoir.DomainModel.Management;

namespace Youbiquitous.Renoir.AppBlazor.Common.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// Specific filter on list of users
    /// </summary>
    /// <param name="users"></param>
    /// <returns></returns>
    public static IEnumerable<User> Bindable(this IEnumerable<User> users)
    {
        return users
            .Where(u => !u.IsSystem())
            .AsEnumerable();
    }
}
