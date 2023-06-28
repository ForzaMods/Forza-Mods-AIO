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
        static HandlingPage shp;

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
            try { shp.VelocitySlider.Value = (float)e.NewValue; } catch { }
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

                        float xVelocityVal = mw.m.ReadFloat(xVelocityAddr) * (float)VelocityValueNum.Value;
                        float zVelocityVal = mw.m.ReadFloat(zVelocityAddr) * (float)VelocityValueNum.Value;
                        mw.m.WriteMemory(xVelocityAddr, "float", xVelocityVal.ToString());
                        mw.m.WriteMemory(zVelocityAddr, "float", zVelocityVal.ToString());
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
                                        float CurrentWheelSpeed = mw.m.ReadFloat(FrontLeftAddr);
                                        float BoostStrength = 0;
                                        shp.Dispatcher.Invoke(delegate () { BoostStrength = (int)shp.Var1NumBox.Value; });
                                        mw.m.WriteMemory(FrontLeftAddr, "float", (CurrentWheelSpeed + BoostStrength / 10).ToString());
                                        mw.m.WriteMemory(FrontRightAddr, "float", (CurrentWheelSpeed + BoostStrength / 10).ToString());
                                        mw.m.WriteMemory(BackLeftAddr, "float", (CurrentWheelSpeed + BoostStrength / 10).ToString());
                                        mw.m.WriteMemory(BackRightAddr, "float", (CurrentWheelSpeed + BoostStrength / 10).ToString());
                                    }

                                    shp.Dispatcher.Invoke(delegate () { Interval = (int)shp.Var2NumBox.Value; });
                                    break;
                                }

                            case "Linear":
                                {
                                    if (DLLImports.GetAsyncKeyState(Keys.W) is 1 or Int16.MinValue)
                                    {
                                        float CurrentWheelSpeed = mw.m.ReadFloat(FrontLeftAddr);
                                        float BoostFactor = 0;
                                        shp.Dispatcher.Invoke(delegate () { BoostFactor = (float)shp.Var1NumBox.Value; });
                                        float BoostStrength = (((BoostFactor / 10) - 1) + ((CurrentWheelSpeed - 100) / 100) * (-5));
                                        if (BoostStrength <= 0)
                                            BoostStrength = 0;
                                        mw.m.WriteMemory(FrontLeftAddr, "float", (CurrentWheelSpeed + BoostStrength).ToString());
                                        mw.m.WriteMemory(FrontRightAddr, "float", (CurrentWheelSpeed + BoostStrength).ToString());
                                        mw.m.WriteMemory(BackLeftAddr, "float", (CurrentWheelSpeed + BoostStrength).ToString());
                                        mw.m.WriteMemory(BackRightAddr, "float", (CurrentWheelSpeed + BoostStrength).ToString());
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
                try { GravityValueNum.Value = MainWindow.mw.m.ReadFloat(Self_Vehicle_Addrs.GravityAddr, round: false); } catch { }
            else
                try { AccelerationValueNum.Value = MainWindow.mw.m.ReadFloat(Self_Vehicle_Addrs.WeirdAddr, round: false); } catch { }
        }

        private void SetSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            float original;
            string Addr;
            string Type;
            if (sender.GetType().GetProperty("Name").GetValue(sender).ToString().Contains("Gravity"))
            {
                Type = "Gravity";
                Addr = Self_Vehicle_Addrs.GravityAddr;
            }
            else
            {
                Type = "Acceleration";
                Addr = Self_Vehicle_Addrs.WeirdAddr;
            }

            if ((bool)sender.GetType().GetProperty("IsOn").GetValue(sender))
            {
                Task.Run(() =>
                {
                    original = MainWindow.mw.m.ReadFloat(Addr, round: false);
                    while (true)
                    {
                        bool Toggled = true;
                        if (Type == "Gravity")
                            Dispatcher.Invoke(delegate () { Toggled = (bool)GravitySetSwitch.GetType().GetProperty("IsOn").GetValue(shp.GravitySetSwitch); });
                        else
                            Dispatcher.Invoke(delegate () { Toggled = (bool)AccelerationSetSwitch.GetType().GetProperty("IsOn").GetValue(shp.AccelerationSetSwitch); });

                        if (!Toggled)
                        {
                            if (Type == "Gravity")
                                shp.Dispatcher.Invoke(delegate () { shp.GravityValueNum.GetType().GetProperty("Value").SetValue(shp.GravityValueNum, Convert.ToDouble(original)); });
                            else
                                shp.Dispatcher.Invoke(delegate () { shp.AccelerationValueNum.GetType().GetProperty("Value").SetValue(shp.AccelerationValueNum, Convert.ToDouble(original)); });
                            MainWindow.mw.m.WriteMemory(Addr, "float", original.ToString());
                            break;
                        }

                        try
                        {
                            float SetValue = 0;
                            if (Type == "Gravity")
                                shp.Dispatcher.Invoke(delegate () { SetValue = Convert.ToSingle(shp.GravityValueNum.GetType().GetProperty("Value").GetValue(shp.GravityValueNum)); });
                            else
                                shp.Dispatcher.Invoke(delegate () { SetValue = Convert.ToSingle(shp.AccelerationValueNum.GetType().GetProperty("Value").GetValue(shp.AccelerationValueNum)); });

                            if (MainWindow.mw.m.ReadFloat(Addr) != SetValue)
                            {
                                MainWindow.mw.m.WriteMemory(Addr, "float", SetValue.ToString());
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
                                    original = MainWindow.mw.m.ReadFloat(Addr);
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
            float FrontLeft = MainWindow.mw.m.ReadFloat(FrontLeftAddr);
            float FrontRight = MainWindow.mw.m.ReadFloat(FrontRightAddr);
            float BackLeft = MainWindow.mw.m.ReadFloat(BackLeftAddr);
            float BackRight = MainWindow.mw.m.ReadFloat(BackRightAddr);

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
            MainWindow.mw.m.WriteMemory(FrontLeftAddr, "float", FrontLeft.ToString());
            MainWindow.mw.m.WriteMemory(FrontRightAddr, "float", FrontRight.ToString());
            MainWindow.mw.m.WriteMemory(BackLeftAddr, "float", BackLeft.ToString());
            MainWindow.mw.m.WriteMemory(BackRightAddr, "float", BackRight.ToString());
        }

        private void SuperCarSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (SuperCarSwitch.IsOn)
            {
                var nop = new byte[] { 0x90, 0x90, 0x90, 0x90 };
                if (mw.gvp.Name == "Forza Horizon 5")
                {
                    mw.m.WriteBytes((SuperCarAddrLong - 4).ToString("X"), nop);
                    mw.m.WriteBytes((SuperCarAddrLong + 4).ToString("X"), nop);
                    mw.m.WriteBytes((SuperCarAddrLong + 12).ToString("X"), nop);
                    mw.m.WriteBytes((SuperCarAddrLong + 20).ToString("X"), nop);
                    mw.m.WriteBytes((SuperCarAddrLong + 32).ToString("X"), nop);
                }
                else
                {
                    mw.m.WriteBytes((SuperCarAddrLong + 4).ToString("X"), nop);
                    mw.m.WriteBytes((SuperCarAddrLong + 12).ToString("X"), nop);
                    mw.m.WriteBytes((SuperCarAddrLong + 20).ToString("X"), nop);
                    mw.m.WriteBytes((SuperCarAddrLong + 32).ToString("X"), nop);
                }
            }
            else
            {
                var before1 = new byte[] { 0x0F, 0x11, 0x41, 0x10 };
                var before2 = new byte[] { 0x0F, 0x11, 0x49, 0x20 };
                var before3 = new byte[] { 0x0F, 0x11, 0x41, 0x30 };
                var before4 = new byte[] { 0x0F, 0x11, 0x49, 0x40 };
                var before5 = new byte[] { 0x0F, 0x11, 0x41, 0x50 };
                if (mw.gvp.Name == "Forza Horizon 5")
                {
                    mw.m.WriteBytes((SuperCarAddrLong - 4).ToString("X"), before1);
                    mw.m.WriteBytes((SuperCarAddrLong + 4).ToString("X"), before2);
                    mw.m.WriteBytes((SuperCarAddrLong + 12).ToString("X"), before3);
                    mw.m.WriteBytes((SuperCarAddrLong + 20).ToString("X"), before4);
                    mw.m.WriteBytes((SuperCarAddrLong + 32).ToString("X"), before5);
                }
                else
                {
                    mw.m.WriteBytes((SuperCarAddrLong + 4).ToString("X"), before1);
                    mw.m.WriteBytes((SuperCarAddrLong + 12).ToString("X"), before2);
                    mw.m.WriteBytes((SuperCarAddrLong + 20).ToString("X"), before3);
                    mw.m.WriteBytes((SuperCarAddrLong + 32).ToString("X"), before4);
                }
            }
        }

        private void WaterDragSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (WaterDragSwitch.IsOn)
            {
                byte[] Enable = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                mw.m.WriteBytes(WaterAddr, Enable);
            }
            else
            {
                byte[] Disable = new byte[] { 0xCD, 0xCC, 0x4C, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x67, 0x45, 0x00, 0xF0, 0x52, 0x46, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x00, 0xCD, 0xCC, 0xCC, 0x3D, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0xC4, 0x44, 0x00, 0x00, 0xFF, 0x44, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x48, 0x42, 0x00, 0x00, 0xC8, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x40, 0x00, 0x00, 0x70, 0x41 };
                mw.m.WriteBytes(WaterAddr, Disable);
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
                            float xVelocityVal = mw.m.ReadFloat(xVelocityAddr) * (float)0.95;
                            float zVelocityVal = mw.m.ReadFloat(zVelocityAddr) * (float)0.95;
                            float Y = mw.m.ReadFloat(yAddr);
                            mw.m.WriteMemory(xVelocityAddr, "float", xVelocityVal.ToString());
                            //mw.m.WriteMemory(yVelocityAddr, "float", "0");
                            mw.m.WriteMemory(zVelocityAddr, "float", zVelocityVal.ToString());
                            mw.m.WriteMemory(yAddr, "float", (Y - 0.01).ToString());
                            //mw.m.WriteMemory(yAngVelAddr, "float", "0");
                        }
                    }
                });
            }
        }
    }
}
