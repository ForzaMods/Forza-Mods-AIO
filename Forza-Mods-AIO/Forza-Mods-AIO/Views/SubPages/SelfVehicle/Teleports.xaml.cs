using System.Windows;
using MahApps.Metro.Controls;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;

namespace Forza_Mods_AIO.Views.SubPages.SelfVehicle;

public partial class Teleports
{
    public Teleports()
    {
        ViewModel = new TeleportsViewModel();
        DataContext = this;
        
        InitializeComponent();
    }

    public TeleportsViewModel ViewModel { get; }
    private static CarCheats CarCheatsFh5 => Forza_Mods_AIO.Resources.Cheats.GetClass<CarCheats>();

    private async void ToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.AreUiElementsEnabled = false;

        if (CarCheatsFh5.WaypointAddress == 0)
        {
            await CarCheatsFh5.CheatWaypoint();
        }

        Forza_Mods_AIO.Resources.Memory.GetInstance().WriteMemory(CarCheatsFh5.WaypointDetourAddress + 0x32, toggleSwitch.IsOn ? (byte)1 : (byte)0);
        ViewModel.AreUiElementsEnabled = true;
    }
}