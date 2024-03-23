using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;

namespace Forza_Mods_AIO.ViewModels.Pages;

public partial class TuningViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _areUiElementsEnabled;
    
    [ObservableProperty]
    private bool _areScanPromptUiElementsEnabled = true;
    
    [ObservableProperty]
    private bool _areScanningUiElementsVisible;

    private static TuningCheats TuningCheatsFh5 => Resources.Cheats.GetClass<TuningCheats>();
    
    [RelayCommand]
    private async Task Scan()
    {
        AreScanPromptUiElementsEnabled = false;
        AreScanningUiElementsVisible = true;

        if (!TuningCheatsFh5.WasScanSuccessful)
        {
            await TuningCheatsFh5.Scan();
        }

        if (!TuningCheatsFh5.WasScanSuccessful)
        {
            AreScanPromptUiElementsEnabled = true;
            AreScanningUiElementsVisible = false;
            return;
        }

        AreScanningUiElementsVisible = false;
        AreUiElementsEnabled = true;
    }
}