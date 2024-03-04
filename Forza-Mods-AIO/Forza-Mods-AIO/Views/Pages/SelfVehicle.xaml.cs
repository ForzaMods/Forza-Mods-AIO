using Forza_Mods_AIO.Models;
using Forza_Mods_AIO.Resources.Search;
using Forza_Mods_AIO.Resources.Theme;
using Forza_Mods_AIO.ViewModels.Pages;

namespace Forza_Mods_AIO.Views.Pages;

public partial class SelfVehicle
{
    public SelfVehicle()
    {
        ViewModel = new SelfVehicleViewModel();
        DataContext = this;

        Loaded += (_, _) => AddSearchResults();

        InitializeComponent();
    }

    public SelfVehicleViewModel ViewModel { get; }

    public virtual Monet Theming => Monet.GetInstance();

    private void AddSearchResults()
    {
        SearchResults.EverySearchResult.Add(new SearchResult("Handling", string.Empty, string.Empty, "Self/Vehicle",
            typeof(SelfVehicle), null!, HandlingExpander));
        SearchResults.EverySearchResult.Add(new SearchResult("Unlocks", string.Empty, string.Empty, "Self/Vehicle",
            typeof(SelfVehicle), null!, UnlocksExpander));
        SearchResults.EverySearchResult.Add(new SearchResult("Photo Mode", string.Empty, string.Empty, "Self/Vehicle",
            typeof(SelfVehicle), null!, PhotoModeExpander));
        SearchResults.EverySearchResult.Add(new SearchResult("Teleports", string.Empty, string.Empty, "Self/Vehicle",
            typeof(SelfVehicle), null!, TeleportsExpander));
        SearchResults.EverySearchResult.Add(new SearchResult("Environment", string.Empty, string.Empty, "Self/Vehicle",
            typeof(SelfVehicle), null!, EnvironmentExpander));
        SearchResults.EverySearchResult.Add(new SearchResult("Customization", string.Empty, string.Empty,
            "Self/Vehicle", typeof(SelfVehicle), null!, CustomizationExpander));
        SearchResults.EverySearchResult.Add(new SearchResult("Miscellaneous", string.Empty, string.Empty,
            "Self/Vehicle", typeof(SelfVehicle), null!, MiscExpander));
        SearchResults.EverySearchResult.Add(new SearchResult("Camera", string.Empty, string.Empty, "Self/Vehicle",
            typeof(SelfVehicle), null!, CameraExpander));
    }
}