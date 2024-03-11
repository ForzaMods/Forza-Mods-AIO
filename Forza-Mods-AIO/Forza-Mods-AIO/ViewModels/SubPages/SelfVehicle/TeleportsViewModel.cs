using CommunityToolkit.Mvvm.ComponentModel;

namespace Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;

public partial class TeleportsViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _areUiElementsEnabled = true;
}