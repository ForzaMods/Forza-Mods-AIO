using Forza_Mods_AIO.Properties;
using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Forza_Mods_AIO.TabForms
{
    public partial class StatsEditor : Form
    {
        List<long> yeet = new List<long>();
        List<string> yeetstring = new List<string>();
        List<string> yeetstring2 = new List<string>();
        List<long> AddrList = new List<long>();
        DataTable StatsTableData = new DataTable();
        public static StatsEditor s;
        private static CultureInfo resourceCulture;
        string Version;
        internal static byte[] TqS77kzQrU
        {
            get
            {
                return (byte[])Resources.ResourceManager.GetObject("TqS77kzQrU", resourceCulture);
            }
        }
        internal static byte[] jTZka9MUBu
        {
            get
            {
                return (byte[])Resources.ResourceManager.GetObject("jTZka9MUBu", resourceCulture);
            }
        }
        public StatsEditor()
        {
            InitializeComponent();
            StatsTable.AutoGenerateColumns = false;
            ScanMarquee.Visible = false;
            SendProgress.Visible = false;
            SendButton.Enabled = false;
            FilterBox.Visible = false;
            StatsTableData.Columns.Add("Key", typeof(string));
            StatsTableData.Columns.Add("Value", typeof(string));
            s = this;
        }
        private void StatsEditor_Load(object sender, EventArgs e)
        {
        }

        private async void StatScanButton_Click(object sender, EventArgs e)
        {
            try
            {
                FilterBox.Visible = false;
                StatsTable.DataSource = null;
                StatsTable.Rows.Clear();
                StatsTableData.Clear();
                yeetstring.Clear();
                yeetstring2.Clear();
                yeet.Clear();
                StatScanButton.Enabled = false;
                ScanMarquee.Visible = true;
                ScanMarquee.StartWaiting();
                if (MainWindow.main.ForzaFour)
                {
                    Version = "TqS77kzQrU.csv";
                    File.WriteAllBytes(Path.Combine(Path.GetTempPath(), Version), TqS77kzQrU);
                }
                else
                {
                    Version = "jTZka9MUBu.csv";
                    File.WriteAllBytes(Path.Combine(Path.GetTempPath(), Version), jTZka9MUBu);
                }
                string nameColumnName = "Name";
                string valueColumnName = "Type";
                string Type = null;
                long ScanStartAddr = (long)MainWindow.m.GetCode(Speedhack.FrontRightAddr) - 50000000000;
                long ScanEndAddr = (long)MainWindow.m.GetCode(Speedhack.FrontRightAddr) - 20000000000;
                if(MainWindow.main.ForzaFour)
                {
                    if (Process.GetProcessesByName("ForzaHorizon4")[0].MainModule.FileName.Contains("Microsoft.SunriseBaseGame"))
                        yeet = (await MainWindow.m.AoBScan(ScanStartAddr, ScanEndAddr, "F8 5F ? ? ? 7F 00 00", true, true)).ToList();
                    else
                        yeet = (await MainWindow.m.AoBScan(ScanStartAddr, ScanEndAddr, "80 BF ? ? ? 7F 00 00", true, true)).ToList();
                }
                else
                {
                    if (Process.GetProcessesByName("ForzaHorizon5")[0].MainModule.FileName.Contains("Microsoft.624F8B84B80"))
                        yeet = (await MainWindow.m.AoBScan(ScanStartAddr, ScanEndAddr, "58 13 ? ? ? 7F 00 00", true, true)).ToList();
                    else
                        yeet = (await MainWindow.m.AoBScan(ScanStartAddr, ScanEndAddr, "48 93 ? ? ? 7F 00 00", true, true)).ToList();
                    
                }
                foreach (var item in yeet)
                {
                    if (MainWindow.m.ReadString((item - 76).ToString("X"), zeroTerminated: true).Length > 1
                        && Regex.IsMatch(MainWindow.m.ReadString((item - 76).ToString("X"), zeroTerminated: true), @"^[a-zA-Z]+$"))
                    {
                        using (CsvReader csvReader = new CsvReader(new StreamReader(Path.Combine(Path.GetTempPath(), Version)), hasHeaders: true))
                        {
                            int nameColumnIndex = csvReader.GetFieldIndex(nameColumnName);
                            int valueColumnIndex = csvReader.GetFieldIndex(valueColumnName);

                            while (csvReader.ReadNextRecord())
                            {
                                if (csvReader[nameColumnIndex] == MainWindow.m.ReadString((item - 76).ToString("X"), zeroTerminated: true))
                                {
                                    Type = csvReader[valueColumnIndex];
                                    break;
                                }
                            }
                        }
                        yeetstring.Add(MainWindow.m.ReadString((item - 76).ToString("X"), zeroTerminated: true));
                        if (Type == "Float")
                            yeetstring2.Add((MainWindow.m.ReadFloat((item + 8).ToString("X"), round: false)*100).ToString());
                        else
                            yeetstring2.Add(MainWindow.m.ReadInt((item + 8).ToString("X")).ToString());
                        AddrList.Add(item);
                    }
                }

                for (int i = 0; i < yeetstring.Count; i++)
                {
                    StatsTableData.Rows.Add(yeetstring[i], yeetstring2[i]);
                }
                System.IO.File.WriteAllLines("Stats.txt", yeetstring);
                try { File.Delete(Path.Combine(Path.GetTempPath(), Version)); }
                catch { }
                StatsTable.DataSource = StatsTableData;
                StatsTable.Update();
                StatsTable.Refresh();
                StatsTable.Sort(StatsTable.Columns[0], 0);
                StatsScrollBar.Maximum = StatsTable.Rows.Count - 17;
                StatsScrollBar.Visible = true;
                StatsTable.Width = 961;
                ScanMarquee.Visible = false;
                ScanMarquee.StopWaiting();
                StatScanButton.Enabled = true;
                SendButton.Enabled = true;
                FilterBox.Visible = true;
                if (FilterBox.Text == "Filter")
                    FilterBox.Text.PadLeft(FilterBox.Text.Length + 15);
            }
            catch
            {
                try { File.Delete(Path.Combine(Path.GetTempPath(), Version)); }
                catch { }
                StatsTable.DataSource = StatsTableData;
                StatsTable.Update();
                StatsTable.Refresh();
                StatsTable.Sort(StatsTable.Columns[0], 0);
                StatsScrollBar.Maximum = StatsTable.Rows.Count - 17;
                StatsScrollBar.Visible = true;
                StatsTable.Width = 961;
                ScanMarquee.Visible = false;
                ScanMarquee.StopWaiting();
                StatScanButton.Enabled = true;
                SendButton.Enabled = true;
                FilterBox.Visible = true;
                if (FilterBox.Text == "Filter")
                    FilterBox.Text.PadLeft(FilterBox.Text.Length + 15);
            }

        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            if (!SendWorker.IsBusy)
            {
                StatScanButton.Enabled = false;
                SendButton.Enabled = false;
                SendProgress.Visible = true;
                SendWorker.RunWorkerAsync();
            }
        }

        private void SendWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            if (MainWindow.main.ForzaFour)
            {
                File.WriteAllBytes(Path.Combine(Path.GetTempPath(), Version), TqS77kzQrU);
            }
            else
            {
                File.WriteAllBytes(Path.Combine(Path.GetTempPath(), Version), jTZka9MUBu);
            }
            foreach (var item in AddrList)
            {
                string nameColumnName = "Name";
                string valueColumnName = "Type";
                string Type = null;
                using (CsvReader csvReader = new CsvReader(new StreamReader(Path.Combine(Path.GetTempPath(), Version)), hasHeaders: true))
                {
                    int nameColumnIndex = csvReader.GetFieldIndex(nameColumnName);
                    int valueColumnIndex = csvReader.GetFieldIndex(valueColumnName);

                    while (csvReader.ReadNextRecord())
                    {
                        if (csvReader[nameColumnIndex] == MainWindow.m.ReadString((item - 76).ToString("X"), zeroTerminated: true))
                        {
                            Type = csvReader[valueColumnIndex];
                            break;
                        }
                    }
                }
                if (Type == "Float")
                    try { MainWindow.m.WriteMemory((item + 8).ToString("X"), "float", ((float)StatsTableData.Rows[i][1] / 100).ToString()); } catch { }
                else
                    try { MainWindow.m.WriteMemory((item + 8).ToString("X"), "int", StatsTableData.Rows[i][1].ToString()); } catch { }
                int progress = (int)(100 * i / AddrList.Count);
                SendWorker.ReportProgress(progress);
                i++;
            }
            File.Delete(Path.Combine(Path.GetTempPath(), Version));
            SendWorker.ReportProgress(100);
            StatScanButton.Enabled = true;
            SendButton.Enabled = true;
        }

        private void SendWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SendProgress.Value1 = e.ProgressPercentage;
        }

        private void SendWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SendProgress.Visible = false;
            SendProgress.Value1 = 0;
        }

        private void FilterBox_TextChanged(object sender, EventArgs e)
        {
            FilterBox.Text.PadLeft(FilterBox.Text.Length + 15);
            StatsTableData.DefaultView.RowFilter = string.Concat("CONVERT(Key,System.String) LIKE '%", FilterBox.Text, "%'");
            if (StatsTable.Rows.Count >= 17)
                StatsScrollBar.Maximum = StatsTable.Rows.Count - 17;
            else
                StatsScrollBar.Maximum = 0;
            StatsScrollBar.Value = 0;
        }

        private void FilterBox_Enter(object sender, EventArgs e)
        {
            if(FilterBox.Text == "Filter")
                FilterBox.Clear();
        }

        private void FilterBox_Leave(object sender, EventArgs e)
        {
            if (FilterBox.Text == "")
            {
                FilterBox.Text = "Filter";
                FilterBox.Text.PadLeft(FilterBox.Text.Length + 15);
                StatsTableData.DefaultView.RowFilter = String.Empty;
            }
        }

        private void FilterBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (FilterBox.Text == "Filter")
                FilterBox.Clear();
        }

        private void StatScanButton_Leave(object sender, EventArgs e)
        {
            StatScanButton.NotifyDefault(false);
        }
        private void StatsTable_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                StatsScrollBar.Value = e.NewValue;
            }
        }

        private void StatsScrollBar_Scroll(object sender, DarkUI.Controls.ScrollValueEventArgs e)
        {
            StatsTable.FirstDisplayedScrollingRowIndex = e.Value;
        }
    }
}
