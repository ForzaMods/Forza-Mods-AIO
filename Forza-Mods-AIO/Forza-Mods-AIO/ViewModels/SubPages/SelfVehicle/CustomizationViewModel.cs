using CommunityToolkit.Mvvm.ComponentModel;

namespace Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;

public partial class CustomizationViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _arePaintUiElementsEnabled = true;
    
    [ObservableProperty]
    private bool _areHeadlightUiElementsEnabled = true;
    
    [ObservableProperty]
    private bool _areCleanlinessUiElementsEnabled = true;
    
    [ObservableProperty]
    private bool _areForceLodUiElementsEnabled = true;
    
    [ObservableProperty]
    private bool _areBackfireUiElementsEnabled = true;
    
    [ObservableProperty]
    private float _glowingPaintValue;
    
    [ObservableProperty]
    private bool _dirtEnabled;
    
    [ObservableProperty]
    private float _dirtValue;

    [ObservableProperty]
    private bool _mudEnabled;
    
    [ObservableProperty]
    private float _mudValue;
}