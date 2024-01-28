using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IniParser;

using static System.Environment;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs;

public partial class OverlayKeybindings
{
    public OverlayKeybindings()
    {
        InitializeComponent();
        UpdateKeybindButtons();
    }

    private void CTButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
        {
            return;
        }
        
        button.Content = "Change Key";
        Mw.Gamepad.GetAndSetXInputKey(button);
    }
    
    private void UpdateKeybindButtons()
    {
        /*UpButton.Content = Up;
        DownButton.Content = Down;
        LeftButton.Content = Left;
        RightButton.Content = Right;
        LeaveButton.Content = Leave;
        ConfirmButton.Content = Confirm;
        OverlayVisibilityButton.Content = OverlayVisibility;
        RapidAdjust.Content = OverlayHandling.RapidAdjust;

        ControllerUpButton.Content = ControllerUp;
        ControllerDownButton.Content = ControllerDown;
        ControllerLeftButton.Content = ControllerLeft;
        ControllerRightButton.Content = ControllerRight;
        ControllerLeaveButton.Content = ControllerLeave;
        ControllerConfirmButton.Content = ControllerConfirm;
        ControllerOverlayVisibilityButton.Content = ControllerOverlayVisibility;
        ControllerRapidAdjust.Content = OverlayHandling.ControllerRapidAdjust;*/
    }
    
    private void KeybindingButton_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (sender is not Button button)
        {
            return;
        }
        
        Forza_Mods_AIO.Resources.Keybindings.ChangeKeybinding(ref button, e.Key);
    }
}