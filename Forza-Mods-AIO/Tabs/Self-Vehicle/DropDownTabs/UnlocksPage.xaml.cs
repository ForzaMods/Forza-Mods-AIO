using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

/// <summary>
///     Interaction logic for CameraPage.xaml
/// </summary>
public partial class UnlocksPage : Page
{
    public static UnlocksPage Up;
    
    public UnlocksPage()
    {
        InitializeComponent();
        Up = this;
    }

    private void CreditsSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!CreditsSwitch.IsOn)
        {
            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.CreditsHookAddr, new byte [] { 0x89, 0x84, 0x24, 0x80, 0x00, 0x00, 0x00 });
            return;
        }

        assembly.Credits(Self_Vehicle_Addrs.CodeCave13);
    }
    
    private void CreditsNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.CodeCave13 + 0x35).ToString("X"), "int", CreditsNum.Value.ToString());
    }
    
    private void XpSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!XpSwitch.IsOn)
        {
            return;
        }
    }
}