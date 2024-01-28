using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using SharpDX.XInput;

namespace Forza_Mods_AIO.Resources;

public class Keybindings
{
    #region Overlay
    
    public Key Up = Key.I;
    public Key Down = Key.K;
    public Key Left = Key.J;
    public Key Right = Key.L;
    public Key Confirm = Key.O;
    public Key Leave = Key.U;
    public Key RapidAdjust = Key.LeftAlt;
    public Key OverlayVisibility = Key.Subtract;

    public GamepadButtonFlags ControllerUp = GamepadButtonFlags.DPadUp;
    public GamepadButtonFlags ControllerDown = GamepadButtonFlags.DPadDown;
    public GamepadButtonFlags ControllerLeft = GamepadButtonFlags.DPadLeft;
    public GamepadButtonFlags ControllerRight = GamepadButtonFlags.DPadRight;
    public GamepadButtonFlags ControllerConfirm = GamepadButtonFlags.LeftThumb;
    public GamepadButtonFlags ControllerLeave = GamepadButtonFlags.RightThumb;
    public GamepadButtonFlags ControllerRapidAdjust = GamepadButtonFlags.LeftShoulder;
    public GamepadButtonFlags ControllerOverlayVisibility = GamepadButtonFlags.RightShoulder;

    #endregion

    #region Handling

    public Key JumpHack = Key.LeftCtrl;
    public Key BrakeHack = Key.Space;
    public Key Velocity = Key.LeftShift;
    public Key WheelSpeed = Key.W;

    public GamepadButtonFlags JumpHackController = GamepadButtonFlags.RightThumb;
    public GamepadButtonFlags BrakeHackController = GamepadButtonFlags.A;
    public GamepadButtonFlags VelocityController = GamepadButtonFlags.LeftShoulder;
    public GamepadButtonFlags WheelSpeedHackController = GamepadButtonFlags.LeftShoulder;

    #endregion

    public static void ChangeKeybinding(ref Button button, Key key)
    {
        var fields = typeof(Keybindings).GetFields(BindingFlags.Public).Where(f => f.FieldType == typeof(Key));
        var cleanButtonName = button.Name.Replace("Button", string.Empty);
        foreach (var field in fields)
        {
            if (field.Name != cleanButtonName)
            {
                continue;
            }
            
            field.SetValue(MainWindow.Mw.Keybindings, key);
        }
    }
}