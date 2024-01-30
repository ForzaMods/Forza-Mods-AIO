using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ControlzEx.Theming;
using Memory;
using Forza_Mods_AIO.Resources;
using SharpDX.XInput;
using static System.Diagnostics.FileVersionInfo;
using static System.IO.Path;
using static System.Windows.Forms.Control;
using static System.Windows.Media.ColorConverter;
using static System.Windows.Media.VisualTreeHelper;
using static System.Xml.Linq.XElement;
using static ControlzEx.Theming.ThemeManager;
using static Forza_Mods_AIO.Overlay.Overlay;
using static Forza_Mods_AIO.Resources.Bypass;

using Application = System.Windows.Application;
using Gamepad = Forza_Mods_AIO.Resources.Gamepad;
using Monet = Forza_Mods_AIO.Resources.Theme.Monet;

namespace Forza_Mods_AIO;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    #region Variables

    public static MainWindow Mw { get; private set; } = null!; 

    public readonly Mem M = new() { SigScanTasks = Environment.ProcessorCount };
    public readonly Gamepad Gamepad = new(); 
    public GameVerPlat Gvp { get; private set; } = new();
    public Keybindings Keybindings { get; } = new(); 
    public bool Attached { get; private set; }

    #endregion

    #region Starting

    public MainWindow()
    {
        InitializeComponent();
        Mw = this;
        LoadHotkeys();
#if RELEASE
        UpdateAio();
#endif
        InitializeTheme();
        
        CategoryButton_Click(AioInfo, new RoutedEventArgs());
        Loaded += (_, _) =>
        {
            Task.Run(IsAttached);
            ToggleButtons(false);
        };
    }

    private void LoadHotkeys()
    {
        var fields = typeof(Keybindings).GetFields(BindingFlags.Public | BindingFlags.Instance);
        if (!fields.Any()) return;
        
        Properties.Settings.Default.Upgrade();
        foreach (var field in fields)
        {
            var hotkey = Properties.Settings.Default[field.Name].ToString();
            if (hotkey == null) continue;
            if (field.FieldType == typeof(Key))
            {
                HandleKeyboardLoading(field, hotkey);
                continue;
            }

            if (!Enum.TryParse(typeof(GamepadButtonFlags), hotkey, out var gamepadResult))
            {
                continue;
            }

            field.SetValue(Keybindings, gamepadResult);
        }
    }

    private void SaveHotkeys()
    {
        var fields = typeof(Keybindings).GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (var field in fields)
        {   
            Properties.Settings.Default[field.Name] = field.GetValue(Keybindings)?.ToString();
        }
        
        Properties.Settings.Default.Save();
    }

    private void HandleKeyboardLoading(FieldInfo field, string? hotkeyName)
    {
        if (!Enum.TryParse(typeof(Key), hotkeyName, out var keyboardResult))
        {
            return;
        }
        
        field.SetValue(Keybindings, keyboardResult);
    }
    
    private void InitializeTheme()
    {
        var converted = (Color)ConvertFromString("#FF2E3440");
        const string name = "AccentCol";
        Current.AddTheme(new Theme(name, name, "Dark", "Red", converted, new SolidColorBrush(converted), false, false));
        Current.ChangeTheme(Application.Current, name);
        AioInfo.IsChecked = true;
        BackgroundBorder.Background = Monet.MainColour;
        SideBar.Background = Monet.DarkishColour;
        TopBar.Background = Monet.DarkColour;
#if RELEASE
        AttachedLabel.Content = "Launch FH4, FH5 or FM8";
#endif
    }

