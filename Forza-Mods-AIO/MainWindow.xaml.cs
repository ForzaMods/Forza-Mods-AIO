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
using Forza_Mods_AIO.Tabs.Tuning;

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
        public static MainWindow mw = new();
        public Mem m = new();
        public GameVerPlat gvp = new(null, null, null, null);
        public string PageFocused = "AIO_Info";
        public bool Attached;
        public LibraryMapper Mapper;
        public bool WasMapped = false;
        IEnumerable<Visual> Visuals;
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
            ThemeManager.Current.AddTheme(new Theme("AccentCol", "AccentCol", "Dark", "Red", (Color)ColorConverter.ConvertFromString("#FF2E3440")!, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2E3440")!), true, false));
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
            string rbName = "";

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

            Visuals ??= Window.GetChildren();

            PageFocused = rbName;
            
            foreach (var Element in Visuals.Cast<FrameworkElement>())
            {
                // Page is RB.name + Frame
                if (Element.Name == rbName + "Frame")
                {
                    PageFocused = rbName;
                    Element.Visibility = Visibility.Visible;
                }

                // Page is not RB.name + Frame
                else if (Element is Frame frame && frame.Name.Contains("Frame"))
                {
                    Element.Visibility = Visibility.Hidden;
                }

                // Scanned is true
                if (!(Is_Scanned.TryGetValue(rbName, out var isScanned) && !isScanned && Attached))
                {
                    continue;
                }

                // Scanned is not true, scan
                Is_Scanned[rbName] = true;
                switch (rbName)
                {
                    case "AutoShow":
                        Task.Run(() => AutoshowVars.Scan());
                        break;
                    case "Self_Vehicle":
                        if (gvp.Name == "Forza Horizon 5")
                            Task.Run(() => new Self_Vehicle_Addrs().FH5_Scan());
                        else
                            Task.Run(() => new Self_Vehicle_Addrs().FH4_Scan());
                        break;
                    case "Tuning":
                        Task.Run(() => Tuning_Addresses.Scan());
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
                Thread.Sleep(500);
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
                    ResetAIO();
                    Attached = false;
                }
            }
        }

        private void GvpMaker(int ver)
        {
            string platform, update = "Unknown", name = $"Forza Horizon {ver}";
            var process = m.MProc.Process;
            
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
                try
                {
                    update = FileVersionInfo.GetVersionInfo(process.MainModule!.FileName).FileVersion;
                }
                catch { update = "Unknown"; }
            }
            gvp = new GameVerPlat(name, platform, process, update);
            
            Dispatcher.Invoke(delegate {
                AttachedLabel.Content = $"{gvp.Name}, {gvp.Plat}, {gvp.Update}";
                Tabs.AIO_Info.AIO_Info.ai.OverlaySwitch.IsEnabled = true;
            });
        }

        private void ResetAIO()
        {
            Dispatcher.Invoke(delegate {
                AttachedLabel.Content = "Launch FH4/5";
                Tabs.Tuning.Tuning.TBM.AOBProgressBar.Value = 0;
                Tabs.Self_Vehicle.Self_Vehicle.sv.AOBProgressBar.Value = 0;
                Tabs.AutoShow.AS.AOBProgressBar.Value = 0;
                Tabs.AIO_Info.AIO_Info.ai.OverlaySwitch.IsEnabled = false;
                AIO_Info.IsChecked = true;
                CategoryButton_Click(new object(), new RoutedEventArgs());
            });
            Is_Scanned["Autoshow"] = false;
            Is_Scanned["Self_Vehicle"] = false;
            Is_Scanned["Tuning"] = false;

            ASM.CreditsFirstTime = true;
            ASM.XpFirstTime = true;
            ASM.GlowingPaintFirstTime = true;
            ASM.RemoveBuildCapFirstTime = true;
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
            if (WasMapped && Mapper.DllBaseAddress != IntPtr.Zero)
            {
                Mapper.UnmapLibrary();
            }

            if (Self_Vehicle_Addrs.BaseAddrHook != null && Self_Vehicle_Addrs.BaseAddrHook != "0" && Self_Vehicle_Addrs.BaseAddrHookLong != -279 && ASM.OriginalBaseAddressHookBytes != null)
            {
                m.WriteArrayMemory(Self_Vehicle_Addrs.BaseAddrHook, ASM.OriginalBaseAddressHookBytes);
            }
            
            try
            {
                    
                m.WriteArrayMemory(Tuning_Addresses.TuningTableHook1, gvp.Name == "Forza Horizon 4" ? new byte[] { 0x49, 0x8B, 0x06, 0x8B, 0xD6 } : ASM.OriginalTuningHook1Bytes);
                m.WriteArrayMemory(Tuning_Addresses.TuningTableHook2, gvp.Name == "Forza Horizon 4" ? new byte[] { 0x49, 0x8B, 0x07, 0x48, 0x8D, 0x55, 0x77 } : ASM.OriginalTuningHook2Bytes);
                m.WriteArrayMemory(Tuning_Addresses.TuningTableHook3, gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x28, 0xCE, 0xF3, 0x0F, 0x10, 0x10 } : ASM.OriginalTuningHook3Bytes);
                m.WriteArrayMemory(Tuning_Addresses.TuningTableHook4, gvp.Name == "Forza Horizon 4" ? new byte[] { 0x48, 0x8B, 0x07, 0x48, 0x8D, 0x95, 0x60, 0x02, 0x00, 0x00 } : ASM.OriginalTuningHook4Bytes);
                    
            }
            catch { /* ignored */}
            
            AutoshowVars.ResetMem();
            
            try
            {
                m.WriteArrayMemory(Self_Vehicle_Addrs.XPaddr, new byte[] { 0xF3, 0x0F, 0x2C, 0xC6, 0x89, 0x45, 0xB8 });
                m.WriteArrayMemory(Self_Vehicle_Addrs.XPAmountaddr, gvp.Name == "Forza Horizon 5" ? new byte[] { 0x8B, 0x89, 0xB8, 0x00, 0x00, 0x00 } : new byte[] { 0x8B, 0x89, 0xC0, 0x00, 0x00, 0x00 });
                m.WriteArrayMemory(Self_Vehicle_Addrs.CreditsHookAddr, new byte[] { 0x89, 0x84, 0x24, 0x80, 0x00, 0x00, 0x00 });
                m.WriteArrayMemory(Self_Vehicle_Addrs.BuildCapAddrASM1, gvp.Name == "Forza Horizon 5" ? new byte[] { 0xF3, 0x0F, 0x11, 0x83, 0x4C, 0x04, 0x00, 0x00 } : new byte[] { 0xF3, 0x0F, 0x11, 0xB3, 0xDC, 0x03, 0x00, 0x00 });
                m.WriteArrayMemory(Self_Vehicle_Addrs.BuildCapAddrASM2, gvp.Name == "Forza Horizon 5" ? new byte[] { 0xF3, 0x0F, 0x11, 0x43, 0x44 } : new byte[] { 0xF3, 0x0F, 0x11, 0x43, 0x30 });
                m.WriteArrayMemory(Self_Vehicle_Addrs.UnbSkillHookAddr, new byte[] { 0x48, 0x8B, 0x10, 0x48, 0x8B, 0xC8 });
                m.WriteArrayMemory(Self_Vehicle_Addrs.WayPointxASMAddr, gvp.Name == "Forza Horizon 5" ? new byte[] { 0x0F, 0x10, 0x97, 0x50, 0x02, 0x00, 0x00 } : new byte[] { 0x0F, 0x10, 0xA0, 0x90, 0x03, 0x00, 0x00 });
                m.WriteArrayMemory(Self_Vehicle_Addrs.GlowingPaintAddr, gvp.Name == "Forza Horizon 4" ? new byte[] { 0x41, 0x0F, 0x11, 0x4A, 0x10 } : new byte[] { 0x0F, 0x11, 0x0A, 0xC6, 0x42, 0xF0, 0x01 });
                m.WriteArrayMemory(Self_Vehicle_Addrs.TimeNOPAddr, new byte[] { 0xF2, 0x0F, 0x11, 0x43, 0x08, 0x48, 0x83, 0xC4, 0x40 });

                if (gvp.Name == "Forza Horizon 5")
                {
                    m.WriteArrayMemory((Self_Vehicle_Addrs.SuperCarAddrLong - 4).ToString("X"), new byte[] { 0x0F, 0x11, 0x41, 0x10 });
                    m.WriteArrayMemory((Self_Vehicle_Addrs.SuperCarAddrLong + 4).ToString("X"), new byte[] { 0x0F, 0x11, 0x49, 0x20 });
                    m.WriteArrayMemory((Self_Vehicle_Addrs.SuperCarAddrLong + 12).ToString("X"), new byte[] { 0x0F, 0x11, 0x41, 0x30 });
                    m.WriteArrayMemory((Self_Vehicle_Addrs.SuperCarAddrLong + 20).ToString("X"), new byte[] { 0x0F, 0x11, 0x49, 0x40 });
                    m.WriteArrayMemory((Self_Vehicle_Addrs.SuperCarAddrLong + 32).ToString("X"), new byte[] { 0x0F, 0x11, 0x41, 0x50 });

                    m.WriteArrayMemory(Self_Vehicle_Addrs.BaseAddrHook, new byte[] { 0xF3, 0x0F, 0x10, 0x81, 0xDC, 0x14, 0x00, 0x00 });
                    m.WriteArrayMemory(Self_Vehicle_Addrs.RotationAddr, new byte[] { 0xF3, 0x44, 0x0F, 0x10, 0x89, 0x94, 0x00, 0x00, 0x00 });
                }
                else
                {
                    m.WriteArrayMemory((Self_Vehicle_Addrs.SuperCarAddrLong + 4).ToString("X"), new byte[] { 0x0F, 0x11, 0x41, 0x10 });
                    m.WriteArrayMemory((Self_Vehicle_Addrs.SuperCarAddrLong + 12).ToString("X"), new byte[] { 0x0F, 0x11, 0x49, 0x20 });
                    m.WriteArrayMemory((Self_Vehicle_Addrs.SuperCarAddrLong + 20).ToString("X"), new byte[] { 0x0F, 0x11, 0x41, 0x30 });
                    m.WriteArrayMemory((Self_Vehicle_Addrs.SuperCarAddrLong + 32).ToString("X"), new byte[] { 0x0F, 0x11, 0x49, 0x40 });

                    m.WriteArrayMemory(Self_Vehicle_Addrs.Car2Addr, new byte[] { 0x0F, 0x84, 0x3A, 0x03, 0x00, 0x00 });
                    
                    m.WriteArrayMemory(Self_Vehicle_Addrs.FOVnopOutAddr, new byte[] { 0x0F, 0x11, 0x43, 0x10 });
                    m.WriteArrayMemory(Self_Vehicle_Addrs.FOVnopInAddr, new byte[] { 0x0F, 0x11, 0x73, 0x10 });
                    m.WriteMemory<byte>(Self_Vehicle_Addrs.FOVJmpAddr, 0x76);
                }

                m.WriteMemory(Self_Vehicle_Addrs.WorldRGBAddr,  0.003921568859f);
                m.WriteMemory((Self_Vehicle_Addrs.WorldRGBAddrLong + 4).ToString("X"), 0.003921568859f);
                m.WriteMemory((Self_Vehicle_Addrs.WorldRGBAddrLong + 8).ToString("X"), 0.003921568859f);

                m.WriteArrayMemory(Self_Vehicle_Addrs.Wall1Addr, gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x84, 0x29, 0x02, 0x00, 0x00 } : new byte[] { 0x0F, 0x84, 0x60, 0x02, 0x00, 0x00 });
                m.WriteArrayMemory(Self_Vehicle_Addrs.Wall2Addr, gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x84, 0x2A, 0x02, 0x00, 0x00 } : new byte[] { 0x0F, 0x84, 0x7E, 0x02, 0x00, 0x00 });
                m.WriteArrayMemory(Self_Vehicle_Addrs.Car1Addr, gvp.Name == "Forza Horizon 4" ? new byte[] { 0x0F, 0x84, 0xB5, 0x01, 0x00, 0x00 } : new byte[] { 0x0F, 0x84, 0x65, 0x03, 0x00, 0x00 }); 
                m.WriteArrayMemory(Self_Vehicle_Addrs.WaterAddr, new byte[] { 0xCD, 0xCC, 0x4C, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x67, 0x45, 0x00, 0xF0, 0x52, 0x46, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x00, 0xCD, 0xCC, 0xCC, 0x3D, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x00,0x00, 0x40, 0xC4, 0x44, 0x00, 0x00, 0xFF, 0x44, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x48, 0x42, 0x00, 0x00, 0xC8, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x40, 0x00, 0x00, 0x70, 0x41 });
                m.WriteArrayMemory(Self_Vehicle_Addrs.AIXAobAddr, new byte[] { 0x0F, 0x11, 0x41, 0x50, 0x48, 0x8B, 0xFA });

                m.WriteMemory<byte>(Self_Vehicle_Addrs.FOVJmpAddr, 0xEB);
            }
            catch { /* ignored */ }


            ASM.FreeMem();
            
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
