using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ControlzEx.Theming;
using Memory;
using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.AIO_Info;
using static System.Diagnostics.FileVersionInfo;
using static System.IO.Path;
using static System.Windows.Forms.Control;
using static System.Windows.Forms.DialogResult;
using static System.Windows.Forms.MessageBoxButtons;
using static System.Windows.Media.ColorConverter;
using static System.Windows.Media.VisualTreeHelper;
using static System.Xml.Linq.XElement;
using static ControlzEx.Theming.ThemeManager;
using static Forza_Mods_AIO.Overlay.Overlay;
using static Forza_Mods_AIO.Resources.Bypass;
using Monet = Forza_Mods_AIO.Resources.Theme.Monet;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;
using RadioButton = System.Windows.Controls.RadioButton;

namespace Forza_Mods_AIO;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    #region Variables

    public static MainWindow Mw { get; private set; } = null!; 

    public readonly Mem M = new() { SigScanTasks = Environment.ProcessorCount };
    //public LibraryMapper Mapper = null!;
    public readonly Gamepad Gamepad = new(); 
    public GameVerPlat Gvp { get; private set; } = new();
    public Keybinds Keybinds { get; } = new(); 
    public bool Attached { get; private set; }
    private IEnumerable<Visual>? _visuals;

    #endregion

    #region Starting

    public MainWindow()
    {
        InitializeComponent();
        UpdateAio();
        Mw = this;
        var converted = (Color)ConvertFromString("#FF2E3440");
        const string name = "AccentCol";
        Current.AddTheme(new Theme(name, name, "Dark", "Red", converted, new SolidColorBrush(converted), false, false));
        Current.ChangeTheme(Application.Current, name);
        AIO_Info.IsChecked = true;
        BackgroundBorder.Background = Monet.MainColour;
        FrameBorder.Background = Monet.MainColour;
        SideBar.Background = Monet.DarkishColour;
        TopBar1.Background = Monet.DarkColour;
        TopBar2.Background = Monet.DarkColour;
        CategoryButton_Click(AIO_Info, new RoutedEventArgs());
        Loaded += (_, _) =>
        {
            Task.Run(IsAttached);
            ToggleButtons(false);
        };
    }

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
        
        if (MessageBox.Show(@"New tool version found, would you like to update?", @"Updater", YesNo) != Yes)
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
    #endregion

    #region Dragging

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton != MouseButton.Left || !(MousePosition.Y < Window.Top + 50)) return;
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
        var rbName = string.Empty;

        foreach (var element in ButtonStack.Children)
        {
            if (element.GetType() != typeof(RadioButton))
            {
                continue;
            }

            var rb = (RadioButton)element;
            
            if (rb.IsChecked != true)
            {
                rb.Background = Monet.DarkishColour;
                continue;
            }

            rb.Background = Monet.DarkerColour;
            rbName = rb.Name;
        }

        _visuals ??= Window.GetChildren(this);

        foreach (var element in _visuals.Cast<FrameworkElement>())
        {
            if (element.Name == rbName + "Frame")
            {
                element.Visibility = Visibility.Visible;
            }

            else if (element is Frame frame && frame.Name.Contains("Frame"))
            {
                frame.Visibility = Visibility.Hidden;
            }
        }
    }

    #endregion

    #region Attaching/Behaviour

    private void IsAttached()
    {
        var firstTime = true;
        while (true)
        {
            if (!firstTime)
            {
                Task.Delay(Attached ? 1000 : 500).Wait();
            }

            firstTime = false;
            
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
                    Self_Vehicle.IsEnabled = true;
                    Self_Vehicle.Foreground = Brushes.White;
                    Speedtest.Fill = Brushes.White;
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

    private void GvpMaker(string name, GameVerPlat.GameType type)
    {
        var process = M.MProc.Process;
        if (process.MainModule == null)
        {
            return;
        }
        
        string platform;
        string update;
        var gamePath = process.MainModule.FileName;

        if (gamePath.Contains("Microsoft.624F8B84B80") || gamePath.Contains("Microsoft.SunriseBaseGame") ||
            gamePath.Contains("Microsoft.ForzaMotorsport"))
        {
            platform = "MS";
            var filePath = Combine(GetDirectoryName(gamePath) ?? throw new Exception(), "appxmanifest.xml");
            var xml = Load(filePath);
            var descendants = xml.Descendants().Where(e => e.Name.LocalName == "Identity");
            var version = descendants.Select(e => e.Attribute("Version")).FirstOrDefault();
            update = version == null ? "Unable to get update info" : version.Value;
        }
        else
        {
            var filePath = Combine(GetDirectoryName(gamePath) ?? throw new Exception(), "OnlineFix64.dll");
            platform = File.Exists(filePath) ? "OnlineFix - Steam" : "Steam";
            update = GetVersionInfo(process.MainModule.FileName).FileVersion ?? "Unable to get update info";
        }

        Gvp = new GameVerPlat(name, platform, update, process, type);

        Dispatcher.Invoke(delegate
        {
            AttachedLabel.Content = $"{Gvp.Name}, {Gvp.Plat}, {Gvp.Update}";
            AioInfo.Ai.OverlaySwitch.IsEnabled = true;
            AioInfo.Ai.CustomTitle.IsEnabled = true;
            AioInfo.Ai.CustomTitleText.IsEnabled = true;
        });
    }

    
    #endregion
    
    #region Exit Handling
    private void Window_Closing(object sender, CancelEventArgs e)
    {
        Window.Hide();
        try
        {
            O.OverlayToggle(false);
        }
        catch { /* ignored */ }

        if (!Attached || Gvp.Process.MainModule == null)
        {
            Environment.Exit(0);
        }

        Dispatcher.Invoke(() => AioInfo.Ai.CustomTitle.IsOn = false);

        ExitHandling.DestroyDetours();
        ExitHandling.RevertWrites();
        EnableAntiCheat();
        Environment.Exit(0);
    }
    
    #endregion

    #region Keybinds

    private void Window_OnKeyDown(object sender, KeyEventArgs e)
    {
        Keybinds.Grab();
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