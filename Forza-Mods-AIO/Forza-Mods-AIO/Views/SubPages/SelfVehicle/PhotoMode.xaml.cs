using System.Windows;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using MahApps.Metro.Controls;
using static Forza_Mods_AIO.Resources.Cheats;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Views.SubPages.SelfVehicle;

public partial class PhotoMode
{
    public PhotoMode()
    {
        DataContext = this;
        
        InitializeComponent();
    }

    private static PhotomodeCheats PhotomodeCheatsFh5 => GetClass<PhotomodeCheats>();

    private async void NoClipSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (PhotomodeCheatsFh5.NoClipDetourAddress == 0)
        {
            await PhotomodeCheatsFh5.CheatNoClip();
        }
        toggleSwitch.IsEnabled = true;

        
        if (PhotomodeCheatsFh5.NoClipDetourAddress == 0) return;
        GetInstance().WriteMemory(PhotomodeCheatsFh5.NoClipDetourAddress + 0x19, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }
}