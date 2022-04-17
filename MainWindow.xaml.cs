using System.Windows;
using System.Windows.Input;

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
        public static MainWindow mw;
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
            foreach (System.Windows.Controls.RadioButton rb in ButtonStack.Children)
            {
                if ((bool)rb.IsChecked)
                    rb.Background = CustomTheming.Monet.DarkerColour;
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
}
