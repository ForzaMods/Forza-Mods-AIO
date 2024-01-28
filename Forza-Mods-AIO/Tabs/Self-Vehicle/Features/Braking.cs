using System;
using System.Numerics;
using System.Threading.Tasks;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

using static System.Math;
using static System.Convert;
using static System.DateTime;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Resources.KeyStates;
using static Forza_Mods_AIO.Resources.DllImports;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.Entities.CarEntity;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Features;

public abstract class Braking
{
    public abstract class StopAllWheels : FeatureBase
    {
        [Flags]
        private enum Mode
        {
            Instant,
            Gradual,
            Pulse,
            Linear,
            Random
        }

        private static int _strength;
        private static int _interval;
        private static Mode _stopMode = Mode.Instant;
        
        public static void SetMode(int index) =>  _stopMode = (Mode)index;
        public static void SetStrength(double? newValue) => _strength = ToInt32(newValue);
        public static void SetInterval(double? newValue) => _interval = ToInt32(newValue);

        public static void Run()
        {
            while (true)
            {
                if (!IsProcessValid())
                {
                    return;
                }

                if (!Shp.Dispatcher.Invoke(() => Shp.StopAllWheelsSwitch.IsOn))
                {
                    return;
                }

                if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow() || CarSpeed < 10)
                {
                    Task.Delay(_interval).Wait();
                    continue;
                }

                if (!IsKeyPressed(Mw.Keybindings.BrakeHack) && !Mw.Gamepad.IsButtonPressed(Mw.Keybindings.BrakeHackController))
                {
                    Task.Delay(_interval).Wait();
                    continue;
                }

                switch (_stopMode)
                {
                    case Mode.Instant:
                    {
                        ApplyInstantWheelStop();
                        break;
                    }
                    case Mode.Gradual:
                    {
                        ApplyGradualWheelStop();
                        break;
                    }
                    case Mode.Random:
                    {
                        ApplyRandomWheelStop();
                        break;
                    }
                    case Mode.Pulse:
                    {
                        ApplyPulseWheelStop();
                        break;
                    }
                    case Mode.Linear:
                    {
                        ApplyLinearWheelStop();
                        break;
                    }
                    default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                
                Task.Delay(_interval).Wait();
            }
        }

        private static void ApplyInstantWheelStop()
        {
            CarEntity.WheelSpeed = new Vector4(0);
        }
        
        private static void ApplyGradualWheelStop()
        {
            var adjustmentFactor = 1.0f - _strength * 0.1f;
            CarEntity.WheelSpeed *= adjustmentFactor;
        }
        
        private static void ApplyRandomWheelStop()
        {
            var random = new Random();
            var randomFactor = random.Next(1, _strength + 1) * 0.1f;
            CarEntity.WheelSpeed *= 1.0f - randomFactor;
        }
        
        private static void ApplyPulseWheelStop()
        {
            const double frequency = 0.002;
            const double amplitude = 0.5;
            var pulsatingFactor = ToSingle(Sin(Now.Millisecond * frequency * _strength) * amplitude + amplitude);
            CarEntity.WheelSpeed *= pulsatingFactor;
        }
        
        private static void ApplyLinearWheelStop()
        {
            _strength = Max(1, Min(_strength, 10));
            var linearFactor = 1.0f - _strength * 0.05f;
            linearFactor = Max(0.0f, linearFactor);
            CarEntity.WheelSpeed *= linearFactor;
        }
    }

    public abstract class SuperBrake : FeatureBase
    {
        private static float _strength = 0.95f;
        public static void SetStrength(double? newValue) => _strength = ToSingle(newValue);
        
        public static void Run()
        {
            if (!IsProcessValid())
            {
                return;
            }
            
            while (true)
            {
                if (!Shp.Dispatcher.Invoke(() => Shp.SuperBrakeSwitch.IsOn))
                {
                    break;
                }

                if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow() || CarSpeed < 10)
                {
                    Task.Delay(25).Wait();
                    continue;
                }

                if (!IsKeyPressed(Mw.Keybindings.BrakeHack) && !Mw.Gamepad.IsButtonPressed(Mw.Keybindings.BrakeHackController))
                {
                    Task.Delay(25).Wait();
                    continue;
                }
                
                LinearVelocity = LinearVelocity with
                {
                    X = LinearVelocity.X * _strength,
                    Z = LinearVelocity.Z * _strength
                };
                Task.Delay(10).Wait();
            }
        }
    }
}