using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Forza_Mods_AIO.ViewModels.Pages;

public partial class SelfVehicleViewModel : ObservableObject
{
    private bool _isInitialized;

    [ObservableProperty]
    private bool _wasScanSuccessful = true;
    
    public SelfVehicleViewModel()
    {
        if (_isInitialized) return;
        InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        _isInitialized = true;
    }
}