using CommunityToolkit.Mvvm.ComponentModel;

namespace Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;

public partial class MiscViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _spooferUiElementsEnabled = true;
    
    [ObservableProperty]
    private bool _mainUiElementsEnabled = true;
    
    [ObservableProperty]
    private bool _spinPrizeScaleEnabled;
    
    [ObservableProperty]
    private bool _spinSellFactorEnabled;
    
    [ObservableProperty]
    private bool _skillScoreMultiplierEnabled;
    
    [ObservableProperty]
    private bool _driftScoreMultiplierEnabled;
    
    [ObservableProperty]
    private bool _skillTreeWideEditEnabled;
    
    [ObservableProperty]
    private bool _skillTreeCostEnabled;
    
    [ObservableProperty]
    private bool _missionTimeScaleEnabled;
    
    [ObservableProperty]
    private bool _trailblazerTimeScaleEnabled;
}