///////////////////////////////////////////////////////////////////
//
// Project RENOIR
// Release Notes Instant Reporter
//
// Reference application presented in
// Clean Architecture with .NET (MS Press) 2024
// Author: Dino Esposito
// 


namespace Youbiquitous.Renoir.DomainModel.Misc;


/// <summary>
/// Counters of relevant tables
/// </summary>
public class CountersDescriptor  
{
    public int TotalProducts { get; set; }
    public int TotalUsers { get; set; }
    public int TotalNotes { get; set; }
    public int TotalRoadmaps { get; set; }
}