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
using static Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs.HandlingKeybindings;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Features;

public abstract class Braking
{
    public abstract class StopAllWheels : FeatureBase
    {
        private const int MinStrength = 1;
        private const int MaxStrength = 10;
        
        [Flags]
        private enum Mode
        {
            Instant,
            Gradual,
            Pulse,
            Linear,
            Random
        };

        private static Mode _stopMode = Mode.Instant;
        public static void SetMode(int index)
        {
            _stopMode = (Mode)index;
        }

        private static float _strength;
        public static void SetStrength(double? newValue)
        {
            _strength = ToSingle(newValue);
        }
        
        private static int _interval;
        public static void SetInterval(double? newValue)
        {
            _interval = ToInt32(newValue);
        }

        public static void Run()
        {
            if (!IsProcessValid())
            {
                return;
            }
            
            while (true)
            {
                if (!Shp.Dispatcher.Invoke(() => Shp.StopAllWheelsSwitch.IsOn))
                {
                    return;
                }

                if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow() || CarSpeed < 10)
                {
                    Task.Delay(_interval).Wait();
                    continue;
                }

                if (!IsKeyPressed(Hk.BrakeHack) && !Mw.Gamepad.IsButtonPressed(BrakeHackController))
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
                        ApplyGradualWheelStop(_strength);
                        break;
                    }
                    case Mode.Random:
                    {
                        ApplyRandomWheelStop(_strength);
                        break;
                    }
                    case Mode.Pulse:
                    {
                        ApplyPulseWheelStop(_strength);
                        break;
                    }
                    case Mode.Linear:
                    {
                        ApplyLinearWheelStop(_strength);
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
        
        private static void ApplyGradualWheelStop(float strength)
        {
            strength = Max(MinStrength, Min(strength, MaxStrength));
            var adjustmentFactor = 1.0f - strength * 0.1f;
            CarEntity.WheelSpeed *= adjustmentFactor;
        }
        
        private static void ApplyRandomWheelStop(float strength)
        {
            strength = Max(MinStrength, Min(strength, MaxStrength));
            var random = new Random();
            var randomFactor = random.Next(1, ToInt32(strength + 1)) * 0.1f;
            CarEntity.WheelSpeed *= 1.0f - randomFactor;
        }
        
        private static void ApplyPulseWheelStop(float strength)
        {
            strength = Max(MinStrength, Min(strength, MaxStrength));
            const double frequency = 0.002;
            const double amplitude = 0.5;
            var pulsatingFactor = ToSingle(Sin(Now.Millisecond * frequency * strength) * amplitude + amplitude);
            CarEntity.WheelSpeed *= pulsatingFactor;
        }
        
        private static void ApplyLinearWheelStop(float strength)
        {
            strength = Max(1, Min(strength, 10));
            var linearFactor = 1.0f - strength * 0.05f;
            linearFactor = Max(0.0f, linearFactor);
            CarEntity.WheelSpeed *= linearFactor;
        }
    }

    public abstract class SuperBrake : FeatureBase
    {
        private static float _strength = 0.95f;
        public static void SetStrength(double? newValue)
        {
            _strength = ToSingle(newValue);
        }
        
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

                if (!IsKeyPressed(Hk.BrakeHack) && !Mw.Gamepad.IsButtonPressed(BrakeHackController))
                {
                    Task.Delay(25).Wait();
                    continue;
                }
                
                LinearVelocity = LinearVelocity with
                {
                    X = LinearVelocity.X * _strength,
                    Y = LinearVelocity.Y - 0.05f,
                    Z = LinearVelocity.Z * _strength
                };
                Task.Delay(10).Wait();
            }
        }
    }
}