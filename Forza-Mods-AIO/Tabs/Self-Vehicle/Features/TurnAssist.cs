using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Input;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

using static System.Math;
using static System.Convert;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Resources.DllImports;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;
using KeyStates = Forza_Mods_AIO.Resources.KeyStates;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Features;

public abstract class TurnAssist : FeatureBase
{
    private static int _interval;
    private static float _ratio;
    private static float _strength;
    private static Mode _mode = Mode.Basic;

    private enum Mode
    {
        Basic = 0,
        Advanced
    }
    
    public static void SetInterval(double? newValue) => _interval = ToInt32(newValue);
    public static void SetRatio(double? newValue) => _ratio = ToSingle(newValue);
    public static void SetStrength(double? newValue) => _strength = ToSingle(newValue) / 10;
    public static void SetMode(int newValue) => _mode = (Mode)newValue;
    
    public static void Run()
    {
        while (true)
        {
            if (!IsProcessValid() || !Shp.Dispatcher.Invoke(() => Shp.TurnAssistSwitch.IsOn))
            {
                return;
            }

            if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow())
            {
                Task.Delay(_interval).Wait();
                continue;
            }
            
            switch (_mode)
            {
                case Mode.Basic:
                    BasicTurnAssist();
                    break;
                case Mode.Advanced:
                    AdvancedTurnAssist();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Task.Delay(_interval).Wait();
        }
    }

    private static void BasicTurnAssist()
    {
        var steeringInput = GetSteeringInput();
        switch (steeringInput)
        {
            case < 0:
            {
                BasicLeft();
                break;
            }
            case > 0:
            {
                BasicRight();
                break;
            }
        }
    }

    private static void BasicLeft()
    {
        float frontLeft = CarEntity.WheelSpeed.X, frontRight = CarEntity.WheelSpeed.Y;
        float backLeft = CarEntity.WheelSpeed.Z, backRight = CarEntity.WheelSpeed.W;
        
        if (Abs(frontRight - frontLeft) < frontRight / _ratio && Abs(backRight - frontLeft) < backRight / _ratio)
        {
            frontLeft -= _strength;
            backLeft -= _strength;
            frontRight += _strength;
            backRight += _strength;
        }
        
        CarEntity.WheelSpeed = new Vector4(frontLeft, frontRight, backLeft, backRight);
    }

    private static void BasicRight()
    {
        float frontLeft = CarEntity.WheelSpeed.X, frontRight = CarEntity.WheelSpeed.Y;
        float backLeft = CarEntity.WheelSpeed.Z, backRight = CarEntity.WheelSpeed.W;

        if (Abs(frontLeft - frontRight) < frontLeft / _ratio && Abs(backLeft - frontRight) < backLeft / _ratio)
        {
            frontRight -= _strength;
            backRight -= _strength;
            frontLeft += _strength;
            backLeft += _strength;
        }
        
        CarEntity.WheelSpeed = new Vector4(frontLeft, frontRight, backLeft, backRight);
    }
    
    private static void AdvancedTurnAssist()
    {
        var steeringInput = GetSteeringInput();
        switch (steeringInput)
        {
            case < 0:
            {
                AdvancedLeft(ref steeringInput);
                break;
            }
            case > 0:
            {
                AdvancedRight(ref steeringInput);
                break;
            }
        }
    }
    
    private const int VelocityChangeDivider = 25;
    private const int WheelSpeedChangeDivider = 10;

#if !RELEASE
    private static int _leftRunCount;
#endif
    
