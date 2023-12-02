using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ControlzEx.Theming;
using Memory;
using Forza_Mods_AIO.Overlay;
using Forza_Mods_AIO.Tabs.AutoShowTab;
using Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;
using Forza_Mods_AIO.Tabs.Tuning;
using Lunar;
using Keys = System.Windows.Forms.Keys;
using Monet = Forza_Mods_AIO.Resources.Theme.Monet;
using static System.Diagnostics.FileVersionInfo;
using static System.IO.Path;
using static System.Threading.Tasks.Task;
using static System.Windows.Media.ColorConverter;
using static System.Windows.Media.VisualTreeHelper;
using static System.Xml.Linq.XElement;
using static ControlzEx.Theming.ThemeManager;
using static Forza_Mods_AIO.Overlay.Overlay;
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
    public LibraryMapper Mapper = null!;
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
        Current.AddTheme(new Theme("AccentCol", "AccentCol", "Dark", "Red", (Color)ConvertFromString("#FF2E3440")!, new SolidColorBrush((Color)ConvertFromString("#FF2E3440")!), true, false));
        Current.ChangeTheme(Application.Current, "AccentCol");
        AIO_Info.IsChecked = true;
        Background.Background = Monet.MainColour;
        FrameBorder.Background = Monet.MainColour;
        SideBar.Background = Monet.DarkishColour;
        TopBar1.Background = Monet.DarkColour;
        TopBar2.Background = Monet.DarkColour;
        CategoryButton_Click(new Object(), new RoutedEventArgs());
        Loaded += (_, _) => ToggleButtons(false);
        Run(() => IsAttached());
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

        _visuals ??= Window.GetChildren(this);

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
                Run(AutoshowVars.Scan);
                break;
            }
            case "Self_Vehicle":
            {
                if (Gvp.Name == "Forza Horizon 5")
                    Run(FH5_Scan);
                else
                    Run(FH4_Scan);
                break;
            }
            case "Tuning":
            {
                Run(TuningAddresses.Scan);
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
            Delay(1000).Wait();
            if (M.OpenProcess("ForzaHorizon5"))
            {
                if (Attached)
                    continue;
                    
                GvpMaker(5);
                DisableAntiCheat(5);
                ToggleButtons(true);
                Attached = true;
            }
            else if (M.OpenProcess("ForzaHorizon4"))
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
        string? update;
        var name = $"Forza Horizon {ver}";
        var process = M.MProc.Process;
        var forzaPath = process.MainModule!.FileName;
            
        if (forzaPath.Contains("Microsoft.624F8B84B80") || forzaPath.Contains("Microsoft.SunriseBaseGame"))
        {
            platform = "MS";
            try
            {
                var filePath = Combine(GetDirectoryName(forzaPath), "appxmanifest.xml");
                var xml = Load(filePath);
                var descendants = xml.Descendants().Where(e => e.Name.LocalName == "Identity");
                var version = descendants.Select(e => e.Attribute("Version")).FirstOrDefault();
                update = version == null ? "Unable to get update info" : version.Value;
            }
            catch { update = "Unable to get update info"; }
        }
        else
        {
            try
            {
                var filePath = Combine(GetDirectoryName(forzaPath), "OnlineFix64.dll");
                platform = File.Exists(filePath) ? "OnlineFix - Steam" : "Steam";
            }
            catch
            {
                platform = "Unable to get platform info";
            }
            try
            {
                update = GetVersionInfo(process.MainModule!.FileName).FileVersion;
            }
            catch { update = "Unable to get update info"; }
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
            Tabs.Self_Vehicle.SelfVehicle.Sv.AobProgressBar.Value = 0;
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
        EnvironmentPage.TimeDetour.Clear();
        LocatorEntity.WaypointDetour.Clear();
        CarEntity.BaseDetour.Clear();
        MiscellaneousPage.ScaleDetour.Clear();
        MiscellaneousPage.SellDetour.Clear();
        MiscellaneousPage.Build1Detour.Clear();
        MiscellaneousPage.Build2Detour.Clear();
        MiscellaneousPage.SkillTreeDetour.Clear();
        MiscellaneousPage.CleanlinessDetour.Clear();
        MiscellaneousPage.ScoreDetour.Clear();
        HandlingPage.FlyHackDetour.Clear();
        MiscellaneousPage.SkillCostDetour.Clear();
        MiscellaneousPage.DriftDetour.Clear();
        MiscellaneousPage.MiscPage!.WasSkillDetoured = false;
        EnvironmentPage.WasTimeDetoured = false;
        TeleportsPage.WaypointDetoured = false;
        IsScanRunning = false;
    }
    #endregion
    #region Exit Handling
    private void Window_Closing(object sender, CancelEventArgs e)
    {
        Window.Hide();
        try
        {
            o?.OverlayToggle(false);
        }
        catch { /* ignored */ }

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
        CarEntity.BaseDetour.Destroy();
        EnvironmentPage.TimeDetour.Destroy();
        LocatorEntity.WaypointDetour.Destroy();
        HandlingPage.FlyHackDetour.Destroy();
        MiscellaneousPage.Build1Detour.Destroy();
        MiscellaneousPage.Build2Detour.Destroy();
        MiscellaneousPage.ScaleDetour.Destroy();
        MiscellaneousPage.SellDetour.Destroy();
        MiscellaneousPage.SkillTreeDetour.Destroy();
        MiscellaneousPage.CleanlinessDetour.Destroy();
        MiscellaneousPage.ScoreDetour.Destroy();
        MiscellaneousPage.SkillCostDetour.Destroy();
        MiscellaneousPage.DriftDetour.Destroy();

        try
        {
            if (Gvp.Name == "Forza Horizon 5")
            {
                M.WriteArrayMemory(SuperCarAddr - 4, new byte[] { 0x0F, 0x11, 0x41, 0x10 });
            }
                    
            M.WriteArrayMemory(SuperCarAddr + 4, Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x11, 0x41, 0x10 } : new byte[] { 0x0F, 0x11, 0x49, 0x20 });
            M.WriteArrayMemory(SuperCarAddr + 12, Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x11, 0x49, 0x20 } : new byte[] { 0x0F, 0x11, 0x41, 0x30 });
            M.WriteArrayMemory(SuperCarAddr + 20, Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x11, 0x41, 0x30 } : new byte[] { 0x0F, 0x11, 0x49, 0x40 });
            M.WriteArrayMemory(SuperCarAddr + 32, Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x11, 0x49, 0x40 } : new byte[] { 0x0F, 0x11, 0x41, 0x50 });
                
            M.WriteMemory(SunRedAddr,  0.003921568859f);
            M.WriteMemory(SunGreenAddr, 0.003921568859f);
            M.WriteMemory(SunBlueAddr, 0.003921568859f);

            M.WriteMemory(WaterAddr, new Vector3(0f, 3700f, 13500f));
            
            
            M.WriteArrayMemory(Wall1Addr, Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x84, 0x29, 0x02, 0x00, 0x00 } : new byte[] { 0x0F, 0x84, 0x60, 0x02, 0x00, 0x00 });
            M.WriteArrayMemory(Wall2Addr, Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x84, 0x2A, 0x02, 0x00, 0x00 } : new byte[] { 0x0F, 0x84, 0x7E, 0x02, 0x00, 0x00 });
            M.WriteArrayMemory(Car1Addr, Gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x84, 0xB5, 0x01, 0x00, 0x00 } : new byte[] { 0x0F, 0x84, 0x65, 0x03, 0x00, 0x00 }); 
            
            Mw.M.WriteMemory(WorldCollisionThreshold, 12f);
            Mw.M.WriteMemory(CarCollisionThreshold,12f);
            Mw.M.WriteMemory(SmashableCollisionTolerance,22f);

            M.WriteArrayMemory(AiXAddr, new byte[] { 0x0F, 0x11, 0x41, 0x50, 0x48, 0x8B, 0xFA });
            M.WriteArrayMemory(Car2Addr, new byte[] { 0x0F, 0x84, 0x3A, 0x03, 0x00, 0x00 });
        }
        catch { /* ignored */ }

        Environment.Exit(0);
    }
    #endregion

    private void Window_OnKeyDown(object sender, KeyEventArgs e)
    {   
        if (!Grabbing || !IsClicked) return;
        Grabbing = false;
        IsClicked = false;
        var oldKey = ClickedButton.Content;

        // proper conversion from wpf key to win-forms
        // https://stackoverflow.com/a/1153059
        
        foreach (var field in typeof(OverlayHandling).GetFields())
        {
            if (field.Name != ClickedButton?.Name.Replace("Button", string.Empty))
            {
                continue;
            }
            
            field.SetValue(new OverlayHandling(), (Keys)KeyInterop.VirtualKeyFromKey(e.Key));
            ClickedButton.Content = e.Key;
            return;
        }
        
        foreach (var field in typeof(HandlingKeybindings).GetFields())
        {
            if (field.Name.ToLower() != ClickedButton?.Name.ToLower().Replace("Button", string.Empty))
            {
                continue;
            }
            
            field.SetValue(new HandlingKeybindings(), (Keys)KeyInterop.VirtualKeyFromKey(e.Key));
            ClickedButton.Content = e.Key;
            return; 
        }

        ClickedButton!.Content = oldKey;
    }

    public bool Grabbing, IsClicked;
    public Button? ClickedButton;
}

