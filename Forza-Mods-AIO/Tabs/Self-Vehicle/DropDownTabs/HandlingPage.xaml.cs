using System;
using System.Globalization;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs;
using static System.Convert;
using static System.DateTime;
using static System.Math;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Resources.DllImports;
using static Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs.HandlingKeybindings;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.Entities.CarEntity;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

/// <summary>
/// Interaction logic for SpeedHacksPage.xaml
/// </summary>
public partial class HandlingPage
{
    public static HandlingPage Shp { get; private set; } = null!;
    public static readonly Detour FlyHackDetour = new();
    
    private readonly byte[] _before1 = { 0x0F, 0x11, 0x41, 0x10 },
                            _before2 = { 0x0F, 0x11, 0x49, 0x20 },
                            _before3 = { 0x0F, 0x11, 0x41, 0x30 },
                            _before4 = { 0x0F, 0x11, 0x49, 0x40 },
                            _before5 = { 0x0F, 0x11, 0x41, 0x50 };
    private readonly byte[] _nop = { 0x90, 0x90, 0x90, 0x90 };
        
    public HandlingPage()
    {
        InitializeComponent();
        Shp = this;
    }

    private void VelocitySwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (!VelocitySwitch.IsOn || !Mw.Attached)
        {
            return;
        }
            
