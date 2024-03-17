using CommunityToolkit.Mvvm.ComponentModel;

namespace Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;

public partial class EnvironmentViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _areSunRgbUiElementsEnabled = true;
}