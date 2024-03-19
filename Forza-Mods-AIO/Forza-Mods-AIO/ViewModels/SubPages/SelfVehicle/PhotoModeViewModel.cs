using CommunityToolkit.Mvvm.ComponentModel;

namespace Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;

public partial class PhotoModeViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _areScanPromptLimiterUiElementsVisible = true;
    
    [ObservableProperty]
    private bool _areScanningLimiterUiElementsVisible;
    
    [ObservableProperty]
    private bool _areModifierUiElementsVisible;

}