using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Forza_Mods_AIO.Tabs.Saveswapper
{
    public partial class Saveswapper : Page
    {
        bool SwapperVisibility = true;

        public Saveswapper()
        {
            Task.Run(SwapperBGWorker);
            InitializeComponent();
        }

        /* Wondering why this exists?
         * Because frames in this are a huge peice of shit and dont want to show if they are used the normal way
         * So ive came up with a solution: BG worker that sets the visibility for this
         * Pretty easy to understand i guess
         * Only needed for when you first open the page
         */
        void SwapperBGWorker()
        {
            while (true)
            {
                if(SwapperVisibility)
                {
                    Dispatcher.BeginInvoke((Action)delegate () {
                        SwapperFrame.Visibility = Visibility.Visible;
                    });
                }
                if(!SwapperVisibility)
                {
                    Dispatcher.BeginInvoke((Action)delegate () {
                        SwapperFrame.Visibility = Visibility.Hidden;
                    });
                }
                Thread.Sleep(100);
            }
        }

        public void SwapperButton(object sender, RoutedEventArgs e)
        {
            SwapperFrame.Visibility = Visibility.Visible;
            GuideFrame.Visibility = Visibility.Hidden;
            SwapperVisibility = true;
        }
        private void GuideButton(object sender, RoutedEventArgs e)
        {
            SwapperFrame.Visibility = Visibility.Hidden;
            GuideFrame.Visibility = Visibility.Visible;
            SwapperVisibility = false;
        }
    }
}
 