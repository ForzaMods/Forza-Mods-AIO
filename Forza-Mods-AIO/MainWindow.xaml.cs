using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using Memory;
using Forza_Mods_AIO.CustomTheming;
using System.Xml.Linq;
using Forza_Mods_AIO.Tabs.Self_Vehicle;
using Forza_Mods_AIO.Tabs.AutoShowTab;
using Lunar;

namespace Forza_Mods_AIO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    /*
     * Modify GUI from BG worker
     * 
        this.Dispatcher.BeginInvoke((Action)delegate () {
            Do Stuff to UI
        });
     * 
     * 
     */

    public partial class MainWindow : MetroWindow
    {
        public class GameVerPlat
        {
            public string Name { get; }
            public string Plat { get; }
            public Process Process { get; }
            public string Update { get; }

            public GameVerPlat(string name, string plat, Process process, string update)
            {
                Name = name; Plat = plat; Process = process; Update = update;
            }
        }

        #region Variables
        public static MainWindow mw = new MainWindow();
        public Mem m = new Mem();
        readonly List<Page> _tabs = new List<Page>() { new Tabs.AIO_Info.AIO_Info(), new Tabs.AutoShow(), new Tabs.Self_Vehicle.Self_Vehicle(), new Tabs.Tuning.Tuning(), new Tabs.Settings.Settings() };
        public GameVerPlat gvp = new GameVerPlat(null, null, null, null);
        public string PageFocused = "AIO_Info";
        public bool Attached = false;
        public LibraryMapper Mapper;
        public bool WasMapped = false;
        Dictionary<string, bool> Is_Scanned = new()
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
            mw = this;
            Task.Run(() => IsAttached());
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Forza Mods AIO" ;
            if (!Directory.Exists(documentsPath))
                Directory.CreateDirectory(documentsPath);
            ThemeManager.Current.AddTheme(new Theme("AccentCol", "AccentCol", "Dark", "Red", (Color)ColorConverter.ConvertFromString("#FF2E3440"), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2E3440")), true, false));
            ThemeManager.Current.ChangeTheme(Application.Current, "AccentCol");
            AIO_Info.IsChecked = true;
            Background.Background = Monet.MainColour;
            FrameBorder.Background = Monet.MainColour;
            SideBar.Background = Monet.DarkishColour;
            TopBar1.Background = Monet.DarkColour;
            TopBar2.Background = Monet.DarkColour;
            CategoryButton_Click(new Object(), new RoutedEventArgs());
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
            this.Close();
        }
        public void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (RadioButton rb in ButtonStack.Children)
            {
                if (rb.IsChecked != null && (bool)rb.IsChecked)
                {
                    rb.Background = Monet.DarkerColour;
                    foreach (var t in _tabs.Where(t => t.Title == rb.Name))
                    {
                        try
                        {
                            foreach (FrameworkElement Element in Window.GetChildren(true))
                            {
                                if (Element.Name == rb.Name + "Frame")
                                {
                                    PageFocused = rb.Name;
                                    Element.Visibility = Visibility.Visible;

                                    if (Is_Scanned.TryGetValue(rb.Name, out var isScanned) && !isScanned && Attached)
                                    {
                                        Is_Scanned[rb.Name] = true;
                                        switch (rb.Name)
                                        {
                                            case "AutoShow":
                                                Task.Run(() => new AutoshowVars().Scan());
                                                break;
                                            case "Self_Vehicle":
                                                if (gvp.Name == "Forza Horizon 5")
                                                    Task.Run(() => new Self_Vehicle_Addrs().FH5_Scan());
                                                else
                                                    Task.Run(() => new Self_Vehicle_Addrs().Old_Scan());
                                                break;
                                            case "Tuning":
                                                Task.Run(() => Tabs.Tuning.Tuning_Addresses.Scan());
                                                break;
                                        }
                                    }
                                }
                                else if (Element.GetType() == typeof(Frame) && Element.GetType().GetProperty("Name").GetValue(Element).ToString().Contains("Frame"))
                                {
                                    Element.Visibility = Visibility.Hidden;
                                }
                            }
                        }
                        catch { }
                    }
                }
                else
                {
                    rb.Background = Monet.DarkishColour;
                }
            }
        }
        #endregion
        #region Attaching/Behaviour
        private void IsAttached()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (m.OpenProcess("ForzaHorizon5"))
                {
                    if (Attached)
                        continue;
                    GvpMaker(5);
                    Attached = true;
                }
                else if (m.OpenProcess("ForzaHorizon4"))
                {
                    if (Attached)
                        continue;
                    GvpMaker(4);
                    Attached = true;
                }
                else
                {
                    if (!Attached)
                        continue;
                    Dispatcher.Invoke((Action)delegate () {
                        AttachedLabel.Content = "Launch FH4/5";
                        Tabs.Tuning.Tuning.TBM.AOBProgressBar.Value = 0;
                        Tabs.Self_Vehicle.Self_Vehicle.sv.AOBProgressBar.Value = 0;
                        Tabs.AutoShow.AS.AOBProgressBar.Value = 0;
                        Tabs.AIO_Info.AIO_Info.ai.OverlaySwitch.IsEnabled = false;
                        AIO_Info.IsChecked = true;
                        CategoryButton_Click(new Object(), new RoutedEventArgs());
                    });
                    Is_Scanned["Autoshow"] = false;
                    Is_Scanned["Self_Vehicle"] = false;
                    Is_Scanned["TuningTableMain"] = false;
                    Attached = false;
                }
            }
        }

        private void GvpMaker(int ver)
        {
            string platform, update = "Unknown";
            var process = m.mProc.Process;
            var name = "Forza Horizon " + ver;
            
            if (process.MainModule!.FileName.Contains("Microsoft.624F8B84B80") || process.MainModule!.FileName.Contains("Microsoft.SunriseBaseGame"))
            {
                platform = "MS";
                try
                {
                    var xml = XElement.Load(process.MainModule!.FileName[..process.MainModule!.FileName.LastIndexOf(@"\", StringComparison.Ordinal)] + @"\appxmanifest.xml");
                    var versionAttribute = xml.Descendants().Where(e => e.Name.LocalName == "Identity").Select(e => e.Attribute("Version")).FirstOrDefault();

                    if (versionAttribute != null)
                        update = versionAttribute.Value;
                    
                }
                catch { update = "Unknown"; }
            }
            else
            {
                platform = File.Exists(Path.Combine(Path.GetDirectoryName(process.MainModule!.FileName) ?? string.Empty, "OnlineFix64.dll")) ? "OnlineFix - Steam" : "Steam";
                update = FileVersionInfo.GetVersionInfo(process.MainModule!.FileName).FileVersion;
            }
            gvp = new GameVerPlat(name, platform, process, update);
            
            Dispatcher.Invoke(delegate {
                AttachedLabel.Content = $"{gvp.Name}, {gvp.Plat}, {gvp.Update}";
                Tabs.AIO_Info.AIO_Info.ai.OverlaySwitch.IsEnabled = true;
            });
        }
        #endregion
        #region Exit Handling
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!Attached)
            {
                Environment.Exit(0);
            }

            //TODO Cleanup here
            if (WasMapped)
                Mapper.UnmapLibrary();

            if (Self_Vehicle_Addrs.BaseAddrHook != null && Self_Vehicle_Addrs.BaseAddrHook != "0" && Self_Vehicle_Addrs.BaseAddrHookLong != -279 && assembly.OriginalBaseAddressHookBytes != null)
                m.WriteBytes(Self_Vehicle_Addrs.BaseAddrHook, assembly.OriginalBaseAddressHookBytes);
            AutoshowVars.ResetMem();
            Environment.Exit(0);
        }
        #endregion
    }

    public static class GetChildrenExtension
    {
        //Credit to BrainSlugs83 for the GetChildren Method (https://stackoverflow.com/questions/874380/wpf-how-do-i-loop-through-the-all-controls-in-a-window) 
        public static IEnumerable<Visual> GetChildren(this Visual parent, bool recurse = true)
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
}
