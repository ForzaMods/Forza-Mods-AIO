using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Forza_Mods_AIO.Models;
using Forza_Mods_AIO.Views.Pages;

namespace Forza_Mods_AIO.ViewModels.Pages;

public partial class SearchViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<SearchResult> _searchResults = [];
    
    public void Search(string text)
    {
        SearchResults.Clear();
        if (text == string.Empty)
        {
            return;
        }

        var search = Resources.Search.SearchResults.EverySearchResult.Where(i =>
            i.Name.Contains(text, StringComparison.InvariantCultureIgnoreCase) ||
            i.Feature.Contains(text, StringComparison.InvariantCultureIgnoreCase) ||
            i.Category.Contains(text, StringComparison.InvariantCultureIgnoreCase) ||
            i.Page.Contains(text, StringComparison.InvariantCultureIgnoreCase));
        
        var searchResults = search as SearchResult[] ?? search.ToArray();
        foreach (var element in searchResults)
        {
            if (GameVerPlat.GetInstance().Type == GameVerPlat.GameType.None && element.PageType != typeof(AioInfo))
            {
                continue;
            }

            SearchResults.Add(element);
        }
    }
}