#if RELEASE
    
    private async void UpdateAio()
    {
        if (!await Updater.CheckInternetConnection())
        {
            return;
        }
        
        var updater = new Updater();

        if (!await updater.CheckForUpdates())
        {
            updater.Dispose();
            return;
        }

        Hide();
        
        if (MessageBox.Show(@"New tool version found, would you like to update?", @"Updater", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
        {
            Show();
            updater.Dispose();
            return;
        }

        if (await updater.DownloadAndApplyUpdate())
        {
            return;
        }
        
        Show();
        updater.Dispose();
    }
    
#endif

    #endregion

    #region Dragging

    private DateTime _lastMouseDownTime = DateTime.MinValue;
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        var isLeftButton = e.ChangedButton == MouseButton.Left;
        var isWithinTopArea = MousePosition.Y < Window.Top + 50;

        if (!(isLeftButton && isWithinTopArea))
        {
            return;
        }

        if ((DateTime.Now - _lastMouseDownTime).TotalMilliseconds < 200)
        {
            HandleTopBarDoubleClick();
        }

        _lastMouseDownTime = DateTime.Now;
        DragMove();
    }

    private void HandleTopBarDoubleClick()
    {
        switch (Window.WindowState)
        {
            case WindowState.Normal:
            {
                Window.WindowState = WindowState.Maximized;
                break;
            }
            case WindowState.Maximized:
            {
                Window.WindowState = WindowState.Normal;
                break;
            }
            case WindowState.Minimized:
            default:
            {
                return;
            }
        }
    }

    private void Minimize_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton != MouseButton.Left)
        {
            return;
        }
        
        WindowState = WindowState.Minimized;
    }
    
    #endregion

    #region Buttons

    private void ExitButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Close();
    }

    public void CategoryButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not RadioButton senderRb)
        {
            return;
        }
        
        var rbName = string.Empty;

        foreach (var element in ButtonStack.Children)
        {
            if (element.GetType() != typeof(Grid))
            {
                continue;
            }

            var grid = (Grid)element;
            var rb = GetRadioButtonFromGrid(grid);

            if (rb != senderRb)
            {
                rb.Background = Monet.DarkishColour;
                rb.IsChecked = false;
                continue;
            }

            rb.Background = Monet.DarkerColour;
            rbName = rb.Name.Replace("Button", string.Empty);
        }
        
        foreach (var element in FramesGrid.Children.Cast<FrameworkElement>())
        {
            if (element.Name == rbName + "Frame")
            {
                element.Visibility = Visibility.Visible;
                continue;
            }

            element.Visibility = Visibility.Hidden;
        }
    }

    private static RadioButton GetRadioButtonFromGrid(Panel grid)
    {
        foreach (var element in grid.Children)
        {
            if (element is not RadioButton radioButton)
            {
                continue;
            }

            return radioButton;
        }

        throw new ArgumentException(@"Grid doesnt contain any radiobutton");
    }
    
    #endregion

    #region Attaching/Behaviour

    private void IsAttached()
    {
        while (true)
        {
            Task.Delay(Attached ? 1000 : 500).Wait();
            
            if (M.OpenProcess("ForzaHorizon5"))
            {
                if (Attached)
                    continue;

                const string name = "Forza Horizon 5";
                const GameVerPlat.GameType type = GameVerPlat.GameType.Fh5;
                GvpMaker(name, type);
                ToggleButtons(true);
                Attached = true;
            }
            else if (M.OpenProcess("ForzaHorizon4"))
            {
                if (Attached)
                    continue;
                
                const string name = "Forza Horizon 4";
                const GameVerPlat.GameType type = GameVerPlat.GameType.Fh4;
                GvpMaker(name, type);
                ToggleButtons(true);
                Attached = true;
            }
            else if (M.OpenProcess("forza_gaming.desktop.x64_release_final"))
            {
                if (Attached)
                    continue;
                
                const string name = "Forza Motorsport 8";
                const GameVerPlat.GameType type = GameVerPlat.GameType.Fm8;
                GvpMaker(name, type);
                Dispatcher.Invoke(() =>
                {
                    SelfVehicle.IsEnabled = true;
                    SelfVehicle.Foreground = Brushes.White;
                    SpeedTest.Fill = Brushes.White;
                });
                Attached = true;
            }
            else
            {
                if (!Attached)
                    continue;

                ResetHandling.ResetAio();
                Gvp = new GameVerPlat();
                M.CloseProcess();
                Attached = false;
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }

    public void ToggleButtons(bool on)
    {
#if RELEASE
        Dispatcher.Invoke(() =>
        {
            SelfVehicle.IsEnabled = on;
            AutoShow.IsEnabled = on;
            Tuning.IsEnabled = on;

            SelfVehicle.Foreground = on ? Brushes.White : Brushes.DarkGray;
            AutoShow.Foreground = on ? Brushes.White : Brushes.DarkGray;
            Tuning.Foreground = on ? Brushes.White : Brushes.DarkGray;

            CarSports.Fill = on ? Brushes.White : Brushes.DarkGray;
            SpeedTest.Fill = on ? Brushes.White : Brushes.DarkGray;
            Tools.Fill = on ? Brushes.White : Brushes.DarkGray;
        });
#endif
    }

    private void GvpMaker(string name, GameVerPlat.GameType type)
    {
        var process = M.MProc.Process;
        if (process.MainModule == null)
        {
            return;
        }
        
        string platform;
#if RELEASE
        string update;
#endif        
        var gamePath = process.MainModule.FileName;

        if (gamePath.Contains("Microsoft.624F8B84B80") || gamePath.Contains("Microsoft.SunriseBaseGame") ||
            gamePath.Contains("Microsoft.ForzaMotorsport"))
        {
            platform = "MS";
#if RELEASE
            var filePath = Combine(GetDirectoryName(gamePath) ?? throw new Exception(), "appxmanifest.xml");
            var xml = Load(filePath);
            var descendants = xml.Descendants().Where(e => e.Name.LocalName == "Identity");
            var version = descendants.Select(e => e.Attribute("Version")).FirstOrDefault();
            update = version == null ? "Unable to get update info" : version.Value;
#endif
        }
        else
        {
            var filePath = Combine(GetDirectoryName(gamePath) ?? throw new Exception(), "OnlineFix64.dll");
            platform = File.Exists(filePath) ? "OnlineFix - Steam" : "Steam";
#if RELEASE
            update = GetVersionInfo(process.MainModule.FileName).FileVersion ?? "Unable to get update info";
#endif 
        }
#if RELEASE
        Gvp = new GameVerPlat(name, platform, update, process, type);
#else
        Gvp = new GameVerPlat(name, platform, process, type);
#endif
        Dispatcher.Invoke(delegate
        {
#if RELEASE
            AttachedLabel.Content = $"{Gvp.Name}, {Gvp.Plat}, {Gvp.Update}";
#endif
            Tabs.AIO_Info.AioInfo.Ai.OverlaySwitch.IsEnabled = true;
        });
    }

    
    #endregion
    
    #region Exit Handling
    private void Window_Closing(object sender, CancelEventArgs e)
    {
        Window.Hide();
        SaveHotkeys();
        try
        {
            O.OverlayToggle(false);
        }
        catch { /* ignored */ }

        if (!Attached || Gvp.Process.MainModule == null)
        {
            Environment.Exit(0);
        }

        ExitHandling.DestroyDetours();
        ExitHandling.RevertWrites();
        EnableAntiCheat();
        Environment.Exit(0);
    }
    
    #endregion
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

    private static bool IsOnTarget(DependencyObject? visual, Visual target)
    {
        while (visual != null)
        {
            if (visual == target)
            {
                return true;
            }
            
            visual = GetParent(visual);
        }
        return false;
    }
}