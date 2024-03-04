using CommunityToolkit.Mvvm.ComponentModel;

namespace Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;

public partial class TeleportsViewModel : ObservableObject
{
    private bool _isInitialized;

    [ObservableProperty]
    private bool _areUiElementsEnabled = true;

    public TeleportsViewModel()
    {
        if (_isInitialized) return;
        InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        _isInitialized = true;
    }
}