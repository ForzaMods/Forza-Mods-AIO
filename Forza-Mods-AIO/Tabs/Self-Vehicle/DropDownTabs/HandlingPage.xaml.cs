using System;
using System.Globalization;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Forza_Mods_AIO.Resources;
using static System.Convert;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Overlay.OverlayHandling;
using static Forza_Mods_AIO.Resources.DllImports;
using static Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs.HandlingKeybindings;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.Entities.CarEntity;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

/// <summary>
/// Interaction logic for SpeedHacksPage.xaml
/// </summary>
public partial class HandlingPage : Page
{
    public static HandlingPage? Shp;
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

    private void VelocitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        VelocityValueNum.Value = Math.Round(e.NewValue, 5);
    }

    private void VelocityValueNum_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (VelocitySwitch == null)
        {
            return;
        }

        VelocitySlider.Value = ToDouble(e.NewValue);
    }

    private void VelocitySwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (!VelocitySwitch.IsOn)
        {
            return;
        }
            
        Task.Run(() =>
        {
            while (true)
            {
                // TODO wrap for controlls / ishplement controls
                var toggled = true;
                Dispatcher.Invoke(() => toggled = VelocitySwitch.IsOn);
                if (!toggled)
                {
                    break;
                }

                if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow())
                {
                    Thread.Sleep(25);
                    continue;
                }
                
                float multiply = 1;
                Dispatcher.Invoke(() => multiply = (float)VelocityValueNum.Value);
                LinearVelocity = LinearVelocity with { X = LinearVelocity.X * multiply, Z = LinearVelocity.Z * multiply };
                Thread.Sleep(50);
            }
        });
    }

    private void WheelSpeedSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (!WheelSpeedSwitch.IsOn)
        {
            return;
        }
            
        Task.Run(() =>
        {
            while (true)
            {
                bool toggled = true;
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

                var mode = "";
                var interval = 1;
                
                Dispatcher.Invoke(delegate
                {
                    mode = WheelSpeedModeComboBox.SelectedItem.ToString();
                    interval = ToInt32(IntervalBox.Value);
                });
                
                switch (mode)
                {
                    case "Static" when GetAsyncKeyState(KbVelHack) is 1 or short.MinValue:
                    {
                        float currentWheelSpeed = WheelSpeed.X, boostStrength = 0;
                        Dispatcher.Invoke(() => boostStrength = ToSingle(StrengthBox.Value));
                        var boost = currentWheelSpeed + boostStrength / 10;
                        WheelSpeed = new Vector4 { X = boost, Y = boost, Z = boost, W = boost };
                        break;
                    }
                        
                    case "Linear" when GetAsyncKeyState(KbVelHack) is 1 or short.MinValue:
                    {
                        float currentWheelSpeed = WheelSpeed.X, boostFactor = 0;
                        Dispatcher.Invoke(() => boostFactor = ToSingle(StrengthBox.Value));
                        float boostStrength = boostFactor / 10 - 1 + (currentWheelSpeed - 100) / 100 * -5;
                        
                        if (boostStrength <= 0)
                        {
                            boostStrength = 0;
                        }
                        
                        var boost = currentWheelSpeed + boostStrength;
                        WheelSpeed = new Vector4 { X = boost, Y = boost, Z = boost, W = boost };
                        break;
                    }
                }
                Task.Delay(interval).Wait();
            }
        });
    }

    public void PullButton_Click(object sender, RoutedEventArgs e)
    {
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
        if (!TurnAssistSwitch.IsOn)
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
                    if (Math.Abs(frontRight - frontLeft) < frontRight / ratio && Math.Abs(backRight - frontLeft) < backRight / ratio)
                    {
                        frontLeft -= strength;
                        backLeft -= strength;
                        frontRight += strength;
                        backRight += strength;
                    }
                }
                else if (GetAsyncKeyState(Keys.D) is 1 or Int16.MinValue)
                {
                    if (Math.Abs(frontLeft - frontRight) < frontLeft / ratio && Math.Abs(backLeft - frontRight) < backLeft / ratio)
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
        Mw.M.WriteMemory(WaterAddr, WaterDragSwitch.IsOn ? new Vector3(0f, 0f, 0f) : new Vector3(0f, 3700f, 13500f));
    }

    private void SuperBrakeSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (!SuperBrakeSwitch.IsOn)
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
                    
                if (GetAsyncKeyState(KbBrakeHack) is 1 or short.MinValue)
                {
                    LinearVelocity = LinearVelocity with
                    {
                        X = LinearVelocity.X * 0.95f,
                        Z = LinearVelocity.Z * 0.95f
                    };
                }
                Task.Delay(10).Wait();
            }
        });
    }

    private void StopAllWheelsSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!StopAllWheelsSwitch.IsOn)
        {
            return;
        }

        Task.Run(() =>
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-GB");
                
            while (true)
            {
                bool toggled = true;
                Dispatcher.Invoke(delegate { toggled = StopAllWheelsSwitch.IsOn; });
                if (!toggled)
                {
                    break;
                }

                if (Mw.Gvp.Process!.MainWindowHandle != GetForegroundWindow())
                {
                    Task.Delay(25).Wait();
                    continue;
                }

                if (GetAsyncKeyState(KbBrakeHack) is 1 or short.MinValue)
                {
                    WheelSpeed = new Vector4(0f, 0f, 0f, 0f);
                }
                Task.Delay(10).Wait();
            }
        });
    }
        
    private float _originalGrav;
        
    private void FlyHackSwitch_OnToggled(object? sender, RoutedEventArgs e)
    {
        GravitySetSwitch.IsEnabled = !GravitySetSwitch.IsEnabled;
        FlyHackDetour.Toggle();
        
        if (!FlyHackSwitch.IsOn)
        {
            Gravity = _originalGrav;
            return;
        }

        if (GravitySetSwitch.IsOn)
        {
            GravitySetSwitch.IsOn = false;
        }

        if (!FlyHackDetour.Setup(sender, RotationAddr, "48 39 0D 10 00 00 00 74 09 F3 44 0F 10 89 94 00 00 00", 9, true))
        {
            FlyHackSwitch.Toggled -= FlyHackSwitch_OnToggled;
            FlyHackSwitch.IsOn = false;
            FlyHackSwitch.Toggled += FlyHackSwitch_OnToggled;
            FlyHackDetour.Clear();
            return;
        }
        _originalGrav = Gravity;
        FlyHackDetour.UpdateVariable(BitConverter.GetBytes(PlayerCarEntity));
        Gravity = 0f;
            
        // Rotation
        Task.Run(() =>
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-GB");
                
            var aDown = false;
            var dDown = false;
                
            while (true)
            {
                var toggled = true;
                Dispatcher.Invoke(() => toggled = FlyHackSwitch.IsOn);
                    
                if (!toggled)
                {
                    break;
                }

                if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow())
                {
                    Task.Delay(25).Wait();
                    continue;
                }

                UpdateRotationKeyStates(ref aDown, ref dDown);

                if (!aDown && !dDown)
                {
                    Task.Delay(25).Wait();
                    continue;
                }

                var flyHackRotSpeed = 1f;
                Dispatcher.Invoke(() => flyHackRotSpeed = ToSingle(FlyHackRotSpeedNum.Value / 2));

                HandleRotation(flyHackRotSpeed, aDown, dDown);
                
                Task.Delay(10).Wait();
            }
        });
        
        // Movement
        Task.Run(() =>
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-GB");
                
            while (true)
            {
                var toggled = true;
                Dispatcher.Invoke(delegate { toggled = FlyHackSwitch.IsOn; });
                    
                if (!toggled)
                {
                    break;
                }

                Rotation = Rotation with { Z = 1f };
                LinearVelocity = new Vector3 { X = 0f, Z = 0f, Y = 0f };
                AngularVelocity = new Vector3 { X = 0f, Z = 0f, Y = 0f };

                if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow())
                {
                    Task.Delay(25).Wait();
                    continue;
                }
                
                bool wDown = false, sDown = false, shiftDown = false, controlDown = false;
                UpdateMovementKeyStates(ref wDown, ref sDown, ref shiftDown, ref controlDown);

                if (!wDown && !sDown && !shiftDown && !controlDown)
                {
                    Task.Delay(25).Wait();
                    continue;
                }

                var angle = (float)((float)Math.Atan2(Rotation.X, Rotation.Y) * (180 / Math.PI));
                if (angle < 0)
                {
                    angle += 360;
                }
                
                float flyHackMoveSpeed = 1;
                Dispatcher.Invoke(() => flyHackMoveSpeed = ToSingle(FlyHackMoveSpeedNum.Value / 2));
                
                HandleMovement(angle,flyHackMoveSpeed,wDown,sDown,shiftDown,controlDown);

                Task.Delay(10).Wait();
            }
        });
    }

    private void HandleRotation(float speed, bool aDown, bool dDown)
    {
        if (aDown)
        {
            Rotation = Rotation.X switch
            {
                // Top right
                >= 0 when Rotation.Y >= 0 => Rotation with
                {
                    X = Rotation.X + speed / 10,
                    Y = Rotation.Y - speed / 10
                },
                // Bottom right
                >= 0 when Rotation.Y <= 0 => Rotation with
                {
                    X = Rotation.X - speed / 10,
                    Y = Rotation.Y - speed / 10
                },
                // Bottom Left
                <= 0 when Rotation.Y <= 0 => Rotation with
                {
                    X = Rotation.X - speed / 10,
                    Y = Rotation.Y + speed / 10
                },
                // Top Left
                <= 0 when Rotation.Y >= 0 => Rotation with
                {
                    X = Rotation.X + speed / 10,
                    Y = Rotation.Y + speed / 10
                },
                _ => Rotation
            };
        }
                    
        else if (dDown)
        {
            Rotation = Rotation.X switch
            {
                // Top right
                >= 0 when Rotation.Y >= 0 => Rotation with
                {
                    X = Rotation.X - speed / 10,
                    Y = Rotation.Y + speed / 10
                },
                // Bottom right
                >= 0 when Rotation.Y <= 0 => Rotation with
                {
                    X = Rotation.X + speed / 10,
                    Y = Rotation.Y + speed / 10
                },
                // Bottom Left
                <= 0 when Rotation.Y <= 0 => Rotation with
                {
                    X = Rotation.X + speed / 10,
                    Y = Rotation.Y - speed / 10
                },
                // Top Left
                <= 0 when Rotation.Y >= 0 => Rotation with
                {
                    X = Rotation.X - speed / 10,
                    Y = Rotation.Y - speed / 10
                },
                _ => Rotation
            };
        }
    }

    private void HandleMovement(float angle, float speed, bool wDown, bool sDown, bool shiftDown, bool controlDown)
    {
        if (wDown)
        {
            float xComp = 0f, zComp = 0f;
                        
            switch (angle)
            {
                // Top Left
                case < 90:
                {
                    xComp = -(float)Math.Sin(Math.PI * angle / 180);
                    zComp = (float)Math.Cos(Math.PI * angle / 180);
                    break;
                }
                // Bottom Left
                case > 90 and < 180:
                {
                    xComp = -(float)Math.Sin(Math.PI * (180 - angle) / 180);
                    zComp = -(float)Math.Cos(Math.PI * (180 - angle) / 180);
                    break;
                }
                // Bottom Right
                case > 180 and < 270:
                {
                    xComp = (float)Math.Cos(Math.PI * (270 - angle) / 180);
                    zComp = -(float)Math.Sin(Math.PI * (270 - angle) / 180);
                    break;
                }
                // Top Right
                case > 270:
                {
                    xComp = (float)Math.Sin(Math.PI * (360 - angle) / 180);
                    zComp = (float)Math.Cos(Math.PI * (360 - angle) / 180);
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
                    xComp = (float)Math.Sin(Math.PI * angle / 180);
                    zComp = -(float)Math.Cos(Math.PI * angle / 180);
                    break;
                }
                // Bottom Left
                case > 90 and < 180:
                {
                    xComp = (float)Math.Sin(Math.PI * (180 - angle) / 180);
                    zComp = (float)Math.Cos(Math.PI * (180 - angle) / 180);
                    break;
                }
                // Bottom Right
                case > 180 and < 270:
                {
                    xComp = -(float)Math.Cos(Math.PI * (270 - angle) / 180);
                    zComp = (float)Math.Sin(Math.PI * (270 - angle) / 180);
                    break;
                }
                // Top Right
                case > 270:
                {
                    xComp = -(float)Math.Sin(Math.PI * (360 - angle) / 180);
                    zComp = -(float)Math.Cos(Math.PI * (360 - angle) / 180);
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

    private void UpdateMovementKeyStates(ref bool wDown, ref bool sDown, ref bool shiftDown, ref bool controlDown)
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
    
    private void UpdateRotationKeyStates(ref bool aDown, ref bool dDown)
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
        JumpHackVelocityNum.Value = Math.Round(e.NewValue, 4);
    }

    private void JumpHackVelocityNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (Shp == null)
        {
            return;
        }
        
        try
        {
            JumpHackSlider.Value = (double)JumpHackVelocityNum.Value;
        }
        catch
        {
            
        }
    }

    private void JumpHackSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!JumpHackSwitch.IsOn)
        {
            return;
        }

        Task.Run(() =>
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-GB");
                
            while (true)
            {
                var toggled = true;
                Dispatcher.Invoke(() => toggled = JumpHackSwitch.IsOn);
                    
                if (!toggled)
                {
                    break;
                }

                if (GetAsyncKeyState(KbJmpHack) is not (1 or short.MinValue))
                {
                    Task.Delay(25).Wait();
                    continue;
                }

                if (LinearVelocity.Y > 0.5)
                {
                    continue;
                }

                var jmpVal = 1f;
                Dispatcher.Invoke(() => jmpVal = ToSingle(JumpHackVelocityNum.Value));
                LinearVelocity = LinearVelocity with { Y = LinearVelocity.Y + jmpVal };
                Task.Delay(50).Wait();
            }
        });
    }
}