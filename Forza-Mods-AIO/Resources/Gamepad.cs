using System;
using SharpDX.XInput;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Reflection.BindingFlags;

namespace Forza_Mods_AIO.Resources;

public class Gamepad
{
    public bool IsControllerConnected { get; private set; }
    private Controller _controller = null!;
    
    public void InitializeControllersAndInput()
    {
        var controllers = new[]
        {
            new Controller(UserIndex.One), 
            new Controller(UserIndex.Two),
            new Controller(UserIndex.Three),
            new Controller(UserIndex.Four)
        };
        
        GetXInputControllers(controllers);

        if (_controller == null!)
        {
            IsControllerConnected = false;
            return;
        }

        if (!IsControllerConnected)
        {
            IsControllerConnected = true;
        }
            
        try
        {
            _controller.GetState();
        }
        catch
        {
            _controller = null!;
        }
    }

    private void GetXInputControllers(IEnumerable<Controller> controllers)
    {
        foreach (var selectController in controllers)
        {
            if (!selectController.IsConnected) continue;
            _controller = selectController;
            break;
        }
    }
    
    public bool IsButtonPressed(GamepadButtonFlags key)
    {
        return _controller != null! && IsXInputButtonPressed(key);
    }

    private bool IsXInputButtonPressed(GamepadButtonFlags key)
    {
        var state = _controller.GetState();
        return (state.Gamepad.Buttons & key) != 0;
    }

    private bool _controllerReading;
    public async void GetAndSetXInputKey(ContentControl button)
    {
        if (_controllerReading)
        {
            return;
        }

        button.Content = "Change Key";
        var keyBuffer = GamepadButtonFlags.None;
        while (keyBuffer == GamepadButtonFlags.None)
        {
            InitializeControllersAndInput();

            if (!IsControllerConnected)
            {
                return;
            }

            foreach (GamepadButtonFlags buttonFlag in Enum.GetValues(typeof(GamepadButtonFlags)))
            {
                if (buttonFlag == GamepadButtonFlags.None)
                {
                    continue;
                }

                var isPressed = IsButtonPressed(buttonFlag);
                if (!isPressed)
                {
                    continue;
                }
                
                keyBuffer = buttonFlag;
                break;
            }

            await Task.Delay(5);
        }

        var cleanButtonName = button.Name.Replace("Button", string.Empty);
        var targetType = typeof(GamepadButtonFlags);
        var fields = typeof(Keybindings).GetFields(Public | Instance).Where(f => f.FieldType == targetType);
        foreach (var field in fields)
        {
            if (field.Name != cleanButtonName)
            {
                continue;
            }
            
            field.SetValue(MainWindow.Mw.Keybindings, keyBuffer);
            button.Content = keyBuffer;
            return;
        }

        _controllerReading = false;
    }
}