using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
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
using Forza_Mods_AIO.Tabs.AIO_Info;
using Forza_Mods_AIO.Tabs.Self_Vehicle;
using Forza_Mods_AIO.Tabs.AutoShowTab;
using Forza_Mods_AIO.Tabs.TuningTablePort;

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
            public string Name { get; set; }
            public string Plat { get; set; }
            public Process Process { get; set; }
            public string Update { get; set; }

            public GameVerPlat(string name, string plat, Process process, string update)
            {
                Name = name; Plat = plat; Process = process; Update = update;
            }
        }

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(int hwnd);

        #region Variables
        public static MainWindow mw = new MainWindow();
        public Mem m = new Mem();
        public static AIO_Info AInfo = new AIO_Info();
        List<Page> tabs = new List<Page>() { new Tabs.AIO_Info.AIO_Info(), new Tabs.AutoShow(), new Tabs.Self_Vehicle.Self_Vehicle(), new Tabs.TuningTablePort.TuningTableMain(), new Tabs.Settings.Settings() };
        public GameVerPlat gvp = new GameVerPlat(null, null, null, null);
        public string Page_Focused = "AIO-Info";
        Dictionary<string, bool> Is_Scanned = new Dictionary<string, bool>()
        {
            { "AutoShow", false },
            { "Self_Vehicle", false },
            { "TuningTableMain", false }
        };
        #endregion
        #region Starting
        public MainWindow()
        {
            InitializeComponent();
            mw = this;
            Task.Run(IsAttached);
            #region Saveswapper stuff
            if (!Directory.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Packages\Microsoft.SunriseBaseGame_8wekyb3d8bbwe\SystemAppData\wgs"))
            {
                //Saveswapper.IsEnabled = false;
                //Saveswapper.Foreground = Brushes.DarkGray;
                //SaveFill.Fill = Brushes.DarkGray;
            }
            else
            {
                var BaseDir = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper";
                if (!Directory.Exists(BaseDir)) { Directory.CreateDirectory(BaseDir); }
                if (!Directory.Exists(BaseDir + @"\Imported saves")) { Directory.CreateDirectory(BaseDir + @"\Imported saves"); }
                if (!Directory.Exists(BaseDir + @"\Save backups")) { Directory.CreateDirectory(BaseDir + @"\Save backups"); }
            }
            #endregion
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
            // Slow when debugging therefore shit but whatever

            foreach (RadioButton rb in ButtonStack.Children)
            {
                if ((bool)rb.IsChecked)
                {
                    rb.Background = Monet.DarkerColour;
                    foreach (Page t in tabs)
                    {
                        if (t.Title == rb.Name)
                        {
                            try
                            {
                                foreach (FrameworkElement Element in Window.GetChildren(true))
                                {
                                    string Source = "";
                                    if (Element.GetType() == typeof(Frame))
                                    {
                                        Source = Element.GetType().GetProperty("Name").GetValue(Element).ToString();
                                    }
                                    if (Element.Name == rb.Name + "Frame")
                                    {
                                        Page_Focused = rb.Name;
                                        Element.Visibility = Visibility.Visible;

                                        if (Is_Scanned.TryGetValue(rb.Name, out bool isScanned) && !isScanned)
                                        {
                                            Is_Scanned[rb.Name] = true;
                                            switch (rb.Name)
                                            {
                                                case "AutoShow":
                                                    Task.Run(() => (new AutoshowVars()).Scan());
                                                    break;
                                                case "Self_Vehicle":
                                                    Task.Run(() => (new Self_Vehicle_Addrs()).Scan());
                                                    break;
                                                case "TuningTableMain":
                                                    Task.Run(() => Addresses.Scan());
                                                    break;
                                            }
                                        }
                                    }
                                    else if (Element.GetType() == typeof(Frame) && Source.Contains("Frame"))
                                    {
                                        Element.Visibility = Visibility.Hidden;
                                    }
                                }
                            }
                            catch { }
                        }
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
            bool attached = false;
            while (true)
            {
                Thread.Sleep(1000);
                if (m.OpenProcess("ForzaHorizon5"))
                {
                    if (attached)
                        continue;
                    gvpMaker(5);
                    Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        AttachedLabel.Content = $"{gvp.Name}, {gvp.Plat}, {gvp.Update}";
                    });
                    attached = true;
                }
                else if (m.OpenProcess("ForzaHorizon4"))
                {
                    if (attached)
                        continue;
                    gvpMaker(4);
                    Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        AttachedLabel.Content = $"{gvp.Name}, {gvp.Plat}, {gvp.Update}";
                    });
                    attached = true;
                }
                else
                {
                    if (!attached)
                        continue;
                    Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        AttachedLabel.Content = "Launch FH4/5";
                    });
                    attached = false;
                }
            }
        }

        private void gvpMaker(int Ver)
        {
            string name;
            string platform;
            string update = "unknown";
            Process process;
            try
            {
                process = Process.GetProcessesByName("ForzaHorizon" + Ver.ToString())[0];
                if (process.MainModule.FileName.Contains("Microsoft.624F8B84B80") || process.MainModule.FileName.Contains("Microsoft.SunriseBaseGame"))
                {
                    platform = "MS";
                    var xml = XElement.Load(process.MainModule.FileName.Substring(0, (process.MainModule.FileName.LastIndexOf("\\"))) + "\\appxmanifest.xml").Elements();
                    foreach (var VARIABLE in xml)
                    {
                        if (VARIABLE.ToString().Contains(" Version=\"") && !VARIABLE.ToString().Contains("Version=\"14.0\""))
                        {
                            update = VARIABLE.Attribute("Version").ToString().Remove(0, 9);
                            update = update.Remove((update.Length - 1), 1);
                        }
                    }
                }
                else
                {
                    platform = "Steam";
                    var file = FileVersionInfo.GetVersionInfo(process.MainModule.FileName.ToString());
                    update = file.FileVersion;
                }
                name = "Forza Horizon " + Ver.ToString();
            }
            catch { name = null; platform = null; process = null; update = null; }
            gvp = new GameVerPlat(name, platform, process, update);
        }
        #endregion
        #region Exit Handling
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //TODO Cleanup here
            Environment.Exit(0);
        }
        #endregion
    }

    public static class GetChildrenExtension
    {
        //Credit to BrainSlugs83 for the GetChildren Method (https://stackoverflow.com/questions/874380/wpf-how-do-i-loop-through-the-all-controls-in-a-window) 
        public static IEnumerable<Visual> GetChildren(this Visual parent, bool recurse = true)
        {
            if (parent != null)
            {
                int count = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < count; i++)
                {
                    // Retrieve child visual at specified index value.
                    var child = VisualTreeHelper.GetChild(parent, i) as Visual;

                    if (child != null && !child.GetType().ToString().Contains("MahApps.Metro.IconPacks"))
                    {
                        try { _ = (FrameworkElement)child; }
                        catch { continue; }

                        yield return child;

                        if (recurse)
                        {
                            foreach (var grandChild in child.GetChildren(true))
                            {
                                yield return grandChild;
                            }
                        }
                    }
                }
            }
        }
    }
}
