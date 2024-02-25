using Forza_Mods_AIO.Models;
using Forza_Mods_AIO.Views.Pages;

namespace Forza_Mods_AIO.Resources.Search;

public static class SearchResults
{
    public static readonly List<SearchResult> EverySearchResult = 
    [
        new SearchResult("Aio Info", string.Empty, string.Empty, string.Empty, typeof(AioInfo)),
        new SearchResult("Autoshow/Garage", string.Empty, string.Empty, string.Empty, typeof(Autoshow)),
        new SearchResult("Self/Vehicle", string.Empty, string.Empty, string.Empty, typeof(SelfVehicle)),
        new SearchResult("Tuning", string.Empty, string.Empty, string.Empty, typeof(Tuning)),
        new SearchResult("Keybindings", string.Empty, string.Empty, string.Empty, typeof(Keybindings)),
    ];
}