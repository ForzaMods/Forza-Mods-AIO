using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Memory;

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

    public partial class MainWindow : Window
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
        #region Variables
        public static MainWindow mw = new MainWindow();
        Mem m = new Mem();
        List<Page> tabs = new List<Page>() { new Tabs.AutoShow() };
        private readonly BackgroundWorker IsAttachedWorker = new BackgroundWorker();
        public GameVerPlat gvp = new GameVerPlat(null, null, null);
        #endregion
        #region Starting
        public MainWindow()
        {
            InitializeComponent();
            mw = this;
            IsAttachedWorker.DoWork += IsAttachedWorker_DoWork;
            IsAttachedWorker.RunWorkerAsync();
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
        private void Exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (RadioButton rb in ButtonStack.Children)
            {
                if ((bool)rb.IsChecked)
                {
                    rb.Background = CustomTheming.Monet.DarkerColour;
                    foreach (Page w in tabs)
                        if (w.Title == rb.Name)
                        {
                            foreach (FrameworkElement Element in Window.GetChildren(true))
                            {
                                if (Element.Name == rb.Name + "Frame")
                                    Element.Visibility = Visibility.Visible;
                            }
                        }
                }
                else
                    rb.Background = CustomTheming.Monet.MainColour;
            }
        }
        private void WallButton_Click(object sender, RoutedEventArgs e)
        {
            CustomTheming.Monet.ApplyMonet();
        }
        #endregion
        #region Attaching/Behaviour
        private void IsAttachedWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if(m.OpenProcess("ForzaHorizon5"))
                {
                    gvpMaker(5);
                    Dispatcher.BeginInvoke((Action)delegate () {
                        AttachedLabel.Content = $"{gvp.Name}, {gvp.Plat}, has been detected, attaching now (be in-game)";
                    });
                }
                else if (m.OpenProcess("ForzaHorizon4"))
                {
                    gvpMaker(4);
                    Dispatcher.BeginInvoke((Action)delegate () {
                        AttachedLabel.Content = $"{gvp.Name}, {gvp.Plat}, has been detected, attaching now (be in-game)";
                    });
                }
                else
                {
                    AttachedLabel.Content = "Launch FH4/5";
                }
            }
        }
        private void gvpMaker(int Ver)
        {
            Process process = Process.GetProcessesByName("ForzaHorizon" + Ver.ToString())[0];
            string platform;
            if (process.MainModule.FileName.Contains("Microsoft.624F8B84B80") || process.MainModule.FileName.Contains("Microsoft.SunriseBaseGame"))
                platform = "MS";
            else
                platform = "Steam";
            gvp = new GameVerPlat("Forza Horizon " + Ver.ToString(), platform, process);
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

                    if (child != null)
                    {
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
