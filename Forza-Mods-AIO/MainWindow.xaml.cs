using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ControlzEx.Theming;
using Memory;
using System.Xml.Linq;
using Forza_Mods_AIO.Overlay;
using Forza_Mods_AIO.Tabs.Self_Vehicle;
using Forza_Mods_AIO.Tabs.AutoShowTab;
using Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using Forza_Mods_AIO.Tabs.Tuning;
using Lunar;
using static Forza_Mods_AIO.Overlay.Overlay;
using Keys = System.Windows.Forms.Keys;
using Monet = Forza_Mods_AIO.Resources.Theme.Monet;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;
using static Forza_Mods_AIO.Resources.Bypass;

namespace Forza_Mods_AIO;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public class GameVerPlat
    {
        public string? Name { get; }
        public string? Plat { get; }
        public Process? Process { get; }
        public string? Update { get; }

        public GameVerPlat(string? name, string? plat, Process? process, string? update)
        {
            Name = name; Plat = plat; Process = process; Update = update;
        }
    }

    #region Variables
    public static MainWindow Mw = new();
    public readonly Mem M = new()
    {
        SigScanTasks = Environment.ProcessorCount * 5
    };
    public LibraryMapper Mapper;
    public GameVerPlat Gvp = new(null, null, null, null);
    public bool Attached;
    private IEnumerable<Visual>? _visuals;
    private readonly Dictionary<string, bool> _isScanned = new()
    {
        { "AutoShow", false },
        { "Self_Vehicle", false },
        { "Tuning", false }
    };
    #endregion
    #region Starting
    public MainWindow()
    {
        InitializeComponent();
        Mw = this;
        ThemeManager.Current.AddTheme(new Theme("AccentCol", "AccentCol", "Dark", "Red", (Color)ColorConverter.ConvertFromString("#FF2E3440")!, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2E3440")!), true, false));
        ThemeManager.Current.ChangeTheme(Application.Current, "AccentCol");
        AIO_Info.IsChecked = true;
        Background.Background = Monet.MainColour;
        FrameBorder.Background = Monet.MainColour;
        SideBar.Background = Monet.DarkishColour;
        TopBar1.Background = Monet.DarkColour;
        TopBar2.Background = Monet.DarkColour;
        CategoryButton_Click(new Object(), new RoutedEventArgs());
        Loaded += (_, _) => ToggleButtons(false);
        Task.Run(() => IsAttached());
    }
    #endregion
    #region Dragging
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left && System.Windows.Forms.Control.MousePosition.Y < Window.Top + 50)
            DragMove();
    }
    #endregion
    #region Buttons
    private void ExitButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Close();
    }

    public void CategoryButton_Click(object sender, RoutedEventArgs e)
    {
        var rbName = "";

        foreach (RadioButton rb in ButtonStack.Children)
        {
            // RB Isnt the checked one
            if (rb.IsChecked != true)
            {
                rb.Background = Monet.DarkishColour;
                continue;
            }

            // RB Is the checked one
            rb.Background = Monet.DarkerColour;
            rbName = rb.Name;
        }

        _visuals ??= Window.GetChildren();

        foreach (var element in _visuals.Cast<FrameworkElement>())
        {
            // Page is RB.name + Frame
            if (element.Name == rbName + "Frame")
            {
                element.Visibility = Visibility.Visible;
            }

            // Page is not RB.name + Frame
            else if (element is Frame frame && frame.Name.Contains("Frame"))
            {
                element.Visibility = Visibility.Hidden;
            }
        }
            
        // Scanned is true
        if (!(_isScanned.TryGetValue(rbName, out var isScanned) && !isScanned && Attached))
        {
            return;
        }

        // Scanned is not true, scan
        _isScanned[rbName] = true;
        switch (rbName)
        {
            case "AutoShow":
            {
                Task.Run(() => AutoshowVars.Scan());
                break;
            }
            case "Self_Vehicle":
            {
                if (Gvp.Name == "Forza Horizon 5")
                    Task.Run(() => new SelfVehicleAddresses().FH5_Scan());
                else
                    Task.Run(() => new SelfVehicleAddresses().FH4_Scan());
                break;
            }
            case "Tuning":
            {
                Task.Run(() => TuningAddresses.Scan());
                break;
            }
        }
    }
    #endregion
    #region Attaching/Behaviour
    private void IsAttached()
    {
        while (true)
        {
            Task.Delay(1000).Wait();
            if (M != null && M.OpenProcess("ForzaHorizon5"))
            {
                if (Attached)
                    continue;
                    
                GvpMaker(5);
                DisableAntiCheat(5);
                ToggleButtons(true);
                Attached = true;
            }
            else if (M != null && M.OpenProcess("ForzaHorizon4"))
            {
                if (Attached)
                    continue;
                    
                GvpMaker(4);
                DisableAntiCheat(4);
                ToggleButtons(true);
                Attached = true;
            }
            else
            {
                if (!Attached)
                    continue;
                    
                ResetAio();
                M?.CloseProcess();
                Attached = false;
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private void ToggleButtons(bool on)
    {
        Dispatcher.Invoke(() =>
        {
            Self_Vehicle.IsEnabled = on;
            AutoShow.IsEnabled = on;
            Tuning.IsEnabled = on;

            Self_Vehicle.Foreground = on ? Brushes.White : Brushes.DarkGray;
            AutoShow.Foreground = on ? Brushes.White : Brushes.DarkGray;
            Tuning.Foreground = on ? Brushes.White : Brushes.DarkGray;

            CarSports.Fill = on ? Brushes.White : Brushes.DarkGray;
            Speedtest.Fill = on ? Brushes.White : Brushes.DarkGray;
            Tools.Fill = on ? Brushes.White : Brushes.DarkGray;
        });
    }
    private void GvpMaker(int ver)
    {
        string platform;
        var update = "Unknown";
        var name = $"Forza Horizon {ver}";
        var process = M.MProc.Process;
        var forzaPath = process.MainModule!.FileName;
            
        if (forzaPath.Contains("Microsoft.624F8B84B80") || forzaPath.Contains("Microsoft.SunriseBaseGame"))
        {
            platform = "MS";
            try
            {
                var filePath = Path.Combine(Path.GetDirectoryName(forzaPath), "appxmanifest.xml");
                var xml = XElement.Load(filePath);
                var descendants = xml.Descendants().Where(e => e.Name.LocalName == "Identity");
                var version = descendants.Select(e => e.Attribute("Version")).FirstOrDefault();
                    
                if (version != null)
                {
                    update = version.Value;
                }
            }
            catch { update = "Unknown"; }
        }
        else
        {
            var filePath = Path.Combine(Path.GetDirectoryName(forzaPath), "OnlineFix64.dll");
            platform = File.Exists(filePath) ? "OnlineFix - Steam" : "Steam";
            try
            {
                update = FileVersionInfo.GetVersionInfo(process.MainModule!.FileName).FileVersion;
            }
            catch { update = "Unknown"; }
        }
        Gvp = new GameVerPlat(name, platform, process, update);
            
        Dispatcher.Invoke(delegate 
        {
            AttachedLabel.Content = $"{Gvp.Name}, {Gvp.Plat}, {Gvp.Update}";
            Tabs.AIO_Info.AioInfo.Ai.OverlaySwitch.IsEnabled = true;
        });
    }

    private void ResetAio()
    {
        Dispatcher.Invoke(delegate 
        {
            AttachedLabel.Content = "Launch FH4/5";
            Tabs.Tuning.Tuning.T.AOBProgressBar.Value = 0;
            Tabs.Self_Vehicle.SelfVehicle.Sv.AOBProgressBar.Value = 0;
            Tabs.AutoShowTab.AutoShow.As.AobProgressBar.Value = 0;
            if (Tabs.AIO_Info.AioInfo.Ai.OverlaySwitch.IsOn)
            {
                Tabs.AIO_Info.AioInfo.Ai.OverlaySwitch.IsOn = false;
            }

            Tabs.AIO_Info.AioInfo.Ai.OverlaySwitch.IsEnabled = false;
            AIO_Info.IsChecked = true;
            CategoryButton_Click(new object(), new RoutedEventArgs());
        });
        _isScanned["Autoshow"] = false;
        _isScanned["Self_Vehicle"] = false;
        _isScanned["Tuning"] = false;

        ToggleButtons(false);
            
        AutoshowGarageOption.IsEnabled = false;
        SelfVehicleOption.IsEnabled = false;
        TuningOption.IsEnabled = false;
            
        UnlocksPage.XpDetour.Clear();
        UnlocksPage.CrDetour.Clear();
        CustomizationPage.GlowingPaintDetour.Clear();
        FovPage.FovLockDetour.Clear();
        Tabs.Self_Vehicle.Entities.CarEntity.BaseDetour.Clear();
        MiscellaneousPage.WasSkillDetoured = false;

        IsScanRunning = false;
    }
    #endregion
    #region Exit Handling
    private void Window_Closing(object sender, CancelEventArgs e)
    {
        Window.Hide();
        
        if (!Attached)
        {
            Environment.Exit(0);
        }
            

        try
        {
            Mapper.UnmapLibrary();
        }
        catch
        {
            // ignored
        }
        
        //TODO Cleanup here

        TuningAsm.Cleanup();
        AutoshowVars.ResetMem();
        UnlocksPage.XpDetour.Destroy();
        UnlocksPage.CrDetour.Destroy();
        UnlocksPage.SeasonalDetour.Destroy();   
        UnlocksPage.SeriesDetour.Destroy();   
        CustomizationPage.GlowingPaintDetour.Destroy();
        FovPage.FovLockDetour.Destroy();
        MiscellaneousPage.Build1Detour.Destroy();
        MiscellaneousPage.Build2Detour.Destroy();
        Tabs.Self_Vehicle.Entities.CarEntity.BaseDetour.Destroy();
            
        try
        {
            if (Gvp.Name == "Forza Horizon 5")
            {
                M.WriteArrayMemory((SuperCarAddr - 4).ToString("X"), new byte[] { 0x0F, 0x11, 0x41, 0x10 });
            }
                    
            M.WriteArrayMemory((SuperCarAddr + 4).ToString("X"), Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x11, 0x41, 0x10 } : new byte[] { 0x0F, 0x11, 0x49, 0x20 });
            M.WriteArrayMemory((SuperCarAddr + 12).ToString("X"), Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x11, 0x49, 0x20 } : new byte[] { 0x0F, 0x11, 0x41, 0x30 });
            M.WriteArrayMemory((SuperCarAddr + 20).ToString("X"), Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x11, 0x41, 0x30 } : new byte[] { 0x0F, 0x11, 0x49, 0x40 });
            M.WriteArrayMemory((SuperCarAddr + 32).ToString("X"), Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x11, 0x49, 0x40 } : new byte[] { 0x0F, 0x11, 0x41, 0x50 });
                
            M.WriteMemory(SunRedAddr,  0.003921568859f);
            M.WriteMemory(SunGreenAddr, 0.003921568859f);
            M.WriteMemory(SunBlueAddr, 0.003921568859f);

            M.WriteArrayMemory(Wall1Addr, Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x84, 0x29, 0x02, 0x00, 0x00 } : new byte[] { 0x0F, 0x84, 0x60, 0x02, 0x00, 0x00 });
            M.WriteArrayMemory(Wall2Addr, Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x84, 0x2A, 0x02, 0x00, 0x00 } : new byte[] { 0x0F, 0x84, 0x7E, 0x02, 0x00, 0x00 });
            M.WriteArrayMemory(Car1Addr, Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x84, 0xB5, 0x01, 0x00, 0x00 } : new byte[] { 0x0F, 0x84, 0x65, 0x03, 0x00, 0x00 }); 
            M.WriteArrayMemory(WaterAddr, new byte[] { 0xCD, 0xCC, 0x4C, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x67, 0x45, 0x00, 0xF0, 0x52, 0x46, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x00, 0xCD, 0xCC, 0xCC, 0x3D, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x00,0x00, 0x40, 0xC4, 0x44, 0x00, 0x00, 0xFF, 0x44, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x48, 0x42, 0x00, 0x00, 0xC8, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x40, 0x00, 0x00, 0x70, 0x41 });
            M.WriteArrayMemory(AiXAddr, new byte[] { 0x0F, 0x11, 0x41, 0x50, 0x48, 0x8B, 0xFA });
            M.WriteArrayMemory(Car2Addr, new byte[] { 0x0F, 0x84, 0x3A, 0x03, 0x00, 0x00 });
        }
        catch { /* ignored */ }

        Destroy();
        Environment.Exit(0);
    }
    #endregion

    private void Window_OnKeyDown(object sender, KeyEventArgs e)
    {   
        if (!Grabbing || !IsClicked) return;
        Grabbing = false;
        IsClicked = false;
        if (ClickedButton != null) ClickedButton.Content = e.Key;

        foreach (var field in typeof(OverlayHandling).GetFields())
        {
            if (field.Name != ClickedButton?.Name.Replace("Button", string.Empty))
            {
                continue;
            }
            
            field.SetValue(new OverlayHandling(), (Keys)Enum.Parse(typeof(Keys), e.Key.ToString()));
            return;
        }
        
        foreach (var field in typeof(HandlingKeybindings).GetFields())
        {
            if (field.Name.ToLower() != ClickedButton?.Name.ToLower().Replace("Button", string.Empty))
            {
                continue;
            }
            
            field.SetValue(new HandlingKeybindings(), (Keys)Enum.Parse(typeof(Keys), e.Key.ToString()));
            return; 
        }
    }

    public static bool Grabbing, IsClicked;
    public static Button? ClickedButton;
}

public static class GetChildrenExtension
{
    //Credit to BrainSlugs83 for the GetChildren Method (https://stackoverflow.com/questions/874380/wpf-how-do-i-loop-through-the-all-controls-in-a-window) 
    public static IEnumerable<Visual> GetChildren(this Visual? parent, bool recurse = true)
    {
        if (parent == null) yield break;
        int count = VisualTreeHelper.GetChildrenCount(parent);
        for (int i = 0; i < count; i++)
        {
            // Retrieve child visual at specified index value.
            var child = VisualTreeHelper.GetChild(parent, i) as Visual;

            if (child == null || child.GetType().ToString().Contains("MahApps.Metro.IconPacks")) continue;
            try { _ = (FrameworkElement)child; }
            catch { continue; }

            yield return child;

            if (!recurse) continue;
            foreach (var grandChild in child.GetChildren(true))
            {
                yield return grandChild;
            }
        }
    }
}