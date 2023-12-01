using System.Windows;
using System.Windows.Controls;
using static System.Windows.Forms.Keys;
using static Forza_Mods_AIO.MainWindow;
using Forms = System.Windows.Forms;

namespace Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs;

public partial class HandlingKeybindings
{
    public static Forms.Keys KbJmpHack = LControlKey, KbBrakeHack = Space, KbVelHack = Alt;
    
    public HandlingKeybindings()
    {
        InitializeComponent();
    }

    private void KBButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (Mw.Grabbing)
        {
            return;
        }
        
        Mw.IsClicked = Mw.Grabbing = true;
        Mw.ClickedButton = (Button)sender;
        Mw.ClickedButton.Content = "Change Key";
    }
    
    private void CTButton_OnClick(object sender, RoutedEventArgs e)
    {
        // TODO: Implement
    }
}