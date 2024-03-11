using CommunityToolkit.Mvvm.ComponentModel;
using Forza_Mods_AIO.Models;

namespace Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;

public partial class HandlingViewModel : ObservableObject
{
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
}