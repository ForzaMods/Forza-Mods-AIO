using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Controls;
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

public abstract class WheelSpeed : FeatureBase
{
    private static float _boostFactor;

    public static void SetBoostFactor(double? newValue)
    {
        _boostFactor = ToSingle(newValue);
    }
    
    public static void Run()
    {
        if (!IsProcessValid())
        {
            return;
        }

        while (true)
        {
            if (!Shp.Dispatcher.Invoke(() => Shp.WheelSpeedSwitch.IsOn))
            {
                return;
            }

            if (!IsKeyPressed(Hk.WheelspeedHack) && !Mw.Gamepad.IsButtonPressed(WheelspeedHackController))
            {
                Task.Delay(25).Wait();
                continue;
            }

            if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow())
            {
                Task.Delay(25).Wait();
                continue;
            }
            
            var mode = Shp.Dispatcher.Invoke(() => ((ComboBoxItem)Shp.WheelSpeedModeBox.SelectedItem).Content.ToString());
            var interval = Shp.Dispatcher.Invoke(() => ToInt32(Shp.IntervalBox.Value));

            CallWheelSpeed(mode);
            Task.Delay(interval).Wait();
        }
    }

    private static void CallWheelSpeed(string? mode)
    {
        if (mode == null)
        {
            throw new ArgumentException(@"Mode cant be null", nameof(mode));
        }
        
        switch (mode)
        {
            case "Static":
            {
                StaticWheelSpeed();
                break;
            }
                        
            case "Linear":
            {
                LinearWheelSpeed();
                break;
            }
                    
            case "Power":
            {
                PowerWheelSpeed();
                break;
            }
                    
            case "Random":
            {
                RandomWheelSpeed();
                break;
            }
                    
            case "Jitter":
            {
                JitterWheelSpeed();
                break;
            }
                    
            case "Pulse":
            {
                PulseWheelSpeed();
                break;
            }
                    
            case "Sway":
            {
                SwayWheelSpeed();
                break;
            }
                    
            case "Surge":
            {
                SurgeWheelSpeed();
                break;
            }
                    
            case "Mixed":
            {
                MixedWheelSpeed();
                break;
            }
                    
            case "Accel":
            {
                AccelWheelSpeed();
                break;
            }
        }
        
    }
    
    private static void StaticWheelSpeed()
    {
        var currentWheelSpeed = AverageWheelSpeed;
        
        var boostStrength = _boostFactor / 10;
        
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }
        
        var boost = currentWheelSpeed + boostStrength;
        CarEntity.WheelSpeed = new Vector4(ToSingle(boost));
    }

    private static void LinearWheelSpeed()
    {
        var currentWheelSpeed = AverageWheelSpeed;
        
        var boostStrength = _boostFactor / 10 - 1 + (currentWheelSpeed - 100) / 100 * -5;
        
        if (boostStrength <= 0)
        {
            boostStrength = 0;
        }
        
        var boost = currentWheelSpeed + boostStrength;
        CarEntity.WheelSpeed = new Vector4(ToSingle(boost));
    }
    
    private static void PowerWheelSpeed()
    {
        var currentWheelSpeed = AverageWheelSpeed;

        const int exponent = 2;
        const float speedFactor = 0.35f;
    
        var boostStrength = Pow(_boostFactor / (_boostFactor / (5 * speedFactor)), exponent);
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }

        var boost = currentWheelSpeed + boostStrength;
        CarEntity.WheelSpeed = new Vector4(ToSingle(boost));
    }

    private static void RandomWheelSpeed()
    {
        var currentWheelSpeed = AverageWheelSpeed;
        
        var boostStrength = ToSingle(new Random().NextDouble() * _boostFactor / 5 + 1);
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }
        
        var boost = currentWheelSpeed + boostStrength;
        CarEntity.WheelSpeed = new Vector4(ToSingle(boost));
    }

    private static void JitterWheelSpeed()
    {
        var currentWheelSpeed = AverageWheelSpeed;

        var jitterAmplitude = _boostFactor / 20;
        var boostStrength = ToSingle(new Random().NextDouble() * 2 - 1) * jitterAmplitude;
        
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }
        
        var boost = currentWheelSpeed + boostStrength;
        CarEntity.WheelSpeed = new Vector4(ToSingle(boost));
    }


    private static void PulseWheelSpeed()
    {
        var currentWheelSpeed = AverageWheelSpeed;

        const float pulseFrequency = 0.25f;
        var pulseEffect = 0.5f + 0.5f * ToSingle(Sin(pulseFrequency * Now.Millisecond));
        var boostStrength = _boostFactor / 10 * pulseEffect;
        
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }

        var boost = currentWheelSpeed + boostStrength;
        CarEntity.WheelSpeed = new Vector4(ToSingle(boost));
    }

    private static void SwayWheelSpeed()
    {
        var currentWheelSpeed = AverageWheelSpeed;

        const double sway = 3;
        var boostStrength = ToSingle(new Random().NextDouble() * sway - 1) * _boostFactor / 10;
        
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }
        
        var boost = currentWheelSpeed + boostStrength;
        CarEntity.WheelSpeed = new Vector4(ToSingle(boost));
    }

    private static void AccelWheelSpeed()
    {
        var currentWheelSpeed = AverageWheelSpeed;

        const float acceleration = 0.02f;
        var boostStrength = acceleration * (_boostFactor / 250) * Now.Millisecond;

        if (boostStrength < 0)
        {
            boostStrength = 0;
        }

        var boost = currentWheelSpeed + boostStrength;
        CarEntity.WheelSpeed = new Vector4(ToSingle(boost));
    }
    
    private static void SurgeWheelSpeed()
    {
        var currentWheelSpeed = AverageWheelSpeed;

        const float frequency = 0.25f; 
        var amplitude = _boostFactor / 10;
        var boostStrength = 2 * amplitude * ToSingle(Asin(Sin(2 * PI * frequency * Now.Millisecond)) / (2 * PI));
        
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }
        
        var boost = currentWheelSpeed + boostStrength;
        CarEntity.WheelSpeed = new Vector4(ToSingle(boost));
    }

    private static void MixedWheelSpeed()
    {
        var currentWheelSpeed = AverageWheelSpeed;

        const float frequency = 0.75f;
        var amplitude = _boostFactor / 10;
        var cosValue = ToSingle(Cos(frequency * Now.Millisecond));
        var sinValue = ToSingle(Sin(frequency * Now.Millisecond));
        var powValue = ToSingle(Pow(cosValue * sinValue, 2));

        var boostStrength = amplitude * powValue;
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }
        
        var boost = currentWheelSpeed + boostStrength;
        CarEntity.WheelSpeed = new Vector4(ToSingle(boost));
    }
}