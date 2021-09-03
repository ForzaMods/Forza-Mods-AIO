using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forza_Mods_AIO.TabForms.PopupForms
{
    public partial class Error : Form
    {
        Exception Data;
        public Error(ThreadExceptionEventArgs data)
        {
            Data = data.Exception;
            InitializeComponent();
        }

        public Error(UnhandledExceptionEventArgs data)
        {
            Data = data.ExceptionObject as Exception;
            InitializeComponent();
        }

        private void Error_Load(object sender, EventArgs e)
        {
            Left = MainWindow.main.Left + ((MainWindow.main.Width - Width) / 2);
            Top = MainWindow.main.Top + ((MainWindow.main.Height - Height) / 2);
        }

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void TopPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        private void TopPanel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(System.Windows.Forms.Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        private void TopPanel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dragging = false;
        }
        static void SendWebhook(string title, string message, string colour)
        {
            WebRequest wr = (HttpWebRequest)WebRequest.Create("https://canary.discord.com/api/webhooks/883356912945606726/SmWV5NZR83e4cfDzBawsugb6Ta0x5MUgi0yncZSj-H3fMHUz91h9bI8Kje1UyZrzDmEz");
            wr.ContentType = "application/json";
            wr.Method = "POST";
            using (var sw = new StreamWriter(wr.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new
                {
                    content = "Ver: " + Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                    username = "Pog bug reporter",
                    embeds = new[]
                    {
                        new
                        {
                            title = title,
                            description = message,
                            color = colour
                        }
                    }
                });
                sw.Write(json);
            }
            var response = (HttpWebResponse)wr.GetResponse();
        }

        private void Continue_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            MainWindow.main.BTN_Close_Click(sender, e);
        }

        private void Devs_Click(object sender, EventArgs e)
        {
            SendWebhook(Data.Message, Data.StackTrace, "9502975");
            this.Hide();
        }
    }
}
