using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Resources.Theme;
using Forza_Mods_AIO.ViewModels.Pages;

namespace Forza_Mods_AIO.Views.Pages;

public partial class Settings
{
    public Settings()
    {
        ViewModel = new SettingsViewModel();
        DataContext = this;
        
        InitializeComponent();
    }
    
    public SettingsViewModel ViewModel { get; }
    public Monet Theming => Monet.GetInstance();
    
    private void LanguageBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
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