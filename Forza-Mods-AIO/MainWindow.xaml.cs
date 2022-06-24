using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using WPF_Mockup.CustomTheming;

namespace WPF_Mockup
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
            public GameVerPlat(string name, string plat, Process process)
            {
                Name = name; Plat = plat; Process = process; 
            }
        }

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(int hwnd);

        #region Variables
        public static MainWindow mw = new MainWindow();
        public Mem m = new Mem();
        List<Page> tabs = new List<Page>() { new Tabs.AIO_Info.AIO_Info(), new Tabs.AutoShow(), new Tabs.Self_Vehicle.Self_Vehicle() };
        public GameVerPlat gvp = new GameVerPlat(null, null, null);
        #endregion
        #region Starting
        public MainWindow()
        {
            InitializeComponent();
            mw = this;
            Task.Run(() => IsAttached());
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
                if ((bool)rb.IsChecked)
                {
                    rb.Background = Monet.DarkerColour;
                    foreach (Page t in tabs)
                        if (t.Title == rb.Name)
                        {
                            try
                            {
                                foreach (FrameworkElement Element in Window.GetChildren(true))
                                {
                                    string Source = "";
                                    if( Element.GetType() == typeof(Frame) )
                                        Source = Element.GetType().GetProperty("Name").GetValue(Element).ToString();
                                    if (Element.Name == rb.Name + "Frame")
                                        Element.Visibility = Visibility.Visible;
                                    else if (Element.GetType() == typeof(Frame) && Source.Contains("Frame"))
                                        Element.Visibility = Visibility.Hidden;
                                }
                            }
                            catch { }
                        }
                }
                else
                    rb.Background = Monet.DarkishColour;
            }
        }
        #endregion
        #region Attaching/Behaviour
        private void IsAttached()
        {
            bool attached = false;
            while (true)
            {
                Thread.Sleep(500);
                if (m.OpenProcess("ForzaHorizon5"))
                {
                    if (attached)
                        continue;
                    gvpMaker(5);
                    Dispatcher.BeginInvoke((Action)delegate () {
                        AttachedLabel.Content = $"{gvp.Name}, {gvp.Plat}";
                    });
                    attached = true;
                }
                else if (m.OpenProcess("ForzaHorizon4"))
                {
                    if (attached)
                        continue;
                    gvpMaker(4);
                    Dispatcher.BeginInvoke((Action)delegate () {
                        AttachedLabel.Content = $"{gvp.Name}, {gvp.Plat}";
                    });
                    attached = true;
                }
                else
                {
                    if (!attached)
                        continue;
                    Dispatcher.BeginInvoke((Action)delegate () {
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
            Process process;
            try
            {
                process = Process.GetProcessesByName("ForzaHorizon" + Ver.ToString())[0];
                if (process.MainModule.FileName.Contains("Microsoft.624F8B84B80") || process.MainModule.FileName.Contains("Microsoft.SunriseBaseGame"))
                    platform = "MS";
                else
                    platform = "Steam";
                name = "Forza Horizon " + Ver.ToString();
            }
            catch { name = null; platform = null; process = null; }
            gvp = new GameVerPlat(name, platform, process);
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
