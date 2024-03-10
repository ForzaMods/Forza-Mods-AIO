using CommunityToolkit.Mvvm.ComponentModel;

namespace Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;

public partial class MiscViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _spooferUiElementsEnabled = true;
}