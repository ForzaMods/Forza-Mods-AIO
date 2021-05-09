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
        public  static string xauth;
        public void GamebarAttach_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (!sm.OpenProcess("GameBarPresenceWriter"))
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
        public void InitialBGworker_DoWork(object sender, DoWorkEventArgs e)
        {

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
        async Task<string> GetAuthTokenAsync()
        {

            var scan1 = (await sm.AoBScan("41 75 74 68 6F 72 69 7A 61 74 69 6F 6E 58 42 4C 33 2E 30 20 78 3D", true, true)).FirstOrDefault();
            var scan2 = (await sm.AoBScan(scan1+1500, scan1+4000, "43 6F 6E 74 65 6E 74 2D 4C 65 6E 67 74 68 31 31 37", true, true)).FirstOrDefault();
            var length = scan2 - scan1;
            length -= 80;
            var address = (scan1+13).ToString("X"); 
            var result = Encoding.ASCII.GetString(sm.ReadBytes(address, length));
            Console.WriteLine(result);
            return (result);
        }

        public void BTN_SwapSave_Click(object sender, EventArgs e)
        {

            if (Radio_MS.Checked)
            {
                GamebarAttach.RunWorkerAsync();
                //FindMSSave();
                while (!attached)
                {
                    Thread.Sleep(100);
                }
                xauth = GetAuthTokenAsync().ToString();
                Console.WriteLine(xauth);
            }

            else if (Radio_Steam.Checked)
                FindSteamSave();
            else
                MessageBox.Show("Platform not selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        void FindSteamSave()
        {
            MessageBox.Show("Steam Save");
        }

        void FindMSSave()
        {
            MessageBox.Show("ms save");
        }






        //bg worker stuff. query xbox api for gamertag using the xuids

        public void GamertagResolve_DoWork(object sender, DoWorkEventArgs e)
        {
            //MessageBox.Show("MS store save");
            string path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Packages\Microsoft.SunriseBaseGame_8wekyb3d8bbwe\SystemAppData\wgs";
            //System.Diagnostics.Process.Start(path);
            var dirinfo = new DirectoryInfo(path);
            var acclist = dirinfo.EnumerateDirectories("*");
            //var result = (dynamic)JObject.Parse(Get("https://peoplehub.xboxlive.com/users/me/people/xuids(2535411285854551)"));
            //MessageBox.Show(result.people[0].ToString());
            int dircount = 0;
            foreach (var dir in acclist)
            {
                if (dir.Name != "t")
                {
                    dircount++;
                }
            }
            if (dircount==1)
            {
                LST_Accounts.Visible = false;
            }
            else if (dircount <=0)
            {
                //add fallback to local file names and time accessed
            }
            else
            {
                var auth = xauth; Console.WriteLine("1");

                //var auth = "XBL3.0 x=3988553565125907605;eyJlbmMiOiJBMTI4Q0JDK0hTMjU2IiwiYWxnIjoiUlNBLU9BRVAiLCJjdHkiOiJKV1QiLCJ6aXAiOiJERUYiLCJ4NXQiOiIxZlVBejExYmtpWklFaE5KSVZnSDFTdTVzX2cifQ.HxSHXyB0qN4qXgxtCR9F-EAre_F8aIVZoBvJwuUq3aLJ1pZf2ZzsojOK1fjsFzSnWaxQWYBYEFVYipPscqf9-EEb1c505ZYoWFOPAQ52QOMI8dSoMz3HjrwWBvRAf55TEVxlhcFVJF0_3fWB2JHniLF56P1ZXtsNtjkHEWT3neY.PgCJ8Wc5GEfJN8a7bcabRg.QTQAvfEvnWZG_6Oh6iIYQ4Gu7Tmy8APQ9evT1uQBx0Rv_sN9k3ProGuUgJDCB5a5M0tFcrXphxoZ4hl3TA3jo5JESb3UuzeWat5gmaBMeYJgiYRq0A2ItMz0d1uIQrhRBHGL_mT6ygkenLMPAq27HGo57dzWrirXCHlTlI7ciMy4EED6pOZvdi14U191vx86lvMbUtFnJG_DH-Gr_UjBN7ajUORq0KiCMR8jx6V3xtJqLvTpctWkRlH9KxDI1M3DuBdeEbIz2KeFuT9WxfQk8AxpZm7dJGc-GUZ8z1-YHO7DJKLZ0DlD6MAHkL8WyNkqlxSrCJb4pPSPaQKx2iYEm_wEMuiZMauxCSu46MW0nB0d88Sh8D3xmyFYTrIYxyqbsplV-MJINVk7G3c9hCGV5dy2W97zVMfyHjCMlda0XGz5-CZ3yl1pDoOFBCsSYhSJh0yUStVi-ODF7qyaf9oU7zDI5ZJ50lJOYmP6fGWWTNCGU9VKNWrY8xi8ZOD0CZkPWOLCVsD_YjecIYkBEdS5nRejmVlf5lAIjnXZVcQ-oZ8l377nABlPspvRCpP3gtKp8GIE9TH16i740Nv8XeosP3Bu9cGRTdGIQwmtQHFlkdZH5wf8wESYX_UA_VAq0duETpNdEkyCJf7sgioFCSWuSFmw3TEMlTXBzTTNYhDp2X5TlIir_0DG4hLLefqzs1y7e_i1Ed11rQPu9vFnoypIGWeAi5nDWSGGhaRk_95xdldSeTzYTJfsWXq0Mv8eT3SlySNlHN0UhU7xE1p5e4AFRxir1arYt5PasiqTjYqpHcSleATg7Mv7X5XDzRG9x0ZlCf4uQ1zhGx8yCdGGNGtdepZ52REV4d9P0wnvBfIxDSxU5uTqXyEj6kplXSGmbaSAZpOF0Uz4NezGKiJg5wsP4QApP2lOlFCTCo7aZ05zK_7aZ8eTYJxAvCIpou0_q_zpbd6msGOGPtaZO29BmqZWPBVNgZTRiHkELAAd46e-2Q_umoMZmMDE6d0Y-ChmMyZp7IUWdn2b5dBwA8GAfI4wm9WYKsBKsOeQ5LykpZRphSlVPmg1vpzuPWRjznFJAw5HCQ142Omq64HyRthvDMBZ4ahcy_jI3cYWwzga-YGRNC1D1UfqHUV7vfltK8dnzBrHxiBnIVa_FLl277ZSWCAtovt0oi5WEBLZ2KzIjPegvaePcOOjkl39aDPGwO5cryPrDwcT_HiI4jg1OoZ3axNM59oJP9srr25UUyULnziSBnLkLXBagNp_XIpcATfpshYsBc9NKWrs6A5JfJTJP7clTgxt1eBFvSGOmx8FxvS6Ls3fAo5OnZBCuRBftIrVvNxCeJVVFXKkryW4stGC_Lva8ttPW2lL9eO0DfAH30CC-gCJ7FIQKNWDTQ8POwnSZ0roFq6w6ea-K0kafpCCfu6iSXnEDM5zf4dcMN-7U5-_cqzC3bC63WWgedf3r-qsBmkU0YYQ6Thn0GKPzUuJytM8lY6-A4IUuX2Yswlmn9a-ssrFA6MopaWa_buDTDKucfvWVjCmctFBztRyzGq8OUwzCk34yovqCHOdr82G-SrH8fQ6ScilaT6dBrksDMf-GUkCCc0blZRru2l725IXcwdidZmvecF80qGFLH8Sbllyv0dFqt_B7u0j43FicPF_64huVhxPmKGJ6OshJ44lU_OsrGXmPBwEBlyVkfFx7v4IOGPSOb2X16TQkRdvrdBUErfcVRurqx5Es1cYWFyRNj9JwYhqvHAYLMDokRsov0-77vFw0ZOwUwyJ4wc1BjFzzKpg3urSB3C2OGcDkp-7KIvYbu8d7lmDwt2s867Bo2_pf4SBGyvMs0CAfDGBo9mZMpy5.yL7uenTw6z2ovHi4qpKwNR8cPJu7t5bl6IoiMVgYicc";

                foreach (var dir in acclist)
                {
                    if (dir.Name != "t")
                    {
                        var result = (dynamic)JObject.Parse(Get("https://peoplehub.xboxlive.com/users/me/people/xuids(" + Int64.Parse(dir.Name.Substring(0, 16), System.Globalization.NumberStyles.HexNumber) + ")", auth.ToString()));
                        gamertags.Add(result.people[0].gamertag.ToString());
                    }
                }
            }


        }

        public void GamertagResolve_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (var gt in gamertags)
            {
                LST_Accounts.Items.Add(gt.ToString());
            }
        }

        public void Radio_MS_CheckedChanged(object sender, EventArgs e)
        {

        }


    }
}
