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


using System.Text.Json;

namespace Youbiquitous.Renoir.AppBlazor.Common.Extensions;

public static class JsonExtensions
{
    /// <summary>
    /// Attempt to turn a string into a given JSON object regardless of property case
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public static T ToJsonObject<T>(this string json) where T : class, new()
    {
        if (string.IsNullOrEmpty(json))
            return new T();

        try
        {
            return JsonSerializer.Deserialize<T>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception)
        {
            return new T();
        }
    }
}
