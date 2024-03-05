using CommunityToolkit.Mvvm.ComponentModel;

namespace Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;

public partial class HandlingViewModel : ObservableObject
{
    private bool _isInitialized;

    [ObservableProperty]
    private bool _areUiElementsEnabled = true;

    [ObservableProperty]
    private double _accelValue;
    
    [ObservableProperty]
    private double _gravityValue;

    [ObservableProperty]
    private bool _isAccelEnabled;
    
    [ObservableProperty]
    private bool _isGravityEnabled;

    public HandlingViewModel()
    {
        if (_isInitialized) return;
        InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        _isInitialized = true;
    }
}