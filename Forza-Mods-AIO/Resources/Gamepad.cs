using System;
using SharpDX.XInput;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Resources;

public class Gamepad
{
    public Controller GetXInputController()
    {
        InitializeControllersAndInput();
        return _controller;
    }

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
    
    public bool IsButtonPressed(string key)
    {
        return _controller != null! && IsXInputButtonPressed(key);
    }

    private bool IsXInputButtonPressed(string key)
    {
        var button = (GamepadButtonFlags)Enum.Parse(typeof(GamepadButtonFlags), key);
        var state = _controller.GetState();
        return (state.Gamepad.Buttons & button) != 0;
    }
}