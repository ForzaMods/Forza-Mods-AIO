using CommunityToolkit.Mvvm.ComponentModel;

namespace Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;

public partial class CustomizationViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _arePaintUiElementsEnabled = true;
    
    [ObservableProperty]
    private bool _areHeadlightUiElementsEnabled = true;
    
    [ObservableProperty]
    private float _glowingPaintValue;
}