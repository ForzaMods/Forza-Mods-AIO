using CommunityToolkit.Mvvm.ComponentModel;
using Forza_Mods_AIO.Models;

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
    
    public bool IsFh4 => GameVerPlat.GetInstance().Type == GameVerPlat.GameType.Fh4;
    public bool IsHorizon => GameVerPlat.GetInstance().Type is GameVerPlat.GameType.Fh4 or GameVerPlat.GameType.Fh5;
    
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