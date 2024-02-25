using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Models;
using Forza_Mods_AIO.Resources.Search;
using Forza_Mods_AIO.Resources.Theme;
using Forza_Mods_AIO.ViewModels.Pages;

namespace Forza_Mods_AIO.Views.Pages;

public partial class AioInfo
{
    public AioInfo()
    {
        ViewModel = new AioInfoViewModel();
        DataContext = this;

        InitializeComponent();
        SearchResults.EverySearchResult.Add(new SearchResult("Monet theme", "", "", "Aio Info", typeof(AioInfo), MonetThemeButton));
    }

    public AioInfoViewModel ViewModel { get; }
    public Monet Theming => Monet.GetInstance();

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox comboBox)
        {
            return;
        }
        
        var typeItem = (ComboBoxItem)comboBox.SelectedItem;
        var value = typeItem.Content.ToString();

        if (value == null)
        {
            return;
        }

        var dict = new ResourceDictionary
        {
            Source = new Uri($"/Resources/Translations/{value}.xaml", UriKind.RelativeOrAbsolute)
        };
        
        Application.Current.Resources.MergedDictionaries.Add(dict);
    }
}