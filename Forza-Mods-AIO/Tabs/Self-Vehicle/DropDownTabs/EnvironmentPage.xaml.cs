using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Keys = System.Windows.Forms.Keys;
using Forza_Mods_AIO.Resources;

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
                    MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.WorldRGBAddr, (Convert.ToSingle(sender.GetType().GetProperty("Value")!.GetValue(sender)!) / 100000000000));
                    break;
                case "SunGreenSlider":
                    MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.WorldRGBAddrLong + 4).ToString("X"), (Convert.ToSingle(sender.GetType().GetProperty("Value")!.GetValue(sender)!) / 100000000000));
                    break;
                case "SunBlueSlider":
                    MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.WorldRGBAddrLong + 8).ToString("X"), (Convert.ToSingle(sender.GetType().GetProperty("Value")!.GetValue(sender)!) / 100000000000));
                    break;
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
                    SunRedSlider.Value = 3.921569E+09;
                    MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.WorldRGBAddr, ((float)3.921569E+09 / 1000000000000));
                    break;
                case "GreenResetButton":
                    SunGreenSlider.Value = 3.921569E+09;
                    MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.WorldRGBAddrLong + 4).ToString("X"), ((float)3.921569E+09 / 1000000000000));
                    break;
                case "BlueResetButton":
                    SunBlueSlider.Value = 3.921569E+09;
                    MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.WorldRGBAddrLong + 8).ToString("X"), ((float)3.921569E+09 / 1000000000000));
                    break;
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
                    if ((bool)sender.GetType().GetProperty("IsOn")!.GetValue(sender)!)
                        Task.Run(() => AidsMode());
                    break;
                case "TimeSwitch":
                    if (TimeSwitch.IsOn)
                        Task.Run((() => ManualTime()));
                    else
                        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.TimeNOPAddr, new byte[] { 0xF2, 0x0F, 0x11, 0x43, 0x08, 0x48, 0x83, 0xC4, 0x40 });
                    break;
                case "OOBSwitch":
                    if (!OOBSwitch.IsOn)
                        return;
                    
                    Task.Run(() => OOB_Bypass());
                    break;
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
            bool Toggled = true;
            Dispatcher.Invoke((() => Toggled = AidsSwitch.IsOn));
            
            if (!Toggled)
                break;
            
            float Last = 0;
            
            Color Rainbow = EnvironmentPage.Rainbow(I);
            Dispatcher.Invoke(() =>
            {
                SunRedSlider.Value = ((float)Rainbow.R / 255) * (float)1E+10;
                SunGreenSlider.Value = ((float)Rainbow.G / 255) * (float)1E+10;
                SunBlueSlider.Value = ((float)Rainbow.B / 255) * (float)1E+10;
                Last = (float)SunRedSlider.Value;
            });
            I += 25 / 10000;
            Thread.Sleep(25);

            Dispatcher.Invoke(() =>
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (SunRedSlider.Value != Last)
                    AidsSwitch.IsOn = false;
            });
        }
    }

    // Might have to fix it for release, Ill leave it like this for now.
    private static Color Rainbow(float Progress)
    {
        float div = (Math.Abs(Progress % 1) * 6);
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

    private double LastCustomTime;
    
    private void ManualTime()
    {
        if (Self_Vehicle_Addrs.TimeAddr == "0" || Self_Vehicle_Addrs.TimeAddr == null)
            ASM.GetTimeAddr();
        
        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.TimeNOPAddr, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });

        if (LastCustomTime != 0)
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.TimeAddr, LastCustomTime);
        
        while (true)
        {
            bool Toggled = true;
            Dispatcher.Invoke((() => Toggled = TimeSwitch.IsOn));
            
            if (!Toggled)
                break;

            if (DLLImports.GetAsyncKeyState(Keys.NumPad6) is 1 or Int16.MinValue || (DLLImports.GetAsyncKeyState(Keys.LShiftKey) is 1 or Int16.MinValue && (DLLImports.GetAsyncKeyState(Keys.OemPeriod) is 1 or Int16.MinValue)))
                TimeForward();
            
            else if (DLLImports.GetAsyncKeyState(Keys.NumPad4) is 1 or Int16.MinValue || (DLLImports.GetAsyncKeyState(Keys.LShiftKey) is 1 or Int16.MinValue && (DLLImports.GetAsyncKeyState(Keys.Oemcomma) is 1 or Int16.MinValue)))
                TimeBackwards();

            LastCustomTime = MainWindow.mw.m.ReadMemory<double>(Self_Vehicle_Addrs.TimeAddr);
            Thread.Sleep(15);
        }
    }
    
    private static void TimeForward()
    {
        if (DLLImports.GetAsyncKeyState(Keys.LControlKey) is 1 or Int16.MinValue)
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.TimeAddr, (MainWindow.mw.m.ReadMemory<double>(Self_Vehicle_Addrs.TimeAddr) + 100));
        else
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.TimeAddr, (MainWindow.mw.m.ReadMemory<double>(Self_Vehicle_Addrs.TimeAddr) + 25));
    }

    private static void TimeBackwards()
    {
        if (DLLImports.GetAsyncKeyState(Keys.LControlKey) is 1 or Int16.MinValue)
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.TimeAddr, (MainWindow.mw.m.ReadMemory<double>(Self_Vehicle_Addrs.TimeAddr) - 100));
        else
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.TimeAddr, (MainWindow.mw.m.ReadMemory<double>(Self_Vehicle_Addrs.TimeAddr) - 25));
    }

    #endregion

    #region OOB Bypass

    private void OOB_Bypass()
    {
        while (true)
        {
            bool Toggled = true;
            Dispatcher.Invoke((() => Toggled = OOBSwitch.IsOn));
            
            if (!Toggled)
                break;

            var Before = MainWindow.mw.gvp.Name == "Forza Horizon 4" ?  new byte[] { 0x0F, 0x11, 0x9B, 0xE0, 0xFA, 0xFF, 0xFF } : new byte[] { 0x0F, 0x11, 0x9B, 0x60, 0xFA, 0xFF, 0xFF };
            var NOP = new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };

            try
            {
                MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.OOBnopAddr, OOB_Check() ? Before : NOP);
            }
            catch
            {
                MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.OOBnopAddr, Before);
            }

            Thread.Sleep(25);
        }
    }

    private static bool OOB_Check()
    {
        return (MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.InPauseAddr) == 1 || (MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.OnGroundAddr) == 0 && (float)(Math.Sqrt(Math.Pow(MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.xVelocityAddr), 2) + Math.Pow(MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.yVelocityAddr), 2) + Math.Pow(MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.zVelocityAddr), 2)) * 2.23694) == 0));
    }
    
    #endregion
}