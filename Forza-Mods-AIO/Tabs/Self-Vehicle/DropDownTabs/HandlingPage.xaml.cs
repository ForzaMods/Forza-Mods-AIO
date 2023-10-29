using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Forza_Mods_AIO.Overlay;
using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.Self_Vehicle_Addrs;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs
{
    /// <summary>
    /// Interaction logic for SpeedHacksPage.xaml
    /// </summary>
    public partial class HandlingPage : Page
    {
        public static HandlingPage shp;

        private byte[] Before1 = { 0x0F, 0x11, 0x41, 0x10 }, Before2 = { 0x0F, 0x11, 0x49, 0x20 }, Before3 = { 0x0F, 0x11, 0x41, 0x30 }, Before4 = { 0x0F, 0x11, 0x49, 0x40 }, Before5 = { 0x0F, 0x11, 0x41, 0x50 };
        private byte[] Nop = { 0x90, 0x90, 0x90, 0x90 };
        
        public HandlingPage()
        {
            InitializeComponent();
            shp = this;
        }

        private void VelocitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VelocityValueNum.Value = Math.Round(e.NewValue, 5);
        }

        private void VelocityValueNum_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { if (shp != null) shp.VelocitySlider.Value = (float)e.NewValue; } catch { }
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
                    bool Toggled = true;
                    shp.Dispatcher.Invoke(delegate { Toggled = (bool)shp.VelocitySwitch.GetType().GetProperty("IsOn").GetValue(shp.VelocitySwitch); });
                    if (!Toggled)
                        break;
                    
                    float Multiply = 1;
                    shp.Dispatcher.Invoke(() => Multiply = (float)VelocityValueNum.Value);
                    
                    float xVelocityVal = mw.m.ReadMemory<float>(xVelocityAddr) * Multiply;
                    float zVelocityVal = mw.m.ReadMemory<float>(zVelocityAddr) * Multiply;
                    mw.m.WriteMemory(xVelocityAddr, xVelocityVal);
                    mw.m.WriteMemory(zVelocityAddr, zVelocityVal);
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
                    Thread.Sleep(25);
                    int Interval = 1;
                    bool Toggled = true;
                    shp.Dispatcher.Invoke(delegate { Toggled = shp.WheelSpeedSwitch.IsOn; });
                    if (!Toggled) 
                        break;
                    
                    if (mw.gvp.Process.MainWindowHandle != OverlayHandling.GetForegroundWindow())
                        continue;
                    
                    string Mode = "";
                    shp.Dispatcher.Invoke(delegate { Mode = (string)WheelSpeedModeComboBox.SelectedItem.GetType().GetProperty("Content").GetValue(WheelSpeedModeComboBox.SelectedItem); });
                    switch (Mode)
                    {
                        case "Static":
                        {
                            if (DLLImports.GetAsyncKeyState(Keys.W) is 1 or Int16.MinValue)
                            {
                                float CurrentWheelSpeed = mw.m.ReadMemory<float>(FrontLeftAddr);
                                float BoostStrength = 0;
                                shp.Dispatcher.Invoke(delegate { BoostStrength = (int)shp.Var1NumBox.Value; });
                                mw.m.WriteMemory(FrontLeftAddr, (CurrentWheelSpeed + BoostStrength / 10));
                                mw.m.WriteMemory(FrontRightAddr, (CurrentWheelSpeed + BoostStrength / 10));
                                mw.m.WriteMemory(BackLeftAddr, (CurrentWheelSpeed + BoostStrength / 10));
                                mw.m.WriteMemory(BackRightAddr, (CurrentWheelSpeed + BoostStrength / 10));
                            }

                            shp.Dispatcher.Invoke(delegate { Interval = (int)shp.Var2NumBox.Value; });
                            break;
                        }
                        
                        case "Linear":
                        {
                            if (DLLImports.GetAsyncKeyState(Keys.W) is 1 or Int16.MinValue)
                            {
                                float CurrentWheelSpeed = mw.m.ReadMemory<float>(FrontLeftAddr);
                                float BoostFactor = 0;
                                shp.Dispatcher.Invoke(delegate { BoostFactor = (float)shp.Var1NumBox.Value; });
                                float BoostStrength = (((BoostFactor / 10) - 1) + ((CurrentWheelSpeed - 100) / 100) * (-5));
                                if (BoostStrength <= 0)
                                    BoostStrength = 0;
                                
                                mw.m.WriteMemory(FrontLeftAddr, (CurrentWheelSpeed + BoostStrength));
                                mw.m.WriteMemory(FrontRightAddr, (CurrentWheelSpeed + BoostStrength));
                                mw.m.WriteMemory(BackLeftAddr, (CurrentWheelSpeed + BoostStrength));
                                mw.m.WriteMemory(BackRightAddr, (CurrentWheelSpeed + BoostStrength));
                            }

                            shp.Dispatcher.Invoke(delegate { Interval = (int)shp.Var2NumBox.Value; });
                            break;
                        }
                    }
                    Thread.Sleep(Interval);
                }
            });
        }

        public void PullButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType().GetProperty("Name").GetValue(sender).ToString().Contains("Gravity"))
                try { GravityValueNum.Value = mw.m.ReadMemory<float>(GravityAddr); } catch { }
            else
                try { AccelerationValueNum.Value = mw.m.ReadMemory<float>(WeirdAddr); } catch { }
        }

        private void SetSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (!(bool)sender.GetType().GetProperty("IsOn").GetValue(sender))
            {
                return;
            }

            float Original;
            string Addr = sender.GetType().GetProperty("Name").GetValue(sender).ToString().Contains("Gravity") ? GravityAddr : WeirdAddr;
            string Type = sender.GetType().GetProperty("Name").GetValue(sender).ToString().Contains("Gravity") ? "Gravity" : "Accel";
            
            Task.Run(() =>
            {
                Original = mw.m.ReadMemory<float>(Addr);
                while (true)
                {
                    Thread.Sleep(100);
                    bool Toggled = true;
                    if (Type == "Gravity")
                        Dispatcher.Invoke(delegate { Toggled = (bool)GravitySetSwitch.GetType().GetProperty("IsOn").GetValue(shp.GravitySetSwitch); });
                    else
                        Dispatcher.Invoke(delegate { Toggled = (bool)AccelerationSetSwitch.GetType().GetProperty("IsOn").GetValue(shp.AccelerationSetSwitch); });

                    if (!Toggled)
                    {
                        if (Type == "Gravity")
                            shp.Dispatcher.Invoke(delegate { shp.GravityValueNum.GetType().GetProperty("Value").SetValue(shp.GravityValueNum, Convert.ToDouble(Original)); });
                        else
                            shp.Dispatcher.Invoke(delegate { shp.AccelerationValueNum.GetType().GetProperty("Value").SetValue(shp.AccelerationValueNum, Convert.ToDouble(Original)); });
                        mw.m.WriteMemory(Addr, Original);
                        break;
                    }

                    float SetValue = 0;
                    if (Type == "Gravity") 
                        shp.Dispatcher.Invoke(delegate { SetValue = Convert.ToSingle(shp.GravityValueNum.GetType().GetProperty("Value").GetValue(shp.GravityValueNum)); });
                    else
                        shp.Dispatcher.Invoke(delegate { SetValue = Convert.ToSingle(shp.AccelerationValueNum.GetType().GetProperty("Value").GetValue(shp.AccelerationValueNum)); });

                    if (mw.m.ReadMemory<float>(Addr) != SetValue)
                    {
                        mw.m.WriteMemory(Addr, SetValue);
                    }
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
                    Thread.Sleep(25);
                    var Toggled = false;
                    shp.Dispatcher.Invoke(() => Toggled = TurnAssistSwitch.IsOn);

                    if (!Toggled)
                        break;

                    if (mw.gvp.Process.MainWindowHandle != OverlayHandling.GetForegroundWindow())
                        continue;
                    
                    float FrontLeft = mw.m.ReadMemory<float>(FrontLeftAddr);
                    float FrontRight = mw.m.ReadMemory<float>(FrontRightAddr);
                    float BackLeft = mw.m.ReadMemory<float>(BackLeftAddr);
                    float BackRight = mw.m.ReadMemory<float>(BackRightAddr);

                    int Interval = 1;
                    float Ratio = 1;
                    float Strength = 1;
                    shp.Dispatcher.Invoke(() =>
                    {
                        Interval = (int)TurnAssistIntervalBox.Value;
                        Ratio = (float)TurnAssistRatioBox.Value;
                        Strength = (float)TurnAssistStrengthBox.Value;
                    });
                    
                    if (DLLImports.GetAsyncKeyState(Keys.A) is 1 or Int16.MinValue)
                    {
                        if (Math.Abs(FrontRight - FrontLeft) < (FrontRight / Ratio) && Math.Abs(BackRight - FrontLeft) < (BackRight / Ratio))
                        {
                            FrontLeft -= Strength;
                            BackLeft -= Strength;
                            FrontRight += Strength;
                            BackRight += Strength;
                        }
                    }
                    else if (DLLImports.GetAsyncKeyState(Keys.D) is 1 or Int16.MinValue)
                    {
                        if (Math.Abs(FrontLeft - FrontRight) < (FrontLeft / Ratio) && Math.Abs(BackLeft - FrontRight) < (BackLeft / Ratio))
                        {
                            FrontRight -= Strength;
                            BackRight -= Strength;
                            FrontLeft += Strength;
                            BackLeft += Strength;
                        }
                    }

                    mw.m.WriteMemory(FrontLeftAddr, FrontLeft);
                    mw.m.WriteMemory(FrontRightAddr, FrontRight);
                    mw.m.WriteMemory(BackLeftAddr, BackLeft);
                    mw.m.WriteMemory(BackRightAddr, BackRight);
                    Thread.Sleep(Interval);
                }
            });
        }

        private void SuperCarSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (SuperCarSwitch.IsOn)
            {
                if (mw.gvp.Name == "Forza Horizon 5")
                {
                    mw.m.WriteArrayMemory((SuperCarAddrLong - 4).ToString("X"), Nop);
                }

                mw.m.WriteArrayMemory((SuperCarAddrLong + 4).ToString("X"), Nop);
                mw.m.WriteArrayMemory((SuperCarAddrLong + 12).ToString("X"), Nop);
                mw.m.WriteArrayMemory((SuperCarAddrLong + 20).ToString("X"), Nop);
                mw.m.WriteArrayMemory((SuperCarAddrLong + 32).ToString("X"), Nop);
                
            }
            else
            {
                if (mw.gvp.Name == "Forza Horizon 5")
                {
                    mw.m.WriteArrayMemory((SuperCarAddrLong - 4).ToString("X"), Before1);
                }
                
                mw.m.WriteArrayMemory((SuperCarAddrLong + 4).ToString("X"), mw.gvp.Name == "Forza Horizon 4" ? Before1 : Before2);
                mw.m.WriteArrayMemory((SuperCarAddrLong + 12).ToString("X"), mw.gvp.Name == "Forza Horizon 4" ? Before2 : Before3);
                mw.m.WriteArrayMemory((SuperCarAddrLong + 20).ToString("X"), mw.gvp.Name == "Forza Horizon 4" ? Before3 : Before4);
                mw.m.WriteArrayMemory((SuperCarAddrLong + 32).ToString("X"), mw.gvp.Name == "Forza Horizon 4" ? Before4 : Before5);
            }
        }

        private void WaterDragSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (WaterDragSwitch.IsOn)
            {
                byte[] Enable = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                mw.m.WriteArrayMemory(WaterAddr, Enable);
            }
            else
            {
                byte[] Disable = { 0xCD, 0xCC, 0x4C, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x67, 0x45, 0x00, 0xF0, 0x52, 0x46, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x00, 0xCD, 0xCC, 0xCC, 0x3D, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0xC4, 0x44, 0x00, 0x00, 0xFF, 0x44, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x48, 0x42, 0x00, 0x00, 0xC8, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x40, 0x00, 0x00, 0x70, 0x41 };
                mw.m.WriteArrayMemory(WaterAddr, Disable);
            }
        }

        private void SuperBrakeSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            StopAllWheelsSwitch.IsEnabled = !StopAllWheelsSwitch.IsEnabled;
            
            if (!SuperBrakeSwitch.IsOn)
                return;
            
            Task.Run(() =>
            {
                CultureInfo.CurrentCulture = new CultureInfo("en-GB");

                while (true)
                {
                    Thread.Sleep(10);
                    var Toggled = true;
                    shp.Dispatcher.Invoke(delegate { Toggled = shp.SuperBrakeSwitch.IsOn; });
                    if (!Toggled)
                        break;

                    if (mw.gvp.Process.MainWindowHandle != OverlayHandling.GetForegroundWindow())
                        continue;
                    
                    if (DLLImports.GetAsyncKeyState(HandlingKeybindings.BrakeHackKey) is not (1 or Int16.MinValue)) 
                        continue;
                    
                    float xVelocityVal = mw.m.ReadMemory<float>(xVelocityAddr) * (float)0.95;
                    float zVelocityVal = mw.m.ReadMemory<float>(zVelocityAddr) * (float)0.95;
                    mw.m.WriteMemory(xVelocityAddr, xVelocityVal);
                    mw.m.WriteMemory(zVelocityAddr, zVelocityVal);
                }
            });
        }

        private void StopAllWheelsSwitch_OnToggled(object sender, RoutedEventArgs e)
        {
            SuperBrakeSwitch.IsEnabled = !SuperBrakeSwitch.IsEnabled;
            
            if (!StopAllWheelsSwitch.IsOn)
                return;
            
            Task.Run(() =>
            {
                CultureInfo.CurrentCulture = new CultureInfo("en-GB");
                
                while (true)
                {
                    Thread.Sleep(25);
                    bool Toggled = true;
                    shp.Dispatcher.Invoke(delegate { Toggled = shp.StopAllWheelsSwitch.IsOn; });
                    if (!Toggled)
                        break;

                    if (mw.gvp.Process.MainWindowHandle != OverlayHandling.GetForegroundWindow())
                        continue;
                    
                    if (!(DLLImports.GetAsyncKeyState(HandlingKeybindings.BrakeHackKey) is 1 or Int16.MinValue)) 
                        continue;

                    mw.m.WriteMemory(FrontLeftAddr, 0f);
                    mw.m.WriteMemory(FrontRightAddr, 0f);
                    mw.m.WriteMemory(BackLeftAddr, 0f);
                    mw.m.WriteMemory(BackRightAddr, 0f);
                }
            });
        }


        private float OriginalGrav;
        
        private void FlyHackSwitch_OnToggled(object sender, RoutedEventArgs e)
        {
            if (!FlyHackSwitch.IsOn)
            {
                Thread.Sleep(25);
                mw.m.WriteMemory(GravityAddr, OriginalGrav);
                mw.m.WriteArrayMemory(RotationAddr, mw.gvp.Name == "Forza Horizon 5" ?  new byte[] { 0xF3, 0x44, 0x0F, 0x10, 0x89, 0x94, 0x00, 0x00, 0x00 } : new byte[] { 0xF3, 0x44, 0x0F, 0x10, 0x89, 0xF4, 0x00, 0x00, 0x00});
                return;
            }

            Self_Vehicle_ASM.Flyhack();
            OriginalGrav = mw.m.ReadMemory<float>(GravityAddr);
            
            // Rotation
            Task.Run(() =>
            {
                CultureInfo.CurrentCulture = new CultureInfo("en-GB");
                
                var ADown = false;
                var DDown = false;
                
                while (true)
                {
                    Thread.Sleep(10);

                    bool Toggled = true;
                    shp.Dispatcher.Invoke(delegate { Toggled = FlyHackSwitch.IsOn; });
                    
                    if (!Toggled)
                        break;
                    
                    if (mw.gvp.Process.MainWindowHandle != OverlayHandling.GetForegroundWindow())
                        continue;
                    
                    if (DLLImports.GetAsyncKeyState(Keys.A) is 1 or Int16.MinValue && !ADown)
                        ADown = true;
                    if (DLLImports.GetAsyncKeyState(Keys.A) is not 1 and not Int16.MinValue && ADown)
                        ADown = false;
                    if (DLLImports.GetAsyncKeyState(Keys.D) is 1 or Int16.MinValue && !DDown)
                        DDown = true;
                    if (DLLImports.GetAsyncKeyState(Keys.D) is not 1 and not Int16.MinValue && DDown)
                        DDown = false;
                    
                    if (!ADown && !DDown)
                        continue;
                    
                    var CurrentXRot = mw.m.ReadMemory<float>(XRot);
                    var CurrentYRot = mw.m.ReadMemory<float>(YRot);
                    var FlyhackRotSpeed = 1f;
                    shp.Dispatcher.Invoke(() => { FlyhackRotSpeed = (float)FlyHackRotSpeedNum.Value; });
                    
                    if (ADown)
                    {
                        switch (CurrentXRot)
                        {
                            // Top right
                            case >= 0 when CurrentYRot >= 0:
                                mw.m.WriteMemory(XRot, (CurrentXRot + (FlyhackRotSpeed / 10)));
                                mw.m.WriteMemory(YRot, (CurrentYRot - (FlyhackRotSpeed / 10)));
                                break;
                            // Bottom right
                            case >= 0 when CurrentYRot <= 0:
                                mw.m.WriteMemory(XRot, (CurrentXRot - (FlyhackRotSpeed / 10)));
                                mw.m.WriteMemory(YRot, (CurrentYRot - (FlyhackRotSpeed / 10)));
                                break;
                            // Bottom Left
                            case <= 0 when CurrentYRot <= 0:
                                mw.m.WriteMemory(XRot, (CurrentXRot - (FlyhackRotSpeed / 10)));
                                mw.m.WriteMemory(YRot, (CurrentYRot + (FlyhackRotSpeed / 10)));
                                break;
                            // Top Left
                            case <= 0 when CurrentYRot >= 0:
                                mw.m.WriteMemory(XRot, (CurrentXRot + (FlyhackRotSpeed / 10)));
                                mw.m.WriteMemory(YRot, (CurrentYRot + (FlyhackRotSpeed / 10)));
                                break;
                        }
                    }
                    
                    if (DDown)
                    {
                        switch (CurrentXRot)
                        {
                            case >= 0 when CurrentYRot >= 0:
                                mw.m.WriteMemory(XRot, (CurrentXRot - (FlyhackRotSpeed / 10)));
                                mw.m.WriteMemory(YRot, (CurrentYRot + (FlyhackRotSpeed / 10)));
                                break;
                            case >= 0 when CurrentYRot <= 0:
                                mw.m.WriteMemory(XRot, (CurrentXRot + (FlyhackRotSpeed / 10)));
                                mw.m.WriteMemory(YRot, (CurrentYRot + (FlyhackRotSpeed / 10)));
                                break;
                            case <= 0 when CurrentYRot <= 0:
                                mw.m.WriteMemory(XRot, (CurrentXRot + (FlyhackRotSpeed / 10)));
                                mw.m.WriteMemory(YRot, (CurrentYRot - (FlyhackRotSpeed / 10)));
                                break;
                            case <= 0 when CurrentYRot >= 0:
                                mw.m.WriteMemory(XRot, (CurrentXRot - (FlyhackRotSpeed / 10)));
                                mw.m.WriteMemory(YRot, (CurrentYRot - (FlyhackRotSpeed / 10)));
                                break;
                        }
                    }
                }
            });

            
            // Movement
            Task.Run(() =>
            {
                CultureInfo.CurrentCulture = new CultureInfo("en-GB");
                
                var WDown = false;
                var SDown = false;
                var ShiftDown = false;
                var ControlDown = false;
                
                while (true)
                {
                    Thread.Sleep(10);

                    var Toggled = true;
                    shp.Dispatcher.Invoke(delegate { Toggled = FlyHackSwitch.IsOn; });
                    
                    if (!Toggled)
                        break;
                    
                    mw.m.WriteMemory(Rotation, 1f);
                    mw.m.WriteMemory(xVelocityAddr, 0f);
                    mw.m.WriteMemory(yVelocityAddr, 0f);
                    mw.m.WriteMemory(zVelocityAddr,  0f);
                    
                    if (mw.gvp.Process.MainWindowHandle != OverlayHandling.GetForegroundWindow())
                        continue;
                    
                    if (DLLImports.GetAsyncKeyState(Keys.W) is 1 or Int16.MinValue && !WDown)
                        WDown = true;
                    if (DLLImports.GetAsyncKeyState(Keys.W) is not 1 and not Int16.MinValue && WDown)
                        WDown = false;
                    if (DLLImports.GetAsyncKeyState(Keys.S) is 1 or Int16.MinValue && !SDown)
                        SDown = true;
                    if (DLLImports.GetAsyncKeyState(Keys.S) is not 1 and not Int16.MinValue && SDown)
                        SDown = false;
                    if (DLLImports.GetAsyncKeyState(Keys.LShiftKey) is 1 or Int16.MinValue && !ShiftDown)
                        ShiftDown = true;
                    if (DLLImports.GetAsyncKeyState(Keys.LShiftKey) is not 1 and not Int16.MinValue && ShiftDown)
                        ShiftDown = false;
                    if (DLLImports.GetAsyncKeyState(Keys.LControlKey) is 1 or Int16.MinValue && !ControlDown)
                        ControlDown = true;
                    if (DLLImports.GetAsyncKeyState(Keys.LControlKey) is not 1 and not Int16.MinValue && ControlDown)
                        ControlDown = false;
                    
                    if (!WDown && !SDown && !ShiftDown && !ControlDown)
                        continue;
                    
                    var angle = (float)((float)Math.Atan2(mw.m.ReadMemory<float>(XRot), mw.m.ReadMemory<float>(YRot)) * (180 / Math.PI));
                    if (angle < 0)
                        angle += 360;
                    
                    float x = mw.m.ReadMemory<float>(xAddr);
                    float y = mw.m.ReadMemory<float>(yAddr);
                    float z = mw.m.ReadMemory<float>(zAddr);
                    float FlyhackMoveSpeed = 1;
                    shp.Dispatcher.Invoke(() => { FlyhackMoveSpeed = (float)FlyHackMoveSpeedNum.Value; });
                    
                    if (WDown)
                    {
                        switch (angle)
                        {
                            // Top Left
                            case < 90:
                            {
                                float XComp = -(float)(Math.Sin(Math.PI * angle / 180));
                                float YComp = (float)(Math.Cos(Math.PI * angle / 180));
                                mw.m.WriteMemory(xAddr, (x + (FlyhackMoveSpeed * 10 * XComp)));
                                mw.m.WriteMemory(zAddr, (z + (FlyhackMoveSpeed * 10 * YComp)));
                                break;
                            }
                            // Bottom Left
                            case > 90 and < 180:
                            {
                                float XComp = -(float)(Math.Sin(Math.PI * (180 - angle) / 180));
                                float YComp = -(float)(Math.Cos(Math.PI * (180 - angle) / 180));
                                mw.m.WriteMemory(xAddr, (x + (FlyhackMoveSpeed * 10 * XComp)));
                                mw.m.WriteMemory(zAddr, (z + (FlyhackMoveSpeed * 10 * YComp)));
                                break;
                            }
                            // Bottom Right
                            case > 180 and < 270:
                            {
                                float XComp = (float)(Math.Cos(Math.PI * (270 - angle) / 180));
                                float YComp = -(float)(Math.Sin(Math.PI * (270 - angle) / 180));
                                mw.m.WriteMemory(xAddr, (x + (FlyhackMoveSpeed * 10 * XComp))); 
                                mw.m.WriteMemory(zAddr, (z + (FlyhackMoveSpeed * 10 * YComp)));
                                break;
                            }
                            // Top Right
                            case > 270:
                            {
                                float XComp = (float)(Math.Sin(Math.PI * (360 - angle) / 180));
                                float YComp = (float)(Math.Cos(Math.PI * (360 - angle) / 180));
                                mw.m.WriteMemory(xAddr, (x + (FlyhackMoveSpeed * 10 * XComp)));
                                mw.m.WriteMemory(zAddr, (z + (FlyhackMoveSpeed * 10 * YComp)));
                                break;
                            }
                        }
                    }
                    if (SDown)
                    {
                        switch (angle)
                        {
                            // Top Left
                            case < 90:
                            {
                                float XComp = (float)(Math.Sin(Math.PI * angle / 180));
                                float YComp = -(float)(Math.Cos(Math.PI * angle / 180));
                                mw.m.WriteMemory(xAddr, (x + (FlyhackMoveSpeed * 10 * XComp)));
                                mw.m.WriteMemory(zAddr, (z + (FlyhackMoveSpeed * 10 * YComp)));
                                break;
                            }
                            // Bottom Left
                            case > 90 and < 180:
                            {
                                float XComp = (float)(Math.Sin(Math.PI * (180 - angle) / 180));
                                float YComp = (float)(Math.Cos(Math.PI * (180 - angle) / 180));
                                mw.m.WriteMemory(xAddr,(x + (FlyhackMoveSpeed * 10 * XComp)));
                                mw.m.WriteMemory(zAddr,(z + (FlyhackMoveSpeed * 10 * YComp)));
                                break;
                            }
                            // Bottom Right
                            case > 180 and < 270:
                            {
                                float XComp = -(float)(Math.Cos(Math.PI * (270 - angle) / 180));
                                float YComp = (float)(Math.Sin(Math.PI * (270 - angle) / 180));
                                mw.m.WriteMemory(xAddr, (x + (FlyhackMoveSpeed * 10 * XComp)));
                                mw.m.WriteMemory(zAddr, (z + (FlyhackMoveSpeed * 10 * YComp)));
                                break;
                            }
                            // Top Right
                            case > 270:
                            {
                                float XComp = -(float)(Math.Sin(Math.PI * (360 - angle) / 180));
                                float YComp = -(float)(Math.Cos(Math.PI * (360 - angle) / 180));
                                mw.m.WriteMemory(xAddr, (x + (FlyhackMoveSpeed * 10 * XComp)));
                                mw.m.WriteMemory(zAddr, (z + (FlyhackMoveSpeed * 10 * YComp)));
                                break;
                            }
                        }
                    }
                    
                    if (ShiftDown)
                        mw.m.WriteMemory(yAddr, (y + (FlyhackMoveSpeed * 5)));
                    else if (ControlDown)
                        mw.m.WriteMemory(yAddr, (y - (FlyhackMoveSpeed * 5)));
                }
            });
        }

        
        private void CarNoclipSwitch_OnToggled(object sender, RoutedEventArgs e)
        {            
            if (!CarNoclipSwitch.IsOn)
            {
                mw.m.WriteArrayMemory(Car1Addr, mw.gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x84, 0xB5, 0x01, 0x00, 0x00 } : new byte[] { 0x0F, 0x84, 0x65, 0x03, 0x00, 0x00 });
                if (mw.gvp.Name != "Forza Horizon 4") return;
                mw.m.WriteArrayMemory(Car2Addr, new byte[] { 0x0F, 0x84, 0x3A, 0x03, 0x00, 0x00});
                return;
            }

            mw.m.WriteArrayMemory(Car1Addr, mw.gvp.Name == "Forza Horizon 4" ? new byte[] { 0xE9, 0xB6, 0x01, 0x00, 0x00, 0x90 } : new byte[] { 0xE9, 0x66, 0x03, 0x00, 0x00, 0x90 });
            if (mw.gvp.Name != "Forza Horizon 4") return;
            mw.m.WriteArrayMemory(Car2Addr, new byte[] { 0xE9, 0x3B, 0x03, 0x00, 0x00, 0x90 });
        }

        private void WallNoclipSwitch_OnToggled(object sender, RoutedEventArgs e)
        {
            if (!WallNoclipSwitch.IsOn)
            {
                mw.m.WriteArrayMemory(Wall1Addr, mw.gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x84, 0x29, 0x02, 0x00, 0x00 } : new byte[] { 0x0F, 0x84, 0x60, 0x02, 0x00, 0x00 } );
                mw.m.WriteArrayMemory(Wall2Addr, mw.gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x84, 0x2A, 0x02, 0x00, 0x00 } : new byte[] { 0x0F, 0x84, 0x7E, 0x02, 0x00, 0x00 } );
                return;
            }
            mw.m.WriteArrayMemory(Wall1Addr, mw.gvp.Name == "Forza Horizon 4" ? new byte[] { 0xE9, 0x2A, 0x02, 0x00, 0x00, 0x90 } : new byte[] { 0xE9, 0x61, 0x02, 0x00, 0x00, 0x90 } );
            mw.m.WriteArrayMemory(Wall2Addr, mw.gvp.Name == "Forza Horizon 4" ? new byte[] { 0xE9, 0x2B, 0x02, 0x00, 0x00, 0x90 } : new byte[] { 0xE9, 0x7F, 0x02, 0x00, 0x00, 0x90 } );
        }

        private void JumpHackSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            JumpHackVelocityNum.Value = Math.Round(e.NewValue, 4);
        }

        private void JumpHackVelocityNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { if (shp != null) JumpHackSlider.Value = (double)JumpHackVelocityNum.Value;} catch {}
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
                    Thread.Sleep(50);
                    var Toggled = true;
                    Dispatcher.Invoke(() => Toggled = JumpHackSwitch.IsOn);
                    
                    if (!Toggled)
                        break;
                    
                    if (DLLImports.GetAsyncKeyState(HandlingKeybindings.JumpHackKey) is not (1 or Int16.MinValue))
                        continue;
                                            
                    mw.m.WriteMemory(yVelocityAddr, mw.m.ReadMemory<float>(yVelocityAddr) + (float)JumpHackVelocityNum.Value);
                }
            });
        }
    }
}