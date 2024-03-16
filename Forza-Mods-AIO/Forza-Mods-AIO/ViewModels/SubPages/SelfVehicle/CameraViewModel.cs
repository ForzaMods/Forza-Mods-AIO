using CommunityToolkit.Mvvm.ComponentModel;

namespace Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;

public partial class CameraViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _areScanPromptLimiterUiElementsVisible = true;
    
    [ObservableProperty]
    private bool _areScanningLimiterUiElementsVisible;
    
    [ObservableProperty]
    private bool _areLimiterUiElementsVisible;
    
    [ObservableProperty]
    private bool _areCameraHookUiElementsEnabled = true;
}