using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Resources.DllImports;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

public partial class EnvironmentPage
{
    public EnvironmentPage()
    {
        InitializeComponent();
    }

    #region Slider Event Handlers

    private void SunRGBSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        try
        {
            switch ((string)sender.GetType().GetProperty("Name")!.GetValue(sender))
            {
                case "SunRedSlider":
                {
                    Mw.M.WriteMemory(SunRedAddr, (Convert.ToSingle(sender.GetType().GetProperty("Value")!.GetValue(sender)!) / 1000000000000));
                    break;
                }
                case "SunGreenSlider":
                {
                    Mw.M.WriteMemory(SunGreenAddr, (Convert.ToSingle(sender.GetType().GetProperty("Value")!.GetValue(sender)!) / 1000000000000));
                    break;
                }
                case "SunBlueSlider":
                {
                    Mw.M.WriteMemory(SunBlueAddr, (Convert.ToSingle(sender.GetType().GetProperty("Value")!.GetValue(sender)!) / 1000000000000));
                    break;
                }
            }
        }
        catch
        {
            // ignored
        }
    }

    #endregion

    #region Reset Buttons Event Handlers

    private void ResetButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            switch ((string)sender.GetType().GetProperty("Name")!.GetValue(sender))
            {
                case "RedResetButton":
                {
                    SunRedSlider.Value = 3.921569E+09;
                    Mw.M.WriteMemory(SunRedAddr, ((float)3.921569E+09 / 1000000000000));
                    break;
                }
                case "GreenResetButton":
                {
                    SunGreenSlider.Value = 3.921569E+09;
                    Mw.M.WriteMemory(SunGreenAddr, ((float)3.921569E+09 / 1000000000000));
                    break;
                }
                case "BlueResetButton":
                {
                    SunBlueSlider.Value = 3.921569E+09;
                    Mw.M.WriteMemory(SunBlueAddr, ((float)3.921569E+09 / 1000000000000));
                    break;
                }
            }
        }
        catch
        {
            // ignoreed
        }
    }

    #endregion

    #region Switch Toggles Event Handlers

    private void SwitchToggled(object sender, RoutedEventArgs e)
    {
        try
        {
            switch ((string)sender.GetType().GetProperty("Name")!.GetValue(sender))
            {
                case "AidsSwitch":
                {
                    if (AidsSwitch.IsOn)
                    {
                        Task.Run(() => AidsMode());
                    }

                    break;
                }
                case "TimeSwitch":
                {
                    if (TimeSwitch.IsOn)
                    {
                        Task.Run(() => ManualTime(TimeSwitch));
                    }
                    else
                    {
                        Mw.M.WriteArrayMemory(TimeNopAddr, new byte[] { 0xF2, 0x0F, 0x11, 0x43, 0x08, 0x48, 0x83, 0xC4, 0x40 });
                    }

                    break;
                }
                case "OOBSwitch":
                {
                    if (!OOBSwitch.IsOn)
                    {
                        return;
                    }

                    Task.Run(() => OOB_Bypass());
                    break;
                }
            }
        }
        catch
        {
            // ignored
        }
    }

    #endregion
    
    #region Aids Mode

    private void AidsMode()
    {
        float I = 0;

        while (true)
        {
            bool toggled = true;
            Dispatcher.Invoke(() => toggled = AidsSwitch.IsOn);
            
            if (!toggled)
                break;
            
            float last = 0;
            
            var rainbow = EnvironmentPage.Rainbow(I);
            Dispatcher.Invoke(() =>
            {
                SunRedSlider.Value = (float)rainbow.R / 255 * (float)1E+10;
                SunGreenSlider.Value = (float)rainbow.G / 255 * (float)1E+10;
                SunBlueSlider.Value = (float)rainbow.B / 255 * (float)1E+10;
                last = (float)SunRedSlider.Value;
            });
            I += 25 / 10000;
            Task.Delay(25).Wait();

            Dispatcher.Invoke(() =>
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (SunRedSlider.Value != last)
                {
                    AidsSwitch.IsOn = false;
                }
            });
        }
    }

    // Might have to fix it for release, Ill leave it like this for now.
    private static Color Rainbow(float progress)
    {
        float div = (Math.Abs(progress % 1) * 6);
        int ascending = (int)((div % 1) * 255);
        int descending = 255 - ascending;

        return (int)div switch
        {
            0 => Color.FromArgb(255, 255, ascending, 0),
            1 => Color.FromArgb(255, descending, 255, 0),
            2 => Color.FromArgb(255, 0, 255, ascending),
            3 => Color.FromArgb(255, 0, descending, 255),
            4 => Color.FromArgb(255, ascending, 0, 255),
            _ => Color.FromArgb(255, 255, 0, descending)
        };
    }

    #endregion

    #region Manual Time

    private double _lastCustomTime;
    
    private void ManualTime(object sender)
    {
        if (TimeAddr is 0 or 8)
        {
            GetTimeAddr(sender);
        }

        Mw.M.WriteArrayMemory(TimeNopAddr, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });

        if (_lastCustomTime != 0)
        {
            Mw.M.WriteMemory(TimeAddr, _lastCustomTime);
        }

        while (true)
        {
            var toggled = true;
            Dispatcher.Invoke(() => toggled = TimeSwitch.IsOn);
            
            if (!toggled)
            {
                break;
            }

            if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow())
            {
                Task.Delay(25).Wait();
                continue;
            }
            
            if (GetAsyncKeyState(Keys.NumPad6) is 1 or short.MinValue || (GetAsyncKeyState(Keys.LShiftKey) is 1 or short.MinValue && GetAsyncKeyState(Keys.OemPeriod) is 1 or short.MinValue))
            {
                TimeForward();
            }

            else if (GetAsyncKeyState(Keys.NumPad4) is 1 or short.MinValue || (GetAsyncKeyState(Keys.LShiftKey) is 1 or short.MinValue && GetAsyncKeyState(Keys.Oemcomma) is 1 or short.MinValue))
            {
                TimeBackwards();
            }

            _lastCustomTime = Mw.M.ReadMemory<double>(TimeAddr);
            Task.Delay(15).Wait();
        }
    }
    
    private static void TimeForward()
    {
        if (GetAsyncKeyState(Keys.LControlKey) is 1 or Int16.MinValue)
        {
            Mw.M.WriteMemory(TimeAddr, Mw.M.ReadMemory<double>(TimeAddr) + 100);
        }
        else
        {
            Mw.M.WriteMemory(TimeAddr, Mw.M.ReadMemory<double>(TimeAddr) + 25);
        }
    }

    private static void TimeBackwards()
    {
        if (GetAsyncKeyState(Keys.LControlKey) is 1 or Int16.MinValue)
        {
            Mw.M.WriteMemory(TimeAddr, Mw.M.ReadMemory<double>(TimeAddr) - 100);
        }
        else
        {
            Mw.M.WriteMemory(TimeAddr, Mw.M.ReadMemory<double>(TimeAddr) - 25);
        }
    }

    #endregion

    #region OOB Bypass

    private void OOB_Bypass()
    {
        while (true)
        {
            Thread.Sleep(25);
            var toggled = true;
            Dispatcher.Invoke(() => toggled = OOBSwitch.IsOn);
            
            if (!toggled)
            {
                break;
            }

            var before = Mw.Gvp.Name == "Forza Horizon 4" ?  new byte[] { 0x0F, 0x11, 0x9B, 0xE0, 0xFA, 0xFF, 0xFF } : new byte[] { 0x0F, 0x11, 0x9B, 0x60, 0xFA, 0xFF, 0xFF };
            var nop = new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };

            try
            {
                Mw.M.WriteArrayMemory(OobNopAddr, OOB_Check() ? before : nop);
            }
            catch
            {
                Mw.M.WriteArrayMemory(OobNopAddr, before);
            }
        }
    }

    private static bool OOB_Check()
    {
        return true;
        //return (MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.InPauseAddr) == 1 || (MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.OnGroundAddr) == 0 && (float)(Math.Sqrt(Math.Pow(MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.xVelocityAddr), 2) + Math.Pow(MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.yVelocityAddr), 2) + Math.Pow(MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.zVelocityAddr), 2)) * 2.23694) == 0));
    }

    private void GetTimeAddr(object sender)
    {
        // 51 48 8B 4B 08 48 89 0D 06 00 00 00 59
    }
    
    #endregion
}