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
using Microsoft.Win32;
using Memory;

namespace Forza_Mods_AIO.Tabs.Saveswapper.Tabs
{
    public partial class Swapper : Page
    {
        #region Variables
        public static Mem m = new Mem();
        string SelectedFilePath;
        string Path;
        bool Attached = false;
        #endregion

        public Swapper()
        {
            InitializeComponent();
            Task.Run(XboxAttach);
        }

        void XboxAttach()
        {
            while(true)
            {
                Thread.Sleep(1000);
                if (!m.OpenProcess("XboxApp"))
                {
                    Attached = false;
                    return;
                }

                Attached = true;
            }
        }

        void SwapMSSave()
        {

        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedFilePath = openFileDialog.FileName;
                
            }
        }

        private void BackupButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SwapButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