public static class GetChildrenExtension
{
    // Credit to BrainSlugs83 for the GetChildren Method (https://stackoverflow.com/questions/874380/wpf-how-do-i-loop-through-the-all-controls-in-a-window) 
    // (Slightly modified)
    private static IEnumerable<Visual> GetChildrenPrivate(DependencyObject? parent, Visual? target, bool recurse)
    {
        if (parent == null) yield break;

        var count = GetChildrenCount(parent);

        for (var i = 0; i < count; i++)
        {
            var child = GetChild(parent, i) as Visual;

            if (child is not FrameworkElement frameworkElement)
            {
                continue;
            }

            if (target != null && !IsOnTarget(frameworkElement, target))
            {
                continue;
            }

            yield return child;

            if (!recurse)
            {
                continue;
            }
            
            foreach (var grandChild in child.GetChildren(target, recurse))
            {
                yield return grandChild;
            }
        }
    }
    
    public static IEnumerable<Visual> GetChildren(this Visual? parent, bool recurse = true)
    {
        return GetChildrenPrivate(parent, null, recurse);
    }

    public static IEnumerable<Visual> GetChildren(this Visual? parent, Visual? target, bool recurse = true)
    {
        return GetChildrenPrivate(parent, target, recurse);
    }

    private static bool IsOnTarget(Visual visual, Visual target)
    {
        var currentVisual = visual;
        while (currentVisual != null)
        {
            if (currentVisual == target)
            {
                return true;
            }

            currentVisual = GetParent(currentVisual) as FrameworkElement;
        }

        return false;
    }
}