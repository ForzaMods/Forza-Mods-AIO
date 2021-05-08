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
            request.Headers.Add("Authorization", "XBL3.0 x=12686054406395589293;eyJlbmMiOiJBMTI4Q0JDK0hTMjU2IiwiYWxnIjoiUlNBLU9BRVAiLCJjdHkiOiJKV1QiLCJ6aXAiOiJERUYiLCJ4NXQiOiIxZlVBejExYmtpWklFaE5KSVZnSDFTdTVzX2cifQ.ewrISRuItJduj2z6RGdZ8SPpmv21qmQDCPWy-GVIrysDIalAt5fuEqntmhDeB4bXc0R1CpOAg9w4GAtGuWUs11V3g-izEmUN0cg3DpR7ogJ9nSZ_bhxSMDij1kFxUON5AbTkNg7P6ui4fIr38HClv2CmZnbKX_xR5rEUbrdLiCQ.wxo76Mi_TAZ10zdvv-uHvQ.WQi2wRfJyS-9HE3HEVoYvHVY1M9sFu6CsISEe074jTz2XanLFWvBJXQNGoipvfscS8D0Y-UR_UGXLs1o5BFj93KJ51ViYyzkf9cVPIaYkeHEDkvRtsZJac8dYJ4f0Rj7XmX2BljEb5FTgK4Zp3aUdLmkqJ_HVm1nF-iT6xt9xuARBhF6Q4N148jK03JMbpwcK5WDIhHml6EXZWEm276a07enS28A1HJJFmaDLHuECOz6uig4fuWf4CkD7QuorlQkov0x88WizwSaUnls3yLXU1X4xpmUoPPX1bE7AkoyTx3i1kZ6pG5_GRd_FaIc3vGRB5TpxJ1fznSKnFDkQ6JdJ8S2P3VC3sZYgte4n3IOUIUd4V5xhr-eJnCRkrqB-PR3_FqoR-6-ZtYNvkG3YmD-xQfhGKliRW6kI6fWASxaHAWQOBE4rQjD8YZjAAZaZkio-r4MYyXVG-lracyAgCgkk8JKv2S2hZh0Hut0zgRGHF2-uCiXF2ERtbbpAG8jpbhVnTlgpa0zxm2eySIMCBMXaB3ewH78suA4M5ihw-Tsc6vfEJTj638rFlCdsDcm9tODz51XK0_nbRUPqeZ_QcoTmUlb6JwBygXN9lqV2zeVeAOKhd7qEKoiEVo9w7qMUu_ALHsxlyAGvh3JfL3qAw4r36ooksx9IKGV5LsHISibNZ3QPDDGxIk_S64KLu3J2dE7vbB60qsBF_ARczntgxkJXPoMRnUCTyFUNKD9eUXEfQokSx2TLPLAqpDn3U0826qUIrhziuwDzbbFroVZq6Owk79oPTqJueASy3tgGwps0YU3KtO7KOLlPhbJIYT-aPAaz9t0SQMqwBiCY6U5Ov3Fsdx1yruq7chJhSmgLw_pwSJxX0oh3V9lJ3fkLE0bCNf1myNejLh0BcjlcUMFFshKC8JIN33nu9MeCHUqcjeT3W2zJBOsQUegoTc4lIBuZ0hF2dJ9_h7llepjauS-od7_l8l-6CHOqy22BgcVtmoAG08-FF0HlJoBMI1bjNAkjeIuHi9Bp7qC0d9iWY-dnR7VdE9p_Xj9WrqUoArC07Idy5S_jgm_6qD3ba8y4bE8NeA-PWZbfr7qPPmErWQCkHqKqagg9tgvjPfShqZ_WnHL_-5_5PGHxzvdpAndIFRjdoFUpRStYP6JwFphULby57X_Ey7GUfliDtQtq14GQ0yw721eIqPyxPeye_4hmGEFVKB0rPwrmrMH_ZFiX56WMhkFaTak05NkD-TTuQNlCyUitTcIdSE-3VuBqPhGEg6pPY_ENyDjxuBS1a6M0oJb3xYph0yP6ErvLiIMMue0fpBhipD7NCtR6KB1kbIP871nZj0CrK-LSYXlU-5FiJLmibKG1z0ZMjqFnLmemRQP5ZzAEVt_sLwzOFp0FdFU3GAUkDW4P6zV2dv1JCzElZPVpS-9bnIK1dDgWTFCiqUE3DYfB7Z6CrQ6OBc9fJhmIjBImiB8r1IWPBVrcDgY7fB8WfTd_trRgTy16uvRp8bsqi_-yQm01Vasat022nyLinKKcG8zHCcYjFnAXZKFXjVv6HCkvGe85hiHZHQ1WLOIe0IhANMyDrsJ7wd3STd_P1Ndo7VXWCInZaKS1HagtzYr8TI1HOVZMNhvxhGkrKbEDBT6dCtYYSEgDqr-0sUVb9iNYhrtsNBXSj3ou7i6ix_wz0Pv0kCX1qZa4d54aT_LrOXDOrZ-KOaEojbmORW5vaL4rMl10MDGOXgPyVNLO3raZgfG6stWPXDZLfCUC2JP8-Ef1kBkELTn5wuEs7P9kCqDXkkBdJLr3j2ICXBfMab0ZtQmZ5m814ohWJCJ3ymniKAOOlFE1n985Trrhq7v46irkQ9l3kH7bH_4vOZhdPHM2WHdoiPp4VrczHPK2poEmZwTIgLUZSxDMKPxmDCEUAneccec0KDOLfpMuLHFv2eJ6EdkYew-9_ya0f7kjxREiLi5ZHM.Z8A00fvFzm0hBwkiefy4dCGvJcKTbYwhkyOJvFp3aGc");

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
            //
            //MessageBox.Show( ;
            foreach (var dir in acclist)
            {
                if (dir.Name != "t")
                {
                    var result = (dynamic)JObject.Parse(Get("https://peoplehub.xboxlive.com/users/me/people/xuids("+ Int64.Parse(dir.Name.Substring(0, 16), System.Globalization.NumberStyles.HexNumber) +")"));
                    LST_Accounts.Items.Add(result.people[0].gamertag.ToString());
                }
                
                    //LST_Accounts.Items.Add("last played: " + dir.LastWriteTime);

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
