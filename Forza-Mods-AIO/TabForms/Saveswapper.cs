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

namespace Forza_Mods_AIO.TabForms
{
    public partial class Saveswapper : Form
    {
        public Saveswapper()
        {
            InitializeComponent();

        }
        List<string> gamertags = new List<string>();
        Mem sm = new Mem();
        bool attached = false;
        string[] savemetadata = null;
        public PopupForms.SaveswappingGuide SaveswapGuide = new PopupForms.SaveswappingGuide();

        public void GamebarAttach_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (!sm.OpenProcess("XboxApp"))
                {
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
            File.Copy(targetacc.FullName + "\\" + savepath, @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\Backups\" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff"));
            BTN_SwapSave.Enabled = true;
            BTN_Backup.Enabled = true;
        }
        //bg worker stuff. query xbox api for gamertag using the xuids

        public async void GamertagResolve_DoWork(object sender, DoWorkEventArgs e)
        {
            var savelist = new DirectoryInfo(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS").EnumerateFiles("*");
            var acclist = new DirectoryInfo(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Packages\Microsoft.SunriseBaseGame_8wekyb3d8bbwe\SystemAppData\wgs").EnumerateDirectories("*");
            int dircount = 0;
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
            }
            else
            {
                try
                {
                    long length = 0;
                    string address = null;
                    string auth = null;
                    int count = 0;
                    bool done = false;
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
                                break;
                            }
                            else if (count == scan2.Count())
                                throw new Exception("yeet lol");
                        }
                        count++;
                    }
                    if(length != 0 && address != null)
                        auth = Encoding.ASCII.GetString(sm.ReadBytes(address, (int)length));
                    LST_Accounts.Items.Clear();

                    foreach (var dir in acclist)
                    {
                        if (dir.Name != "t")
                        {
                            try
                            {
                                var response = (dynamic)JObject.Parse(Get("https://peoplehub.xboxlive.com/users/me/people/xuids(" + Int64.Parse(dir.Name.Substring(0, 16), System.Globalization.NumberStyles.HexNumber) + ")", auth));
                                LST_Accounts.Items.Add(response.people[0].gamertag.ToString());
                            }
                            catch (Exception a)
                            {
                                MessageBox.Show("Resolving Failed");
                                LST_Accounts.Items.Clear();
                                dircount = 0;
                                foreach (var dir2 in acclist)
                                {
                                    if (dir2.Name != "t")
                                    {
                                        dircount++;
                                        LST_Accounts.Items.Add(dircount + ": Last Played " + dir2.LastWriteTime);

                                    }
                                }
                                return;
                            }

                        }
                    }
                }
                catch(Exception a)
                {
                    MessageBox.Show(a.ToString());
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
                }


            }
            LST_Savegames.Items.Clear();
            foreach (var save in savelist)
                LST_Savegames.Items.Add(save);

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
                MessageBox.Show("Savegame has been backed up to: \n " + @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\Backup\" + "\n With the current date and time as the filename");
            }

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
            GamertagResolve.RunWorkerAsync();
        }

        void BTN_Help_Click(object sender, EventArgs e)
        {
            SaveswapGuide.Show();
        }
    }
}
