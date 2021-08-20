using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Memory;
using ContainerReader;
using BaseNcoding;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Forza_Mods_AIO.TabForms
{
    public partial class Saveswapper : Form
    {
        public Saveswapper()
        {
            InitializeComponent();
            s = this;
        }
        Stopwatch AuthTimer = new Stopwatch();
        List<string> gamertags = new List<string>();
        Mem sm = new Mem();
        bool attached = false;
        string[] savemetadata = null;
        public PopupForms.SaveswappingGuide SaveswapGuide = new PopupForms.SaveswappingGuide();
        bool resolving = false;
        string auth = null;
        public static Saveswapper s;
        public void GamebarAttach_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (!sm.OpenProcess("XboxApp"))
                {
                    attached = false;
                    Thread.Sleep(1000);
                    return;
                }

                attached = true;

            }
        }

        public void GamebarAttach_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GamebarAttach.RunWorkerAsync();
        }


        string Get(string uri, string auth)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Headers.Add("x-xbl-client-type", "UWA");
            request.Headers.Add("x-xbl-contract-version", "1");
            request.Headers.Add("Accept-Encoding", "gzip; q=1.0, deflate; q=0.5, identity; q=0.1");
            request.Headers.Add("Cache-Control", "no-store, must-revalidate, no-cache");
            request.Headers.Add("x-xbl-client-name", "XboxApp");
            request.Headers.Add("x-xbl-market", "US");
            request.Headers.Add("PRAGMA", "no-cache");
            request.Headers.Add("Accept-Language", "en-US, en");
            request.Headers.Add("Authorization", auth);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }


        public void BTN_SwapSave_Click(object sender, EventArgs e)
        {

            if (LST_Accounts.SelectedItem != null && LST_Savegames.SelectedItem != null)
            {
                SwapMSSave();
                MessageBox.Show("Saveswap completed successfully");
            }
            else
                MessageBox.Show("Options not selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void SwapMSSave()
        {
            BTN_SwapSave.Enabled = false;
            BTN_Backup.Enabled = false;
            var targetacc = new DirectoryInfo(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Packages\Microsoft.SunriseBaseGame_8wekyb3d8bbwe\SystemAppData\wgs").EnumerateDirectories("*").ToList()[LST_Accounts.SelectedIndex];
            var selectedsave = new DirectoryInfo(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS").EnumerateFiles("*").ToList()[LST_Savegames.SelectedIndex];
            var savepath = container.Read(targetacc.FullName + "\\containers.index");
            //MessageBox.Show("Target profile:" + "\n" + targetacc.FullName + "\n" + "Selected savegame:" + "\n" + selectedsave.FullName + "\n" + "Savegame path:" + "\n" + targetacc.FullName + savepath );
            if (TB_Backup.Checked)
                File.Move(targetacc.FullName + "\\" + savepath, @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\Backups\" + DateTime.Now.ToString("Automatic-yyyy-MM-dd HH-mm-ss-fff"));
            else
                File.Delete(targetacc.FullName + "\\" + savepath);
            File.Copy(selectedsave.FullName.ToString(), targetacc.FullName + "\\"+savepath);
            BTN_SwapSave.Enabled = true;
            BTN_Backup.Enabled = true;
        }
        void BackupMSSave()
        {
            BTN_SwapSave.Enabled = false;
            BTN_Backup.Enabled = false;
            var targetacc = new DirectoryInfo(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Packages\Microsoft.SunriseBaseGame_8wekyb3d8bbwe\SystemAppData\wgs").EnumerateDirectories("*").ToList()[LST_Accounts.SelectedIndex];
            var savepath = container.Read(targetacc.FullName + "\\containers.index");
            var backfolder = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\Backups\";
            if (!Directory.Exists(backfolder))
                Directory.CreateDirectory(backfolder);
            File.Copy(targetacc.FullName + "\\" + savepath,  backfolder + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff"));
            BTN_SwapSave.Enabled = true;
            BTN_Backup.Enabled = true;
        }
        //bg worker stuff. query xbox api for gamertag using the xuids

        public async void GamertagResolve_DoWork(object sender, DoWorkEventArgs e)
        {
            var savelist = new DirectoryInfo(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS").EnumerateFiles("*");
            var acclist = new DirectoryInfo(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Packages\Microsoft.SunriseBaseGame_8wekyb3d8bbwe\SystemAppData\wgs").EnumerateDirectories("*");
            int dircount = 0;
            var resolved = 2;
            LST_Savegames.Items.Clear();
            foreach (var save in savelist)
                LST_Savegames.Items.Add(save);
            LST_Savegames.Update();
            foreach (var dir in acclist)
            {
                if (dir.Name != "t")
                {
                    dircount++;
                }
            }
            if (dircount <= 0)
            {
                MessageBox.Show("No savegames found. Make sure you are using the microsoft store version of the game and own it legitimately", "Error: No Savegames", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!attached)
            {
                resolved = 3;
                LST_Accounts.Items.Clear();
                dircount = 0;
                foreach (var dir in acclist)
                {
                    if (dir.Name != "t")
                    {
                        dircount++;
                        LST_Accounts.Items.Add(dircount + ": Last Played " + dir.LastWriteTime);
                    }
                }
                BTN_ACCRefresh.Enabled = true;
            }
            else
            {
                try
                {
                    if(AuthTimer.Elapsed > TimeSpan.FromMinutes(20))
                        { auth = null; AuthTimer.Reset(); }
                    resolving = true;
                    ResolvingWorker.RunWorkerAsync();
                    bool scandone = false;
                    long length = 0;
                    string address = null;
                    int count = 0;
                    bool done = false;
                    List<long> lengthlist = new List<long>();
                    List<string> addresslist = new List<string>();
                    List<string> authlist = new List<string>();
                    if (!scandone)
                    {
                        IEnumerable<long> scan1 = await sm.AoBScan("41 75 74 68 6F 72 69 7A 61 74 69 6F 6E 58 42 4C 33 2E 30 20 78 3D", true, true);
                        IEnumerable<long> scan2 = await sm.AoBScan("48 6F 73 74 63 6F 6D 6D 65 6E 74 73 2E 78 62 6F 78 6C 69 76 65 2E 63 6F 6D", true, true);
                        foreach (var addr2 in scan2.ToArray())
                        {
                            if (done)
                                break;
                            foreach (var addr1 in scan1.ToArray())
                            {
                                long templength = addr2 - addr1;
                                if (templength < 3500 && templength > 0)
                                {
                                    length = templength - 93;
                                    address = (addr1 + 13).ToString("X");
                                    if (!Encoding.ASCII.GetString(sm.ReadBytes(address, (int)length)).Contains("Content-Length") && !Encoding.ASCII.GetString(sm.ReadBytes(address, (int)length)).Contains(@"\") && !Encoding.ASCII.GetString(sm.ReadBytes(address, (int)length)).Any(c => c > 255))
                                    {
                                        lengthlist.Add(length);
                                        addresslist.Add(address);
                                        //done = true;
                                        //break;
                                    }
                                }
                                else if (count == scan2.Count())
                                    throw new Exception("yeet lol");
                            }
                            count++;
                        }
                        if (lengthlist.Count != 0 && addresslist.Count != 0)
                        {
                            int listcount = 0;
                            foreach(var b in lengthlist)
                            {
                                authlist.Add(Encoding.ASCII.GetString(sm.ReadBytes(addresslist[listcount], (int)b)));
                                listcount++;
                            }
                            scandone = true;
                        }
                            //auth = Encoding.ASCII.GetString(sm.ReadBytes(address, (int)length));
                    }
                    LST_Accounts.Items.Clear();
                    var response = (dynamic)(new JObject());
                    string workingauth = null;
                    foreach (var dir in acclist)
                    {
                        if (dir.Name != "t")
                        {
                            bool authdone = false;
                            try
                            {
                                int retrycount = 0;
                                int authtriedcount = 0;
                                while (authtriedcount < authlist.Count && !authdone)
                                {
                                    while (retrycount < 3)
                                    {
                                        try
                                        {
                                            if (workingauth == null)
                                                auth = authlist[authtriedcount];
                                            else
                                                auth = workingauth;
                                            response = (dynamic)JObject.Parse(Get("https://peoplehub.xboxlive.com/users/me/people/xuids(" + Int64.Parse(dir.Name.Substring(0, 16), System.Globalization.NumberStyles.HexNumber) + ")", auth));
                                            break;
                                        }
                                        catch
                                        {
                                            Thread.Sleep(500);
                                            retrycount++;
                                        }
                                    }
                                    authdone = true;

                                    resolved = 1;
                                    if (!LST_Accounts.Items.Contains(response.people[0].gamertag.ToString()))
                                        LST_Accounts.Items.Add(response.people[0].gamertag.ToString());
                                    else
                                        throw new Exception("yeet lol");
                                }
                                if(authlist.Count == 0)
                                    throw new Exception("yeet lol");
                            }
                            catch (Exception a)
                            {
                                LST_Accounts.Items.Clear();
                                dircount = 0;
                                resolving = false;
                                ResolvingWorker.CancelAsync();
                                LST_Resolved.Text = "Resolving Failed";
                                LST_Resolved.ForeColor = Color.Red;
                                foreach (var dir2 in acclist)
                                {
                                    if (dir2.Name != "t")
                                    {
                                        dircount++;
                                        LST_Accounts.Items.Add(dircount + ": Last Played " + dir2.LastWriteTime);
                                    }
                                }
                                BTN_ACCRefresh.Enabled = true;
                                return;
                            }
                        }
                    }
                }
                catch(Exception a)
                {
                    //MessageBox.Show(a.ToString());
                    LST_Accounts.Items.Clear();
                    dircount = 0;
                    resolving = false;
                    ResolvingWorker.CancelAsync();
                    LST_Resolved.Text = "Resolving Failed";
                    LST_Resolved.ForeColor = Color.Red;
                    foreach (var dir in acclist)
                    {
                        if (dir.Name != "t")
                        {
                            dircount++;
                            LST_Accounts.Items.Add(dircount + ": Last Played " + dir.LastWriteTime);
                            BTN_ACCRefresh.Enabled = true;
                        }
                    }
                }


            }
            if (resolved== 1)
            {
                AuthTimer.Start();
                resolving = false;
                ResolvingWorker.CancelAsync();
                LST_Resolved.Text = "      Resolved";
                LST_Resolved.ForeColor = Color.Green;
            }
            if (resolved == 3)
            {
                ResolvingWorker.CancelAsync();
                LST_Resolved.Text = "";
            }
            BTN_ACCRefresh.Enabled = true;
        }

        public void Saveswapper_Load(object sender, EventArgs e)
        {
            GamebarAttach.RunWorkerAsync();
            GamertagResolve.RunWorkerAsync();
        }


        private void BTN_Backup_Click(object sender, EventArgs e)
        {
            if (LST_Accounts.SelectedItem != null)
            {
                BackupMSSave();
                MessageBox.Show("Savegame has been backed up to: \n " + @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\Backup\", "Save Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Account not selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        public static string GetAlphabet(int charsCount)
        {
            var result = new StringBuilder(charsCount);
            int i = 0;
            int count = 0;
            do
            {
                char c = (char)i;
                if (!char.IsControl(c) && !char.IsWhiteSpace(c))
                {
                    result.Append(c);
                    count++;
                }
                i++;
            }
            while (count < charsCount);

            return result.ToString();
        }

        private void LST_Savegames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (savemetadata==null)
            savemetadata = File.ReadAllText(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\SaveMetadata").Split('Ω');


            //MessageBox.Show(savemetadata[LST_Savegames.SelectedIndex]);
            string alpha = GetAlphabet(844);
            var converter = new BaseN(alpha);
            try
            {
                TXT_SaveInfo.Text = Encoding.ASCII.GetString(converter.Decode(Regex.Replace(savemetadata[LST_Savegames.SelectedIndex], @"\t|\n|\r", String.Empty)));
            }
                catch
            {
                TXT_SaveInfo.Text = "There is no information about this save. \nManually adding metadata will be added at a later date";
            }

        }

        private void BTN_ACCRefresh_Click(object sender, EventArgs e)
        {
            BTN_ACCRefresh.Enabled = false;
            GamertagResolve.RunWorkerAsync();
        }

        void BTN_Help_Click(object sender, EventArgs e)
        {
            SaveswapGuide.Show();
        }

        private void ReolvingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (ResolvingWorker.CancellationPending)
            {
                e.Cancel = true;
            }
            LST_Resolved.ForeColor = Color.White;
            LST_Resolved.Text = "    Resolving";
            if (ResolvingWorker.CancellationPending)
            {
                e.Cancel = true;
            }
            while (resolving)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (ResolvingWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    Thread.Sleep(100);
                    if (ResolvingWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    LST_Resolved.Text += ".";
                }
                if (ResolvingWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                LST_Resolved.Text = "    Resolving";
                if (ResolvingWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        private void TB_Backup_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if(TB_Backup.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)TB_Backup.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
            else
            ((Telerik.WinControls.Primitives.BorderPrimitive)TB_Backup.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = Color.FromArgb(30, 30, 33);
        }
    }
}
