using System;
using System.Drawing;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;
using static System.Convert;
using Keys = System.Windows.Forms.Keys;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Resources.DllImports;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

public partial class EnvironmentPage
{
    public static readonly Detour TimeDetour = new(), FreezeAiDetour = new();
    public static bool WasTimeDetoured { get; set; }
    private const string TimeDetourBytes = "51 48 83 C3 08 48 89 1D 0A 00 00 00 48 83 EB 08 59";
    private const string FreezeAiBytesFh5 = "50 48 8B 05 14 00 00 00 48 39 08 74 02 EB 07 0F 11 41 50 48 8B D9 58";
    private const string FreezeAiBytesFh4 = "50 48 8B 05 14 00 00 00 48 39 08 74 02 EB 07 0F 11 41 40 48 8B FA 58";
    
    public EnvironmentPage()
    {
        InitializeComponent();
    }

    #region Slider Event Handlers

    private void SunRGBSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (TimeSwitch == null || !Mw.Attached)
        {
            return;
        }
        
        switch (sender.GetType().GetProperty("Name")!.GetValue(sender))
        {
            case "SunRedSlider":
            {
                var value = ToSingle(sender.GetType().GetProperty("Value")!.GetValue(sender)!);
                Mw.M.WriteMemory(SunRedAddr, value / 1000000000000);
                break;
            }
            case "SunGreenSlider":
            {
                var value = ToSingle(sender.GetType().GetProperty("Value")!.GetValue(sender)!);
                Mw.M.WriteMemory(SunGreenAddr,  value / 1000000000000);
                break;
            }
            case "SunBlueSlider":
            {
                var value = ToSingle(sender.GetType().GetProperty("Value")!.GetValue(sender)!);
                Mw.M.WriteMemory(SunBlueAddr,  value / 1000000000000);
                break;
            }
        }
    }

    #endregion

    #region Reset Buttons Event Handlers

    private void ResetButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        switch (sender.GetType().GetProperty("Name")!.GetValue(sender))
        {
            case "RedResetButton":
            {
                SunRedSlider.Value = 3.921569E+09;
                if (!Mw.Attached) return;
                Mw.M.WriteMemory(SunRedAddr, 3.921569E+09f / 1000000000000);
                break;
            }
            case "GreenResetButton":
            {
                SunGreenSlider.Value = 3.921569E+09;
                if (!Mw.Attached) return;
                Mw.M.WriteMemory(SunGreenAddr, 3.921569E+09f / 1000000000000);
                break;
            }
            case "BlueResetButton":
            {
                SunBlueSlider.Value = 3.921569E+09;
                if (!Mw.Attached) return;
                Mw.M.WriteMemory(SunBlueAddr, 3.921569E+09f / 1000000000000);
                break;
            }
        }
    }

    #endregion

    #region Switch Toggles Event Handlers

    private void SwitchToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        switch (sender.GetType().GetProperty("Name")!.GetValue(sender))
        {
            case "AidsSwitch" when AidsSwitch.IsOn:
            {
                Task.Run(() => AidsMode());
                break;
            }
            case "TimeSwitch":
            {
                if (TimeSwitch.IsOn)
                {
                    Task.Run(() => ManualTime());
                }
                else
                {
                    Mw.M.WriteArrayMemory(TimeNopAddr, new byte[] { 0xF2, 0x0F, 0x11, 0x43, 0x08 });
                }

                break;
            }
        }
    }

    #endregion
    
    #region Aids Mode

    private Vector3 _last = new(0);
    
    private void AidsMode()
    {
        float I = 0;

        while (true)
        {
            var toggled = true;
            Dispatcher.Invoke(() => toggled = AidsSwitch.IsOn);
            
            if (!toggled)
                break;
            
            
            var rainbow = Rainbow(I);
            Dispatcher.Invoke(() =>
            {
                SunRedSlider.Value = rainbow.R / 255f * 1E+10f;
                SunGreenSlider.Value = rainbow.G / 255f * 1E+10f;
                SunBlueSlider.Value = rainbow.B / 255f * 1E+10f;
                _last = new Vector3(ToSingle(SunRedSlider.Value), ToSingle(SunGreenSlider.Value), ToSingle(SunBlueSlider.Value));
            });
            I += 0.0025f;
            Task.Delay(25).Wait();

            Dispatcher.Invoke(() =>
            {
                var currentValues = new Vector3(
                    ToSingle(SunRedSlider.Value), 
                    ToSingle(SunGreenSlider.Value),
                    ToSingle(SunBlueSlider.Value));
                
                if (currentValues != _last)
                {
                    AidsSwitch.IsOn = false;
                }
            });
        }
    }

    private static Color Rainbow(float progress)
    {
        var div = Math.Abs(progress % 1) * 6;
        var ascending = ToInt32(div % 1 * 255);
        var descending = 255 - ascending;

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
    
    private void ManualTime()
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        WasTimeDetoured = GetTimeAddr();
        
        if (!WasTimeDetoured)
        {
            MessageBox.Show("Failed");
            return;
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

            if (Mw.Gvp.Process!.MainWindowHandle != GetForegroundWindow())
            {
                Task.Delay(25).Wait();
                continue;
            }
            
            if (GetAsyncKeyState(Keys.NumPad6) is 1 or short.MinValue ||
                (GetAsyncKeyState(Keys.LShiftKey) is 1 or short.MinValue &&
                 GetAsyncKeyState(Keys.OemPeriod) is 1 or short.MinValue))
            {
                TimeForward();
            }

            else if (GetAsyncKeyState(Keys.NumPad4) is 1 or short.MinValue ||
                     (GetAsyncKeyState(Keys.LShiftKey) is 1 or short.MinValue &&
                      GetAsyncKeyState(Keys.Oemcomma) is 1 or short.MinValue))
            {
                TimeBackwards();
            }

            _lastCustomTime = Mw.M.ReadMemory<double>(TimeAddr);
            Task.Delay(15).Wait();
        }
    }
    
    private static void TimeForward()
    {
        if (GetAsyncKeyState(Keys.LControlKey) is 1 or short.MinValue)
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
        if (GetAsyncKeyState(Keys.LControlKey) is 1 or short.MinValue)
        {
            Mw.M.WriteMemory(TimeAddr, Mw.M.ReadMemory<double>(TimeAddr) - 100);
        }
        else
        {
            Mw.M.WriteMemory(TimeAddr, Mw.M.ReadMemory<double>(TimeAddr) - 25);
        }
    }

    #endregion

    private bool GetTimeAddr()
    {
        if (TimeDetour.IsSetup)
        {
            return true;
        }

        const string originalBytes = "F2 0F 11 43 08";
        if (!TimeDetour.Setup(TimeSwitch, TimeNopAddr, originalBytes, TimeDetourBytes, 5, true, 0, true))
        {
            TimeDetour.Clear();
            return false;
        }

        TimeAddr = 0;
        
        while ((TimeAddr = TimeDetour.ReadVariable<UIntPtr>()) == UIntPtr.Zero)
        {
            Task.Delay(1).Wait();
        }
        
        return true;
    }

    private void FreezeAi_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        CarEntity.Hook();

        const string origFh5 = "0F 11 41 50 48 8B D9";
        const string origFh4 = "0F 11 41 40 48 8B FA";

        var isFh4 = Mw.Gvp.Name.Contains('4');
        var orig = isFh4 ? origFh4 : origFh5;
        var bytes = isFh4 ? FreezeAiBytesFh4 : FreezeAiBytesFh5;
        
        if (!FreezeAiDetour.Setup(sender, AiXAddr, orig, bytes, 7, true))
        {
            FreezeAiDetour.Clear();
            return;
        }
        
        FreezeAiDetour.UpdateVariable(CarEntity.BaseDetour.VariableAddress);
        FreezeAiDetour.Toggle();
    }
}