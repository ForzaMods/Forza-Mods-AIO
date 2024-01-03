using System.Numerics;
using System.Windows;
using Forza_Mods_AIO.Resources;
using static Forza_Mods_AIO.MainWindow;
using static System.Convert;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

public partial class BackFirePage
{
    public static BackFirePage BackFire { get; private set; } = null!;

    public readonly Detour BackfireTimeDetour = new();
    public readonly Detour BackfireTypeDetour = new();
    
    private const string BackfireTimeBytes = "F3 0F 10 05 0D 00 00 00 F3 0F 10 0D 09 00 00 00";
    private const string BackfireTypeBytes = "80 3D 0F 00 00 00 01 75 05 48 8B D0 EB 03 48 8B C2";
    
    public BackFirePage()
    {
        InitializeComponent();
        BackFire = this;
    }

    private void TimeNumerics_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached || MaxTime == null)
        {
            return;
        }
        
        BackfireTimeDetour.UpdateVariable(new Vector2(ToSingle(MinTime.Value), ToSingle(MaxTime.Value)));
    }

    private void BackfireToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        if (!BackfireTimeDetour.Setup(sender, BackfireTimeAddr, BackfireTimeBytes, 8, true))
        {
            BackfireToggle.Toggled -= BackfireToggle_OnToggled;
            BackfireToggle.IsOn = false;
            BackfireToggle.Toggled += BackfireToggle_OnToggled;
            MessageBox.Show("Failed");            
            return;
        }

        BackfireTimeDetour.UpdateVariable(new Vector2(ToSingle(MinTime.Value), ToSingle(MaxTime.Value)));
        BackfireTimeDetour.Toggle();
    }

    private void ForceBackfireType_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!BackfireTypeDetour.Setup(sender, BackfireTypeAddr, BackfireTypeBytes, 7, true))
        {
            MessageBox.Show("Failed");            
            return;
        }
        
        var name = sender.GetType().GetProperty("Name").GetValue(sender);

        switch (name)
        {
            case "ForceAntiLag":
            {
                ForceNormal.IsEnabled = !ForceNormal.IsEnabled;
                BackfireTypeDetour.UpdateVariable<byte>(1);
                break;
            }
            case "ForceNormal":
            {
                ForceAntiLag.IsEnabled = !ForceAntiLag.IsEnabled;
                BackfireTypeDetour.UpdateVariable<byte>(2);
                break;
            }
        }

        BackfireTypeDetour.Toggle();
    }
}