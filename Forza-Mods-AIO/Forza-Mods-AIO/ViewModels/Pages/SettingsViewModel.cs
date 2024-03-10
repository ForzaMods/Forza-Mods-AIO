using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Forza_Mods_AIO.ViewModels.Pages;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private Color _themeColor;    
    
    [RelayCommand]
    private void ChangeTheme()
    {
        
    }
    
    [RelayCommand]
    private void MonetTheme()
    {
        
    }
}