        Task.Run(() =>
        {
            while (true)
            {
                var toggled = true;
                Dispatcher.Invoke(() => toggled = VelocitySwitch.IsOn);
                if (!toggled)
                {
                    break;
                }

                if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow())
                {
                    Task.Delay(25).Wait();
                    continue;
                }

                if (GetAsyncKeyState(Hk.VelHack) is not (1 or short.MinValue) &&
                    !Mw.Gamepad.IsButtonPressed(VelHackController))
                {
                    Task.Delay(25).Wait();
                    continue;
                }

                var speed = GetCarSpeed();

                double limit = 0;
                Dispatcher.Invoke(delegate
                {
                    limit = ToDouble(VelLimitBox.Value);
                });
                
                if (speed > limit)
                {
                    Task.Delay(25).Wait();
                    continue;   
                }
                
                var multiply = 1f;
                var mode = "";
                const int baseInterval = 100;
                var interval = baseInterval;
                
                Dispatcher.Invoke(delegate
                {
                    mode = ((ComboBoxItem)VelModeBox.SelectedItem).Content.ToString();
                });

                switch (mode)
                {
                    case "Dynamic":
                    {
                        Dispatcher.Invoke(() => multiply += ToSingle(VelValueNum.Value) / ToSingle(VelLimitBox.Value / 3));
                        interval = ToInt32(baseInterval + multiply);
                        break;
                    }
                    case "Direct":
                    {
                        Dispatcher.Invoke(() => multiply += ToSingle(VelValueNum.Value) / 10);
                        break;
                    }
                }  
                
                LinearVelocity = LinearVelocity with
                {
                    X = LinearVelocity.X * multiply,
                    Z = LinearVelocity.Z * multiply
                };
                Task.Delay(interval).Wait();
            }
        });
    }

    private void WheelSpeedSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (!WheelSpeedSwitch.IsOn || !Mw.Attached)
        {
            return;
        }
            
        Task.Run(() =>
        {
            while (true)
            {
                var toggled = true;
                Dispatcher.Invoke(delegate { toggled = WheelSpeedSwitch.IsOn; });
                if (!toggled)
                {
                    break;
                }

                if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow())
                {
                    Task.Delay(25).Wait();
                    continue;
                }


                if (GetAsyncKeyState(Hk.WheelspeedHack) is not (1 or short.MinValue) &&
                    !Mw.Gamepad.IsButtonPressed(WheelspeedHackController))
                {
                    Task.Delay(25).Wait();
                    continue;
                }
                var mode = "";
                var interval = 1;
                
                Dispatcher.Invoke(delegate
                {
                    mode = ((ComboBoxItem)WheelSpeedModeComboBox.SelectedItem).Content.ToString();
                    interval = ToInt32(IntervalBox.Value);
                });
                
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
                Task.Delay(interval).Wait();
            }
        });
    }

    private static double GetCarSpeed()
    {
        var velocitySquaredSum = Pow(LinearVelocity.X, 2) + Pow(LinearVelocity.Y, 2) + Pow(LinearVelocity.Z, 2);
        var speedMetersPerSecond = Sqrt(velocitySquaredSum);
        var speedMilesPerHour = speedMetersPerSecond * 2.23694;
        return speedMilesPerHour;
    }

    private void StaticWheelSpeed()
    {
        float currentWheelSpeed = WheelSpeed.X, boostFactor = 0;
        Dispatcher.Invoke(() => boostFactor = ToSingle(StrengthBox.Value));
        var boostStrength = boostFactor / 10;
        
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }
        
        var boost = currentWheelSpeed + boostStrength;
        WheelSpeed = new Vector4(boost);
    }

    private void LinearWheelSpeed()
    {
        float currentWheelSpeed = WheelSpeed.X, boostFactor = 0;
        Dispatcher.Invoke(() => boostFactor = ToSingle(StrengthBox.Value));
        var boostStrength = boostFactor / 10 - 1 + (currentWheelSpeed - 100) / 100 * -5;
        
        if (boostStrength <= 0)
        {
            boostStrength = 0;
        }
        
        var boost = currentWheelSpeed + boostStrength;
        WheelSpeed = new Vector4(boost);
    }
    
    private void PowerWheelSpeed()
    {
        float currentWheelSpeed = WheelSpeed.X, boostFactor = 0;
        Dispatcher.Invoke(() => boostFactor = ToSingle(StrengthBox.Value));

        const int exponent = 2;
        const float speedFactor = 0.35f;
    
        var boostStrength = Pow(boostFactor / (boostFactor / (5 * speedFactor)), exponent);
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }

        var boost = currentWheelSpeed + boostStrength;
        WheelSpeed = new Vector4(ToSingle(boost));
    }

    private void RandomWheelSpeed()
    {
        float currentWheelSpeed = WheelSpeed.X, boostFactor = 0;
        Dispatcher.Invoke(() => boostFactor = ToSingle(StrengthBox.Value));
        
        var boostStrength = ToSingle(new Random().NextDouble() * boostFactor / 5 + 1);
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }
        
        var boost = currentWheelSpeed + boostStrength;
        WheelSpeed = new Vector4(boost);
    }

    private void JitterWheelSpeed()
    {
        float currentWheelSpeed = WheelSpeed.X, boostFactor = 0;
        Dispatcher.Invoke(() => boostFactor = ToSingle(StrengthBox.Value));

        var jitterAmplitude = boostFactor / 20;
        var boostStrength = ToSingle(new Random().NextDouble() * 2 - 1) * jitterAmplitude;
        
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }
        
        var boost = currentWheelSpeed + boostStrength;

        WheelSpeed = new Vector4(ToSingle(boost));
    }


    private void PulseWheelSpeed()
    {
        float currentWheelSpeed = WheelSpeed.X, boostFactor = 0;
        Dispatcher.Invoke(() => boostFactor = ToSingle(StrengthBox.Value));

        const float pulseFrequency = 0.25f;
        var pulseEffect = 0.5f + 0.5f * ToSingle(Sin(pulseFrequency * Now.Millisecond));
        var boostStrength = boostFactor / 10 * pulseEffect;
        
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }

        var boost = currentWheelSpeed + boostStrength;

        WheelSpeed = new Vector4(ToSingle(boost));
    }

    private void SwayWheelSpeed()
    {
        float currentWheelSpeed = WheelSpeed.X, boostFactor = 0;
        Dispatcher.Invoke(() => boostFactor = ToSingle(StrengthBox.Value));

        const double sway = 3;
        var boostStrength = ToSingle(new Random().NextDouble() * sway - 1) * boostFactor / 10;
        
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }
        
        var boost = currentWheelSpeed + boostStrength;
        WheelSpeed = new Vector4(ToSingle(boost));
    }

    private void AccelWheelSpeed()
    {
        float currentWheelSpeed = WheelSpeed.X, boostFactor = 0;
        Dispatcher.Invoke(() => boostFactor = ToSingle(StrengthBox.Value / 250));

        const float acceleration = 0.02f;
        var boostStrength = acceleration * boostFactor * Now.Millisecond;

        if (boostStrength < 0)
        {
            boostStrength = 0;
        }

        var boost = currentWheelSpeed + boostStrength;
        WheelSpeed = new Vector4(boost);
    }
    
    private void SurgeWheelSpeed()
    {
        float currentWheelSpeed = WheelSpeed.X, boostFactor = 0;
        Dispatcher.Invoke(() => boostFactor = ToSingle(StrengthBox.Value));

        const float frequency = 0.25f; 
        var amplitude = boostFactor / 10;
        var boostStrength = 2 * amplitude * ToSingle(Asin(Sin(2 * PI * frequency * Now.Millisecond)) / (2 * PI));
        
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }
        
        var boost = currentWheelSpeed + boostStrength;
        WheelSpeed = new Vector4(ToSingle(boost));
    }

    private void MixedWheelSpeed()
    {
        float currentWheelSpeed = WheelSpeed.X, boostFactor = 0;
        Dispatcher.Invoke(() => boostFactor = ToSingle(StrengthBox.Value));

        const float frequency = 0.75f;
        var amplitude = boostFactor / 10;
        var cosValue = ToSingle(Cos(frequency * Now.Millisecond));
        var sinValue = ToSingle(Sin(frequency * Now.Millisecond));
        var powValue = ToSingle(Pow(cosValue * sinValue, 2));

        var boostStrength = amplitude * powValue;
        if (boostStrength < 0)
        {
            boostStrength = 0;
        }
        
        var boost = currentWheelSpeed + boostStrength;
        WheelSpeed = new Vector4(ToSingle(boost));
    }

    public void PullButton_Click(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        if (sender.GetType().GetProperty("Name").GetValue(sender).ToString().Contains("Gravity"))
        {
            GravityValueNum.Value = Gravity;
        }
        else
        {
            AccelValueNum.Value = Acceleration;
        }
    }

    private void SetSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        var type = sender.GetType();
        
        if (!(bool)type.GetProperty("IsOn")?.GetValue(sender)!)
        {
            return;
        }

        var mode = ((string)type.GetProperty("Name")?.GetValue(sender)!).Contains("Gravity") ? "Gravity" : "Accel";
        var setSwitch = mode == "Gravity" ? GravitySetSwitch : AccelSetSwitch;
        var valueNum = mode == "Gravity" ? GravityValueNum : AccelValueNum;
        var original = mode == "Gravity" ? Gravity : Acceleration;
        var lastPlayerEnt = PlayerCarEntity;
        
        Task.Run(() =>
        {
            while (true)
            {
                Task.Delay(100).Wait();
                var toggled = true;
                Dispatcher.Invoke(() => toggled = setSwitch.IsOn);

                if (!toggled)
                {
                    _ = mode == "Gravity" ? Gravity = original : Acceleration = original;
                    Dispatcher.Invoke(() => valueNum.Value = ToDouble(original));
                    break;
                }
                
                if (Mw.Gvp.Process!.MainWindowHandle != GetForegroundWindow())
                {
                    Task.Delay(25).Wait();
                    continue;
                }
                
                if (lastPlayerEnt != BaseDetour.ReadVariable<UIntPtr>())
                {
                    if (Acceleration is < 0.2f and > 0f)
                    {
                        original = mode == "Gravity" ? Gravity : Acceleration;
                        lastPlayerEnt = BaseDetour.ReadVariable<UIntPtr>();
                    }
                    continue;
                }
                
                float setValue = 0;
                Dispatcher.Invoke(() => setValue = ToSingle(valueNum.Value));
                _ = mode == "Gravity" ? Gravity = setValue : Acceleration = setValue;
            }
        });
    }
    
    private void TurnAssistSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (!TurnAssistSwitch.IsOn || !Mw.Attached)
        {
            return;
        }

        Task.Run(() =>
        {
            while (true)
            {
                var toggled = false;
                Dispatcher.Invoke(() => toggled = TurnAssistSwitch.IsOn);

                if (!toggled)
                {
                    break;
                }

                if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow())
                {
                    Task.Delay(25).Wait();
                    continue;
                }

                float frontLeft = WheelSpeed.X, frontRight = WheelSpeed.Y; // Front
                float backLeft = WheelSpeed.Z, backRight = WheelSpeed.W; // Rear
                    
                var interval = 1;
                float ratio = 1f, strength = 1f;
                Dispatcher.Invoke(() =>
                {
                    interval = ToInt32(TurnAssistIntervalBox.Value);
                    ratio = ToSingle(TurnAssistRatioBox.Value);
                    strength = ToSingle(TurnAssistStrengthBox.Value);
                });
                    
                if (GetAsyncKeyState(Keys.A) is 1 or Int16.MinValue)
                {
                    if (Abs(frontRight - frontLeft) < frontRight / ratio && Abs(backRight - frontLeft) < backRight / ratio)
                    {
                        frontLeft -= strength;
                        backLeft -= strength;
                        frontRight += strength;
                        backRight += strength;
                    }
                }
                else if (GetAsyncKeyState(Keys.D) is 1 or Int16.MinValue)
                {
                    if (Abs(frontLeft - frontRight) < frontLeft / ratio && Abs(backLeft - frontRight) < backLeft / ratio)
                    {
                        frontRight -= strength;
                        backRight -= strength;
                        frontLeft += strength;
                        backLeft += strength;
                    }
                }

                WheelSpeed = new Vector4(frontLeft, frontRight, backLeft, backRight);
                Task.Delay(interval).Wait();
            }
        });
    }

    private void SuperCarSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        if (SuperCarSwitch.IsOn)
        {
            if (Mw.Gvp.Name == "Forza Horizon 5")
            {
                Mw.M.WriteArrayMemory((SuperCarAddr - 4).ToString("X"), _nop);
            }

            Mw.M.WriteArrayMemory((SuperCarAddr + 4).ToString("X"), _nop);
            Mw.M.WriteArrayMemory((SuperCarAddr + 12).ToString("X"), _nop);
            Mw.M.WriteArrayMemory((SuperCarAddr + 20).ToString("X"), _nop);
            Mw.M.WriteArrayMemory((SuperCarAddr + 32).ToString("X"), _nop);
                
        }
        else
        {
            if (Mw.Gvp.Name == "Forza Horizon 5")
            {
                Mw.M.WriteArrayMemory((SuperCarAddr - 4).ToString("X"), _before1);
            }
                
            Mw.M.WriteArrayMemory((SuperCarAddr + 4).ToString("X"), Mw.Gvp.Name == "Forza Horizon 4" ? _before1 : _before2);
            Mw.M.WriteArrayMemory((SuperCarAddr + 12).ToString("X"), Mw.Gvp.Name == "Forza Horizon 4" ? _before2 : _before3);
            Mw.M.WriteArrayMemory((SuperCarAddr + 20).ToString("X"), Mw.Gvp.Name == "Forza Horizon 4" ? _before3 : _before4);
            Mw.M.WriteArrayMemory((SuperCarAddr + 32).ToString("X"), Mw.Gvp.Name == "Forza Horizon 4" ? _before4 : _before5);
        }
    }

    private void WaterDragSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        Mw.M.WriteMemory(WaterAddr, WaterDragSwitch.IsOn ? new Vector3(0f, 0f, 0f) : new Vector3(0f, 3700f, 13500f));
    }

    private void SuperBrakeSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (!SuperBrakeSwitch.IsOn || !Mw.Attached)
            return;
            
        Task.Run(() =>
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-GB");

            while (true)
            {
                var toggled = true;
                Dispatcher.Invoke(delegate { toggled = SuperBrakeSwitch.IsOn; });
                
                if (!toggled)
                {
                    break;
                }

                if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow())
                {
                    Task.Delay(25).Wait();
                    continue;
                }

                if (GetCarSpeed() < 10)
                {
                    Task.Delay(25).Wait();
                    continue;
                }

                if (GetAsyncKeyState(Hk.BrakeHack) is not (1 or short.MinValue) &&
                    !Mw.Gamepad.IsButtonPressed(BrakeHackController))
                {
                    Task.Delay(25).Wait();
                    continue;
                }
                
                LinearVelocity = LinearVelocity with
                {
                    X = LinearVelocity.X * 0.95f,
                    Z = LinearVelocity.Z * 0.95f
                };
                Task.Delay(10).Wait();
            }
        });
    }

    private void StopAllWheelsSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!StopAllWheelsSwitch.IsOn || !Mw.Attached)
        {
            return;
        }

        Task.Run(() =>
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-GB");
                
            while (true)
            {
                var toggled = true;
                Dispatcher.Invoke(delegate { toggled = StopAllWheelsSwitch.IsOn; });
                if (!toggled)
                {
                    break;
                }

                if (Mw.Gvp.Process!.MainWindowHandle != GetForegroundWindow() || GetCarSpeed() < 10)
                {
                    Task.Delay(25).Wait();
                    continue;
                }

                if (GetAsyncKeyState(Hk.BrakeHack) is not (1 or short.MinValue) &&
                    !Mw.Gamepad.IsButtonPressed(BrakeHackController))
                {
                    Task.Delay(25).Wait();
                    continue;
                }
                
                WheelSpeed = new Vector4(0f);
                Task.Delay(10).Wait();
            }
        });
    }
        
    private float _originalGrav;
        
    private void FlyHackSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || FlyHackSwitch == null)
        {
            return;
        }
        
        if (Mw.Gvp.Name.Contains('4'))
        {
            Detour.FailedHandler(sender, FlyHackSwitch_OnToggled, true);
            return;
        }
        
        GravitySetSwitch.IsEnabled = !GravitySetSwitch.IsEnabled;
        if (!FlyHackSwitch.IsOn)
        {
            Gravity = _originalGrav;
            return;
        }

        if (GravitySetSwitch.IsOn)
        {
            GravitySetSwitch.IsOn = false;
        }
        
        _originalGrav = Gravity;
        FlyHackDetour.UpdateVariable(BitConverter.GetBytes(PlayerCarEntity));
        Gravity = 0f;
            
        Task.Run(() =>
        {
            bool aDown = false, dDown = false;
            bool wDown = false, sDown = false, shiftDown = false, controlDown = false;

            while (true)
            {
                var toggled = true;
                Dispatcher.Invoke(() => toggled = FlyHackSwitch.IsOn);
                    
                if (!toggled)
                {
                    break;
                }

                LinearVelocity = new Vector3 { X = 0f, Z = 0f, Y = 0f };
                AngularVelocity = new Vector3 { X = 0f, Z = 0f, Y = 0f };
                
                if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow())
                {
                    Task.Delay(25).Wait();
                    continue;
                }

                UpdateRotationKeyStates(ref aDown, ref dDown);
                UpdateMovementKeyStates(ref wDown, ref sDown, ref shiftDown, ref controlDown);

                if (aDown || dDown)
                {
                    var flyHackRotSpeed = 1f;
                    Dispatcher.Invoke(() => flyHackRotSpeed = ToSingle(FlyHackRotSpeedNum.Value / 2));
                    HandleRotation(flyHackRotSpeed, aDown);
                }

                if (wDown || sDown || shiftDown || controlDown)
                {
                    var angle = (float)((float)Atan2(Rotation.M13, Rotation.M11) * (180 / PI));
                    if (angle < 0)
                    {
                        angle += 360;
                    }
                    
                    var flyHackMoveSpeed = Dispatcher.Invoke(() => (float)FlyHackMoveSpeedNum.Value / 2);
                    HandleMovement(angle, flyHackMoveSpeed, wDown, sDown, shiftDown, controlDown);
                }
                
                Task.Delay(10).Wait();
            }
        });
    }
    
    private static void HandleRotation(float speed, bool aDown)
    {
        var angle = (aDown ? -1 : 1) * speed / 10;
        var rotationQuaternion = Quaternion.CreateFromAxisAngle(Vector3.UnitY, angle);
        var rotationMatrix = Matrix4x4.CreateFromQuaternion(rotationQuaternion);
        Rotation *= rotationMatrix;
    }

    private static void HandleMovement(float angle, float speed, bool wDown, bool sDown, bool shiftDown, bool controlDown)
    {
        if (wDown)
        {
            float xComp = 0f, zComp = 0f;
                        
            switch (angle)
            {
                // Top Left
                case < 90:
                {
                    xComp = -(float)Sin(PI * angle / 180);
                    zComp = (float)Cos(PI * angle / 180);
                    break;
                }
                // Bottom Left
                case > 90 and < 180:
                {
                    xComp = -(float)Sin(PI * (180 - angle) / 180);
                    zComp = -(float)Cos(PI * (180 - angle) / 180);
                    break;
                }
                // Bottom Right
                case > 180 and < 270:
                {
                    xComp = (float)Cos(PI * (270 - angle) / 180);
                    zComp = -(float)Sin(PI * (270 - angle) / 180);
                    break;
                }
                // Top Right
                case > 270:
                {
                    xComp = (float)Sin(PI * (360 - angle) / 180);
                    zComp = (float)Cos(PI * (360 - angle) / 180);
                    break;
                }
            }
                    
            Position = Position with
            {
                X = Position.X + speed * 5 * xComp,
                Z = Position.Z + speed * 5 * zComp
            };
        }
        else if (sDown)
        {
            float xComp = 0f, zComp = 0f;
                        
            switch (angle)
            {
                // Top Left
                case < 90:
                {
                    xComp = (float)Sin(PI * angle / 180);
                    zComp = -(float)Cos(PI * angle / 180);
                    break;
                }
                // Bottom Left
                case > 90 and < 180:
                {
                    xComp = (float)Sin(PI * (180 - angle) / 180);
                    zComp = (float)Cos(PI * (180 - angle) / 180);
                    break;
                }
                // Bottom Right
                case > 180 and < 270:
                {
                    xComp = -(float)Cos(PI * (270 - angle) / 180);
                    zComp = (float)Sin(PI * (270 - angle) / 180);
                    break;
                }
                // Top Right
                case > 270:
                {
                    xComp = -(float)Sin(PI * (360 - angle) / 180);
                    zComp = -(float)Cos(PI * (360 - angle) / 180);
                    break;
                }
            }
            Position = Position with
            {
                X = Position.X + speed * 5 * xComp,
                Z = Position.Z + speed * 5 * zComp
            };
        }
        if (shiftDown)
        {
            Position = Position with { Y = Position.Y + speed * 5 };
        }
        else if (controlDown)
        {
            Position = Position with { Y = Position.Y - speed * 5 };
        }
    }

    private static void UpdateMovementKeyStates(ref bool wDown, ref bool sDown, ref bool shiftDown, ref bool controlDown)
    {
        if (GetAsyncKeyState(Keys.W) is 1 or short.MinValue && !wDown)
        {
            wDown = true;
        }

        if (GetAsyncKeyState(Keys.W) is not 1 and not short.MinValue && wDown)
        {
            wDown = false;
        }

        if (GetAsyncKeyState(Keys.S) is 1 or short.MinValue && !sDown)
        {
            sDown = true;
        }

        if (GetAsyncKeyState(Keys.S) is not 1 and not short.MinValue && sDown)
        {
            sDown = false;
        }

        if (GetAsyncKeyState(Keys.LShiftKey) is 1 or short.MinValue && !shiftDown)
        {
            shiftDown = true;
        }

        if (GetAsyncKeyState(Keys.LShiftKey) is not 1 and not short.MinValue && shiftDown)
        {
            shiftDown = false;
        }

        if (GetAsyncKeyState(Keys.LControlKey) is 1 or short.MinValue && !controlDown)
        {
            controlDown = true;
        }

        if (GetAsyncKeyState(Keys.LControlKey) is not 1 and not short.MinValue && controlDown)
        {
            controlDown = false;
        }
    }
    
    private static void UpdateRotationKeyStates(ref bool aDown, ref bool dDown)
    {
        if (GetAsyncKeyState(Keys.A) is 1 or short.MinValue && !aDown)
        {
            aDown = true;
        }

        if (GetAsyncKeyState(Keys.A) is not 1 and not short.MinValue && aDown)
        {
            aDown = false;
        }

        if (GetAsyncKeyState(Keys.D) is 1 or short.MinValue && !dDown)
        {
            dDown = true;
        }

        if (GetAsyncKeyState(Keys.D) is not 1 and not short.MinValue && dDown)
        {
            dDown = false;
        }
    }
    
    private void CarNoclipSwitch_OnToggled(object sender, RoutedEventArgs e)
    {           
        if (!Mw.Attached)
        {
            return;
        }
        
        Bypass.DisableAntiCheat();
        if (!CarNoclipSwitch.IsOn)
        {
            Mw.M.WriteArrayMemory(Car1Addr, Mw.Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x84, 0xB5, 0x01, 0x00, 0x00 } : new byte[] { 0x0F, 0x84, 0x65, 0x03, 0x00, 0x00 });
            if (Mw.Gvp.Name != "Forza Horizon 4") return;
            Mw.M.WriteArrayMemory(Car2Addr, new byte[] { 0x0F, 0x84, 0x3A, 0x03, 0x00, 0x00});
            return;
        }

        Mw.M.WriteArrayMemory(Car1Addr, Mw.Gvp.Name == "Forza Horizon 4" ? new byte[] { 0xE9, 0xB6, 0x01, 0x00, 0x00, 0x90 } : new byte[] { 0xE9, 0x66, 0x03, 0x00, 0x00, 0x90 });
        if (Mw.Gvp.Name != "Forza Horizon 4") return;
        Mw.M.WriteArrayMemory(Car2Addr, new byte[] { 0xE9, 0x3B, 0x03, 0x00, 0x00, 0x90 });
    }

    private void WallNoclipSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        Bypass.DisableAntiCheat();
        if (!WallNoclipSwitch.IsOn)
        {
            Mw.M.WriteArrayMemory(Wall1Addr, Mw.Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x84, 0x29, 0x02, 0x00, 0x00 } : new byte[] { 0x0F, 0x84, 0x60, 0x02, 0x00, 0x00 } );
            Mw.M.WriteArrayMemory(Wall2Addr, Mw.Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x84, 0x2A, 0x02, 0x00, 0x00 } : new byte[] { 0x0F, 0x84, 0x7E, 0x02, 0x00, 0x00 } );
            return;
        }
        Mw.M.WriteArrayMemory(Wall1Addr, Mw.Gvp.Name == "Forza Horizon 4" ? new byte[] { 0xE9, 0x2A, 0x02, 0x00, 0x00, 0x90 } : new byte[] { 0xE9, 0x61, 0x02, 0x00, 0x00, 0x90 } );
        Mw.M.WriteArrayMemory(Wall2Addr, Mw.Gvp.Name == "Forza Horizon 4" ? new byte[] { 0xE9, 0x2B, 0x02, 0x00, 0x00, 0x90 } : new byte[] { 0xE9, 0x7F, 0x02, 0x00, 0x00, 0x90 } );
    }

    private void JumpHackSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        JumpHackVelocityNum.Value = Round(e.NewValue, 4);
    }

    private void JumpHackVelocityNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (JumpHackSwitch == null)
        {
            return;
        }
        
        JumpHackSlider.Value = (double)JumpHackVelocityNum.Value;
    }

    private void JumpHackSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!JumpHackSwitch.IsOn || !Mw.Attached)
        {
            return;
        }

        Task.Run(() =>
        {
            while (true)
            {
                var toggled = true;
                Dispatcher.Invoke(() => toggled = JumpHackSwitch.IsOn);
                    
                if (!toggled)
                {
                    break;
                }
                
                if (GetAsyncKeyState(Hk.JmpHack) is not (1 or short.MinValue) &&
                    !Mw.Gamepad.IsButtonPressed(JmpHackController))
                {
                    Task.Delay(25).Wait();
                    continue;
                }
                

                var jmpVal = 1f;
                Dispatcher.Invoke(() => jmpVal = ToSingle(JumpHackVelocityNum.Value));
                LinearVelocity = LinearVelocity with { Y = LinearVelocity.Y + jmpVal };

                while (GetAsyncKeyState(Hk.JmpHack) is 1 or short.MinValue ||
                       Mw.Gamepad.IsButtonPressed(JmpHackController))
                {
                    Task.Delay(50).Wait();
                }
            }
        });
    }

    private void VelValueNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        VelValueNum.Value = Round(ToDouble(e.NewValue), 2);
    }
}