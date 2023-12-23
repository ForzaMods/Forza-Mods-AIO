using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX.DirectInput;
using SharpDX.XInput;
using DeviceType = SharpDX.DirectInput.DeviceType;

namespace Forza_Mods_AIO.Resources;

public class Gamepad
{
    public Controller GetXInputController()
    {
        return _controller;
    }

    public Joystick GetDInputController()
    {
        return _joystick;
    }

    public bool IsControllerConnected { get; private set; }
    
    private Controller _controller = null!;
    private Joystick _joystick = null!;
    private Guid _joystickGuid = Guid.Empty;
    
    public static readonly Dictionary<int, string> DInputMap = new()
    {
        { 0, "X" },
        { 1, "Circle" },
        { 2, "Square" },
        { 3, "Triangle" },
        { 4, "LeftShoulder" },
        { 5, "RightShoulder" },
        { 6, "Select" },
        { 7, "Start" },
        { 8, "LeftStick" },
        { 9, "RightStick" }
    };
    
    public void InitializeControllersAndInput()
    {
        var controllers = new[]
        {
            new Controller(UserIndex.One), 
            new Controller(UserIndex.Two),
            new Controller(UserIndex.Three),
            new Controller(UserIndex.Four)
        };
        
        var directInput = new DirectInput();
        
        GetXInputControllers(controllers);

        if (_controller == null!)
        {
            IsControllerConnected = false;
            GetDInputControllers(directInput);
        }

        if (_controller == null! && _joystickGuid == Guid.Empty)
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
            _controller?.GetState();
        }
        catch
        {
            _controller = null!;
        }

        try
        {
            _joystick.Acquire();
            _joystick.Poll();
        }
        catch
        {
            _joystickGuid = Guid.Empty;
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
    
    private void GetDInputControllers(DirectInput directInput)
    {
        foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
        {
            _joystickGuid = deviceInstance.InstanceGuid;
        }

        if (_joystickGuid == Guid.Empty)
        {
            return;
        }
        
        _joystick.SetCooperativeLevel(IntPtr.Zero, CooperativeLevel.Background | CooperativeLevel.NonExclusive);
        _joystick = new Joystick(directInput, _joystickGuid);
    }

    public bool IsButtonPressed(string key)
    {
        if (_controller != null!)
        {
            return IsXInputButtonPressed(key);
        }

        return _joystickGuid != Guid.Empty && IsDInputButtonPressed(key);
    }

    private bool IsXInputButtonPressed(string key)
    {
        var button = (GamepadButtonFlags)Enum.Parse(typeof(GamepadButtonFlags), key);
        var state = _controller.GetState();
        return (state.Gamepad.Buttons & button) != 0;
    }
    
    private bool IsDInputButtonPressed(string key)
    {
        var state = _joystick.GetCurrentState();
        var controllerButtonState = state.Buttons;
        
        if (DInputMap.TryGetValue(DInputMap.SingleOrDefault(x => x.Value == key).Key, out _))
        {
            return controllerButtonState[DInputMap.SingleOrDefault(x => x.Value == key).Key];
        }
        
        return false;
    }
}