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

        // TODO: Add language switching based on the selected index and not the content since that shit crashes
    }
}