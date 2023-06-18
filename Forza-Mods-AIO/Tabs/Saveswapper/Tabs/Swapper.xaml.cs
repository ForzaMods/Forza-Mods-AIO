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
using System.IO;
using System.Runtime.CompilerServices;

namespace Forza_Mods_AIO.Tabs.Saveswapper.Tabs
{
    public partial class Swapper : Page
    {
        #region Variables
        public static Mem m = new Mem();
        string SelectedFilePath;
        public static string BaseDirectory = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper";
        bool Attached = false;
        #endregion

        public Swapper()
        {
            InitializeComponent();
            ImportedSavesImporter();
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

        void GamertagResolver()
        {

        }

        void ImportedSavesImporter()
        {
            var ImpSaveDir = BaseDirectory + @"\Imported saves";
            string[] Files = Directory.GetFiles(ImpSaveDir);

            foreach (string file in Files)
            {
                string fileName = Path.GetFileName(file);
                SavesBox.Items.Add(fileName);
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedFilePath = openFileDialog.FileName;
                var fileName = Path.GetFileName(SelectedFilePath);

                if (string.IsNullOrEmpty(Path.GetExtension(fileName)))
                {
                    if (!File.Exists(BaseDirectory + @"\Imported Saves" + "/" + fileName))
                    {
                        File.Copy(SelectedFilePath, BaseDirectory + @"\Imported Saves" + "/" + fileName);
                        SavesBox.Items.Add(fileName);
                    }
                    else
                    {
                        MessageBox.Show("Save with this name already exists in your save folder", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Select a valid save first smh", "Error");
                }
            }
        }

        private void BackupButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SwapButton_Click(object sender, RoutedEventArgs e)
        {
            SwapMSSave();
        }
    }
}
