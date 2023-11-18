using System.Windows;
using System.Windows.Controls;
using static System.Windows.Forms.Keys;
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
        if (MainWindow.Grabbing)
        {
            return;
        }
        
        MainWindow.IsClicked = MainWindow.Grabbing = true;
        MainWindow.ClickedButton = (Button)sender;
        MainWindow.ClickedButton.Content = "Change Key";
    }
    
    private void CTButton_OnClick(object sender, RoutedEventArgs e)
    {
        // TODO: Implement
    }
}