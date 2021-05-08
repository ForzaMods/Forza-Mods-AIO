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


namespace Forza_Mods_AIO.TabForms
{
    public partial class Saveswapper : Form
    {
        public Saveswapper()
        {
            InitializeComponent();
        }
        List<string> gamertags = new List<string>();

            string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Headers.Add("x-xbl-contract-version", "1");
            request.Headers.Add("Accept-Encoding", "gzip; q=1.0, deflate; q=0.5, identity; q=0.1");
            request.Headers.Add("x-xbl-contentrestrictions", "eyJ2ZXJzaW9uIjoyLCJkYXRhIjp7Imdlb2dyYXBoaWNSZWdpb24iOiJHQiIsIm1heEFnZVJhdGluZyI6MjU1LCJwcmVmZXJyZWRBZ2VSYXRpbmciOjI1NSwicmVzdHJpY3RQcm9tb3Rpb25hbENvbnRlbnQiOmZhbHNlfX0=");
            request.Headers.Add("Signature", "AAAAAQHXQ4EQ/V7sC+5aQKD9nIHhf3yVjocERoyV0rjBwXygyruSowBOgwWFZSmlquOqLaid2BiHMMizESVEKfN8H6QZXTq2XVlhFg==");
            request.Headers.Add("Cache-Control", "no-store, must-revalidate, no-cache");
            request.Headers.Add("x-xbl-client-name", "XboxApp");
            request.Headers.Add("x-xbl-market", "GB");
            request.Headers.Add("X-XblCorrelationId", "c6c515ec-dcae-47da-b16a-01306ac67028");
            request.Headers.Add("Accept-Language", "en-GB, en");
            request.Headers.Add("Authorization", "XBL3.0 x=9822328712758899513;eyJlbmMiOiJBMTI4Q0JDK0hTMjU2IiwiYWxnIjoiUlNBLU9BRVAiLCJjdHkiOiJKV1QiLCJ6aXAiOiJERUYiLCJ4NXQiOiIxZlVBejExYmtpWklFaE5KSVZnSDFTdTVzX2cifQ.XLt5MrjGvYhqEE2-o8fOw0FBsIpe-nVY9y2jiJaKD9IuhHneZ5AhAMZgDPpjsY2y4MdjI7eBdsJrAWd1ap8tXizfRTff5P6CS3FjIG1IkSotmf25jMSYcRxZv8H2ceHmmooOnS0IQNBCf9uufzuNrYAD_E1IlAT-z4SkDjshoUo.dxH5e9EzOztcP1ovFe7RDQ.Hmpsus-MVNbTC6BOdigeFxMINBM4Fqv3gD5Z-GDZga9XlBxMs-QFxXJDJGyBET_4VM3kj6L21Rff1Zx4VcmKaVQ8Wysa2mhtAcqTDik2ltIaaEwfgo61oGqjh12iVrT5t15EY3vdE3htDhFt-dr96Xa_rubbqe4ZQyZ1ARLHt7KfPNTwHbxQv_6Sdus8x8rcowxvw2euog5nkWrhTPZlBrVeW8-BRMTsx63RdQ28nWsTRv7D-NrQRAJcsP9cPe4RmFdxFDdGPN4hU3QLvAMss8QwSk7Q7Ye_6fxy20IuKCwu4HIE0StUxbtZSTLjs3mlps6blugI2xfzm81JX0-Ny-oW3bDa8OHfMEess-2hL2dXZXjTBh8VgkuYw0fvMbeEun_QlhPgmO8iebYg0y6HHRXPQdWEGwCtW-3zpQX9t7xw4SiCNlxWgUe_KwPlkLzQhquUuFDItSVXfc96zdEgG9B4rSTnDEy5udEvNUW7EjG0bYbvencvR0HEBrp9Z85wyz6xocVsuvR94GLHB9wgdvpxj6XGG88xCqRtFJBKse9xGjQ6deW1KQj3PdxeaOT9EipWtcqah1PiBZ5h8lUDEwrs5VJNDSCFX4x5mmTRCqjU7RxCgNMNTHYbThEZ4EO4Qs4RD5LPfdRNzfUtYzJTEZpXULNqWLPnZEvMKZtkZ51_4M3G08HpXUYqfS9mHElhgEs0ATeyRrDbi9MhreCAvVXNR03ofTn9m_2YJF4wh6Oq2VZlcp-3SKR9brUh3WmFXhaV-5O23OPmjqE3SnC65gtZyNYY-sA57kOhRbQDtNsKyIi9EnUrOmIx2kIpfzxcNvUDjaRX-wlbhibTs1Dv0QkGt-Yk_XVNelaEzejP4cKNciMCLPumkz7C6bDj-S1GJAVf0R0sRFmIr-qU6G6H5gbMHNy5Z9pTKRERm6PdoylYWjJW_CG2-YqhYcFeaVQ853YW43kcQOEHoUqgQ6ZImU7tLQoNChy4Mx89slS4KzRe5zpx0d9miDj7FTR8gp7gNXmo232WFFQ9VKFRqenw3URv0l2rw4KiWcDEEbcFdc_N3uD5aavT_Q3crFUkEy8dmDj4L_l3KayRr5hU00PTEjEQjfhHz-WdGac7Zm2FFoSRZU_u3ILf0HSmQUNXFchrpm8HLTGfyAkoCthtnj46VSFRSy7eId0MgVMf0-TukcB9SvD33vh7j89QOSVkwslORPfZOI5VUHKMtFnggXEyOK-8yMUbVxRZZthgzhBW7EeIQcMGuihB-e0O2DsaTWJxg0Vr0p7ezo67UXppcDT9Yz7fElQgkrsVDdi9N8c7xPaQA4SyEh9Q_36JvSoO05j1IWiTsTo73Ruk3Cc-ldHIfhk4Isft0AmXtWPYmlWHQiMePdcUItxCNaXb9j9EPrK_-QubnQHWiqoX-dkAMXe4bOb4guEQWddRjhYDOhaP_Riv496qVxgl57OTsyZk0ZqoU-9ixzNrA0Y-dNv1bVq3a8C-LGAGuBC_gWnPULYqmEKu7eCywWw0-STlXhQSVg2-Q53IaoiAKErj5azODIqld3ZUAgCJfPOUOXZdZp0sEPC4nzdw_c6ufpW2oxMcX9eiJqY5mdTDNU5Z1ja88PUVHXYP1-5YLWHPAMcQTRdvX8PnDnnhBq2qorUDvjhwbY-He3_Goy1LWBXCrW47O-0PTqLJqRKqYaFsLVncvBiqQROZMFuG_1Bi6OeeLuBQ9vjuhFt1nhFkNz_6ZoLirGWB4qFTuYwxm--T-XQfRFcVzWyxavXGeRbp50gE3KAaIl_9u_S6dzsZHIpNxKyqTU5DPZGp-MeLrj5wfKRtbA8O_IHaUJ6rskqy5ZxFV7safAXZOFlNVRtpY0zG_0nY0f3e4oZ8oa1e1ZjctH8RpVfaKK6U554k6aXnYcfsH2zuIxXWCePjIHJxs6dKhlWoxalAjA.owfY1zPvS9eCVoVkyzXAKRxZzZz2uQ_rO5DFPEMRmSY");

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {

                return reader.ReadToEnd();
            }
        }
        void FindMSSave()
        {
            //MessageBox.Show("MS store save");
            string path = @"C:\Users\"+ Environment.UserName + @"\AppData\Local\Packages\Microsoft.SunriseBaseGame_8wekyb3d8bbwe\SystemAppData\wgs";
            //System.Diagnostics.Process.Start(path);
            var dirinfo = new DirectoryInfo(path);
            var acclist = dirinfo.EnumerateDirectories("*");
            //var result = (dynamic)JObject.Parse(Get("https://peoplehub.xboxlive.com/users/me/people/xuids(2535411285854551)"));
            //MessageBox.Show(result.people[0].ToString());


            foreach (var dir in acclist)
            {
                if (dir.Name != "t")
                {
                    var result = (dynamic)JObject.Parse(Get("https://peoplehub.xboxlive.com/users/me/people/xuids("+ Int64.Parse(dir.Name.Substring(0, 16), System.Globalization.NumberStyles.HexNumber) +")"));
                    gamertags.Add(result.people[0].gamertag.ToString());
                }
                

            }
                
        }
        void FindSteamSave()
        {
            MessageBox.Show("Steam Save");
        }
        private void BTN_SwapSave_Click(object sender, EventArgs e)
        {
            if (Radio_MS.Checked)
                FindMSSave();
            else if (Radio_Steam.Checked)
                FindSteamSave();
            else
                MessageBox.Show("Error", "Platform not selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           


        }
    }
}
