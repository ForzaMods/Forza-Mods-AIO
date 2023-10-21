using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.Self_Vehicle_Addrs;
using static Forza_Mods_AIO.MainWindow;
using System.Threading;
using MahApps.Metro.Controls;
using System.Globalization;
using Forza_Mods_AIO.Resources;
using System.Windows.Forms;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs
{
    /// <summary>
    /// Interaction logic for SpeedHacksPage.xaml
    /// </summary>
    public partial class HandlingPage : Page
    {
        public static HandlingPage shp;

        private static readonly byte[] before1 = { 0x0F, 0x11, 0x41, 0x10 };
        private static readonly byte[] before2 = { 0x0F, 0x11, 0x49, 0x20 };
        private static readonly byte[] before3 = { 0x0F, 0x11, 0x41, 0x30 };
        private static readonly byte[] before4 = { 0x0F, 0x11, 0x49, 0x40 };
        private static readonly byte[] before5 = { 0x0F, 0x11, 0x41, 0x50 };
        private static readonly byte[] nop = { 0x90, 0x90, 0x90, 0x90 };

        
        
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
            if (VelocitySwitch.IsOn)
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        // TODO wrap for controlls / ishplement controls
                        bool Toggled = true;
                        shp.Dispatcher.Invoke(delegate () { Toggled = (bool)shp.VelocitySwitch.GetType().GetProperty("IsOn").GetValue(shp.VelocitySwitch); });
                        if (!Toggled)
                            break;

                        float xVelocityVal = mw.m.ReadMemory<float>(xVelocityAddr) * (float)VelocityValueNum.Value;
                        float zVelocityVal = mw.m.ReadMemory<float>(zVelocityAddr) * (float)VelocityValueNum.Value;
                        mw.m.WriteMemory(xVelocityAddr, (float)xVelocityVal);
                        mw.m.WriteMemory(zVelocityAddr, (float)zVelocityVal);
                        Thread.Sleep(50);
                    }
                });
            }
        }

        private void WheelSpeedSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (WheelSpeedSwitch.IsOn)
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        int Interval = 1;
                        bool Toggled = true;
                        shp.Dispatcher.Invoke(delegate () { Toggled = shp.WheelSpeedSwitch.IsOn; });
                        if (!Toggled)
                            break;

                        string Mode = "";
                        shp.Dispatcher.Invoke(delegate () { Mode = (string)WheelSpeedModeComboBox.SelectedItem.GetType().GetProperty("Content").GetValue(WheelSpeedModeComboBox.SelectedItem); });
                        switch (Mode)
                        {
                            case "Static":
                                {
                                    if (DLLImports.GetAsyncKeyState(Keys.W) is 1 or Int16.MinValue)
                                    {
                                        float CurrentWheelSpeed = mw.m.ReadMemory<float>(FrontLeftAddr);
                                        float BoostStrength = 0;
                                        shp.Dispatcher.Invoke(delegate () { BoostStrength = (int)shp.Var1NumBox.Value; });
                                        mw.m.WriteMemory(FrontLeftAddr, (CurrentWheelSpeed + BoostStrength / 10));
                                        mw.m.WriteMemory(FrontRightAddr, (CurrentWheelSpeed + BoostStrength / 10));
                                        mw.m.WriteMemory(BackLeftAddr, (CurrentWheelSpeed + BoostStrength / 10));
                                        mw.m.WriteMemory(BackRightAddr, (CurrentWheelSpeed + BoostStrength / 10));
                                    }

                                    shp.Dispatcher.Invoke(delegate () { Interval = (int)shp.Var2NumBox.Value; });
                                    break;
                                }

                            case "Linear":
                                {
                                    if (DLLImports.GetAsyncKeyState(Keys.W) is 1 or Int16.MinValue)
                                    {
                                        float CurrentWheelSpeed = mw.m.ReadMemory<float>(FrontLeftAddr);
                                        float BoostFactor = 0;
                                        shp.Dispatcher.Invoke(delegate () { BoostFactor = (float)shp.Var1NumBox.Value; });
                                        float BoostStrength = (((BoostFactor / 10) - 1) + ((CurrentWheelSpeed - 100) / 100) * (-5));
                                        if (BoostStrength <= 0)
                                            BoostStrength = 0;
                                        mw.m.WriteMemory(FrontLeftAddr, (CurrentWheelSpeed + BoostStrength));
                                        mw.m.WriteMemory(FrontRightAddr, (CurrentWheelSpeed + BoostStrength));
                                        mw.m.WriteMemory(BackLeftAddr, (CurrentWheelSpeed + BoostStrength));
                                        mw.m.WriteMemory(BackRightAddr, (CurrentWheelSpeed + BoostStrength));
                                    }

                                    shp.Dispatcher.Invoke(delegate () { Interval = (int)shp.Var2NumBox.Value; });
                                    break;
                                }
                        }
                        Thread.Sleep(Interval);
                    }
                });
            }
        }

        public void PullButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType().GetProperty("Name").GetValue(sender).ToString().Contains("Gravity"))
                try { GravityValueNum.Value = MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.GravityAddr); } catch { }
            else
                try { AccelerationValueNum.Value = MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.WeirdAddr); } catch { }
        }

        private void SetSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            float original;
            string Addr;
            object SetSwitch;
            if (sender.GetType().GetProperty("Name").GetValue(sender).ToString().Contains("Gravity"))
            {
                SetSwitch = GravitySetSwitch;
                Addr = Self_Vehicle_Addrs.GravityAddr;
            }
            else
            {
                SetSwitch = AccelerationSetSwitch;
                Addr = Self_Vehicle_Addrs.WeirdAddr;
            }

            if ((bool)sender.GetType().GetProperty("IsOn").GetValue(sender))
            {
                Task.Run(() =>
                {
                    original = MainWindow.mw.m.ReadMemory<float>(Addr);
                    while (true)
                    {
                        bool Toggled = true;
                        Dispatcher.Invoke(delegate () { Toggled = (bool)SetSwitch.GetType().GetProperty("IsOn").GetValue(SetSwitch); });

                        if (!Toggled)
                        {
                            Dispatcher.Invoke(delegate () { SetSwitch.GetType().GetProperty("Value").SetValue(SetSwitch, Convert.ToDouble(original)); });
                            MainWindow.mw.m.WriteMemory(Addr, original);
                            break;
                        }

                        try
                        {
                            float SetValue = 0;
                            Dispatcher.Invoke(delegate () { SetValue = Convert.ToSingle(SetSwitch.GetType().GetProperty("Value").GetValue(SetSwitch)); });

                            if (MainWindow.mw.m.ReadMemory<float>(Addr) != SetValue)
                            {
                                MainWindow.mw.m.WriteMemory(Addr, SetValue);
                            }
                            Thread.Sleep(1);
                        }
                        catch
                        {
                            //Car Changed
                            while (true)
                            {
                                try
                                {
                                    original = MainWindow.mw.m.ReadMemory<float>(Addr);
                                    break;
                                }
                                catch { }
                            }
                        }
                    }
                });
            }
        }

        private void TurnAssistSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            float FrontLeft = MainWindow.mw.m.ReadMemory<float>(FrontLeftAddr);
            float FrontRight = MainWindow.mw.m.ReadMemory<float>(FrontRightAddr);
            float BackLeft = MainWindow.mw.m.ReadMemory<float>(BackLeftAddr);
            float BackRight = MainWindow.mw.m.ReadMemory<float>(BackRightAddr);

            if (DLLImports.GetAsyncKeyState(Keys.A) is 1 or Int16.MinValue)
            {
                if ((float)Math.Abs(FrontRight - FrontLeft) < (FrontRight / TurnAssistRatioBox.Value) && (float)Math.Abs(BackRight - FrontLeft) < (BackRight / TurnAssistRatioBox.Value))
                {
                    FrontLeft = FrontLeft - (float)TurnAssistStrengthBox.Value;
                    BackLeft = BackLeft - (float)TurnAssistStrengthBox.Value;
                    FrontRight = FrontRight + (float)TurnAssistStrengthBox.Value;
                    BackRight = BackRight + (float)TurnAssistStrengthBox.Value;
                    Thread.Sleep((int)TurnAssistIntervalBox.Value);
                }
            }
            else if (DLLImports.GetAsyncKeyState(Keys.S) is 1 or Int16.MinValue)
            {
                if ((float)Math.Abs(FrontLeft - FrontRight) < (FrontLeft / TurnAssistRatioBox.Value) && (float)Math.Abs(BackLeft - FrontRight) < (BackLeft / TurnAssistRatioBox.Value))
                {
                    FrontRight = FrontRight - (float)TurnAssistStrengthBox.Value;
                    BackRight = BackRight - (float)TurnAssistStrengthBox.Value;
                    FrontLeft = FrontLeft + (float)TurnAssistStrengthBox.Value;
                    BackLeft = BackLeft + (float)TurnAssistStrengthBox.Value;
                    Thread.Sleep((int)TurnAssistIntervalBox.Value);
                }
            }
            MainWindow.mw.m.WriteMemory(FrontLeftAddr, FrontLeft);
            MainWindow.mw.m.WriteMemory(FrontRightAddr, FrontRight);
            MainWindow.mw.m.WriteMemory(BackLeftAddr, BackLeft);
            MainWindow.mw.m.WriteMemory(BackRightAddr, BackRight);
        }

        private void SuperCarSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (SuperCarSwitch.IsOn)
            {
                if (mw.gvp.Name == "Forza Horizon 5")
                {
                    mw.m.WriteArrayMemory((SuperCarAddrLong - 4).ToString("X"), nop);
                }

                mw.m.WriteArrayMemory((SuperCarAddrLong + 4).ToString("X"), nop);
                mw.m.WriteArrayMemory((SuperCarAddrLong + 12).ToString("X"), nop);
                mw.m.WriteArrayMemory((SuperCarAddrLong + 20).ToString("X"), nop);
                mw.m.WriteArrayMemory((SuperCarAddrLong + 32).ToString("X"), nop);
                
            }
            else
            {
                if (mw.gvp.Name == "Forza Horizon 5")
                {
                    mw.m.WriteArrayMemory((SuperCarAddrLong - 4).ToString("X"), before1);
                }
                
                mw.m.WriteArrayMemory((SuperCarAddrLong + 4).ToString("X"), mw.gvp.Name == "Forza Horizon 4" ? before1 : before2);
                mw.m.WriteArrayMemory((SuperCarAddrLong + 12).ToString("X"), mw.gvp.Name == "Forza Horizon 4" ? before2 : before3);
                mw.m.WriteArrayMemory((SuperCarAddrLong + 20).ToString("X"), mw.gvp.Name == "Forza Horizon 4" ? before3 : before4);
                mw.m.WriteArrayMemory((SuperCarAddrLong + 32).ToString("X"), mw.gvp.Name == "Forza Horizon 4" ? before4 : before5);
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
            if (SuperBrakeSwitch.IsOn)
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        bool Toggled = true;
                        shp.Dispatcher.Invoke(delegate () { Toggled = shp.SuperBrakeSwitch.IsOn; });
                        if (!Toggled)
                            break;

                        if (DLLImports.GetAsyncKeyState(Keys.S) is 1 or Int16.MinValue)
                        {
                            float xVelocityVal = mw.m.ReadMemory<float>(xVelocityAddr) * (float)0.95;
                            float zVelocityVal = mw.m.ReadMemory<float>(zVelocityAddr) * (float)0.95;
                            float Y = mw.m.ReadMemory<float>(yAddr);
                            mw.m.WriteMemory(xVelocityAddr, xVelocityVal);
                            //mw.m.WriteMemory(yVelocityAddr, "float", "0");
                            mw.m.WriteMemory(zVelocityAddr, zVelocityVal);
                            mw.m.WriteMemory(yAddr, (Y - 0.01));
                            //mw.m.WriteMemory(yAngVelAddr, "float", "0");
                        }
                    }
                });
            }
        }

        private void StopAllWheelsSwitch_OnToggled(object sender, RoutedEventArgs e)
        {
            if (!StopAllWheelsSwitch.IsOn)
                return;
            
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(25);
                    bool Toggled = true;
                    shp.Dispatcher.Invoke(delegate () { Toggled = shp.StopAllWheelsSwitch.IsOn; });
                    if (!Toggled)
                        break;

                    if (!(DLLImports.GetAsyncKeyState(Keys.Space) is 1 or Int16.MinValue)) 
                        continue;

                    mw.m.WriteMemory(FrontLeftAddr, 0f);
                    mw.m.WriteMemory(FrontRightAddr, 0f);
                    mw.m.WriteMemory(BackLeftAddr, 0f);
                    mw.m.WriteMemory(BackRightAddr, 0f);
                }
            });
        }
    }
}
