using CommunityToolkit.Mvvm.ComponentModel;

namespace Forza_Mods_AIO.ViewModels.Pages;

public partial class TuningViewModel : ObservableObject
{
    private bool _isInitialized;

    [ObservableProperty]
    private bool _wasScanSuccessful = true;
    
    public TuningViewModel()
    {
        if (_isInitialized) return;
        InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        _isInitialized = true;
    }
}