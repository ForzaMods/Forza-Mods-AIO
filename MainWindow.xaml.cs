using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        #region Variables
        public MainWindow mw = new MainWindow();
        List<Page> windows = new List<Page>() { new Tabs.AutoShow() };
        #endregion
        #region Starting
        public MainWindow()
        {
            InitializeComponent();
            mw = this;
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
                    foreach(Page w in windows)
                        if(w.Title == rb.Name)
                        {
                            foreach(FrameworkElement Element in Window.GetChildren(true))
                            {
                                if(Element.Name == rb.Name + "Frame")
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
