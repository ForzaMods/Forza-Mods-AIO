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
    
    [ObservableProperty]
    private float _spinPrizeScaleValue;
    
    [ObservableProperty]
    private int _spinSellFactorValue = 999;
    
    [ObservableProperty]
    private int _skillScoreMultiplierValue = 10;
    
    [ObservableProperty]
    private float _driftScoreMultiplierValue = 5;
    
    [ObservableProperty]
    private float _skillTreeWideEditValue = 10000;
    
    [ObservableProperty]
    private int _skillTreeCostValue;
    
    [ObservableProperty]
    private float _missionTimeScaleValue = 0.5f;
    
    [ObservableProperty]
    private float _trailblazerTimeScaleValue = 0.5f;
}