    private static void AdvancedLeft(ref float steerInput)
    {
        var frontLeft = CarEntity.WheelSpeed.X;
        var frontRight = CarEntity.WheelSpeed.Y;
        var backLeft = CarEntity.WheelSpeed.Z; 
        var backRight = CarEntity.WheelSpeed.W;
        
        var speedDiffFront = frontRight - frontLeft;
        var speedDiffBack = backRight - backLeft;

        if (!(Abs(speedDiffFront) < frontRight / _ratio) || !(Abs(speedDiffBack) < backRight / _ratio)) return;
        
        var linVelMagnitude = CarEntity.LinearVelocity.Length();
        var wheelSpeedChange = _strength * (linVelMagnitude / WheelSpeedChangeDivider);
        
        frontLeft -= wheelSpeedChange;
        backLeft -= wheelSpeedChange;
        frontRight += wheelSpeedChange;
        backRight += wheelSpeedChange;
            
        var linearVelocityChange = Vector3.Transform(new Vector3(-wheelSpeedChange * 0.5f, 0, 0), CarEntity.Rotation);
        var newLinVel = CarEntity.LinearVelocity + linearVelocityChange / VelocityChangeDivider;
        
        var angVelMagnitude = CarEntity.AngularVelocity.Length();
        var angVelChange = _strength * angVelMagnitude * steerInput / VelocityChangeDivider;
        var newAngVel = CarEntity.AngularVelocity + new Vector3(0, 0, angVelChange);
        
#if !RELEASE
        var linVelDiff = newLinVel - CarEntity.LinearVelocity;
        var angVelDiff = newAngVel - CarEntity.AngularVelocity;
        _leftRunCount++;
        Mw.Dispatcher.Invoke(() => Mw.DebugLabel.Text = $"Advanced turn assist left, run count: {_leftRunCount}\nLin Vel Diff: {linVelDiff}, Ang Vel Diff: {angVelDiff}");
#endif
       
        CarEntity.WheelSpeed = new Vector4(frontLeft, frontRight, backLeft, backRight);
        CarEntity.LinearVelocity = newLinVel;
        CarEntity.AngularVelocity = newAngVel;
    }
    
#if !RELEASE
    private static int _rightRunCount;
#endif
    
    private static void AdvancedRight(ref float steerInput)
    {
        var frontLeft = CarEntity.WheelSpeed.X;
        var frontRight = CarEntity.WheelSpeed.Y;
        var backLeft = CarEntity.WheelSpeed.Z; 
        var backRight = CarEntity.WheelSpeed.W;
        
        var speedDiffFront = frontRight - frontLeft;
        var speedDiffBack = backRight - backLeft;

        if (!(Abs(speedDiffFront) < frontLeft / _ratio) || !(Abs(speedDiffBack) < backLeft / _ratio)) return;
        
        var linVelMagnitude = CarEntity.LinearVelocity.Length();
        var wheelSpeedChange = _strength * (linVelMagnitude / WheelSpeedChangeDivider);
        
        frontRight -= wheelSpeedChange;
        backRight -= wheelSpeedChange;
        frontLeft += wheelSpeedChange;
        backLeft += wheelSpeedChange;
        
        var linearVelocityChange = Vector3.Transform(new Vector3(wheelSpeedChange, 0, 0), CarEntity.Rotation);
        var newLinVel = CarEntity.LinearVelocity + linearVelocityChange / VelocityChangeDivider;
        
        var angVelMagnitude = CarEntity.AngularVelocity.Length();
        var angVelChange = _strength * angVelMagnitude * steerInput / VelocityChangeDivider;
        var newAngVel = CarEntity.AngularVelocity + new Vector3(0, 0, angVelChange);
        
#if !RELEASE
        var linVelDiff = newLinVel - CarEntity.LinearVelocity;
        var angVelDiff = newAngVel - CarEntity.AngularVelocity;
        _rightRunCount++;
        Mw.Dispatcher.Invoke(() => Mw.DebugLabel.Text = $"Advanced turn assist right, run count: {_rightRunCount}\nLin Vel Diff: {linVelDiff}, Ang Vel Diff: {angVelDiff}");
#endif
        
        CarEntity.WheelSpeed = new Vector4(frontLeft, frontRight, backLeft, backRight);
        CarEntity.LinearVelocity = newLinVel;
        CarEntity.AngularVelocity = newAngVel;
    }

    private static float GetSteeringInput()
    {
        return KeyStates.IsKeyPressed(Key.A) ? -1 : KeyStates.IsKeyPressed(Key.D) ? 1 : 0;
    }
}