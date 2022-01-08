using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forza_Mods_AIO.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Forza_Mods_AIO.TabForms.PopupForms
{
    public partial class Map : Form
    {
        public int XCoord;
        public int ZCoord;
        public Color Colour = new Color();
        public float[,] CoordArray = new float[21285, 3];
        private static CultureInfo resourceCulture;
        internal static byte[] b6aQzDZP7w
        {
            get
            {
                return (byte[])Resources.ResourceManager.GetObject("b6aQzDZP7w", resourceCulture);
            }
        }

        public static Map map;
        public Map()
        {
            InitializeComponent();
            map = this;
            MapBox.Image = Properties.Resources.RectangleMap;
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
        private void BTN_Close_Click(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            UpdateMapWorker.CancelAsync();
            this.Hide();
        }

        public void drawEllipse(PictureBox pb, int x, int y)
        {
            //refresh the picture box
            pb.Refresh();
            //create a graphics object
            Bitmap Image = Properties.Resources.RectangleMap;
            Graphics g = Graphics.FromImage(Image);
            //create a pen object;
            Pen p = new Pen(Colour, 5);
            //draw Ellipse
            g.DrawEllipse(p, x, y, 5, 5);
            pb.Image = Image;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            //dispose pen and graphics object
            p.Dispose();
            g.Dispose();
        }

        private void UpdateMapWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                XCoord = (int)Math.Round(MainWindow.m.ReadFloat(Speedhack.xAddr));
                ZCoord = (int)Math.Round(MainWindow.m.ReadFloat(Speedhack.zAddr));
                CoordsLabel.Text = "X: " + XCoord + "\nZ: " + ZCoord;
                drawEllipse(MapBox, (int)Math.Round(XCoord / 11.2) + 1195, - (int)Math.Round(ZCoord / 11.2) + 615);
                if (UpdateMapWorker.CancellationPending)
                    break;
                Thread.Sleep(250);
            }
        }

        class Coords
        {
            float x { get; set; }
            float y { get; set; }
            float z { get; set; }
        }

        private void Map_Load(object sender, EventArgs e)
        {
            if(!UpdateMapWorker.IsBusy)
                UpdateMapWorker.RunWorkerAsync();
            File.WriteAllBytes(Path.Combine(Path.GetTempPath(), "b6aQzDZP7w.json"), b6aQzDZP7w);
            string Filename = Path.Combine(Path.GetTempPath(), "b6aQzDZP7w.json");
            string JsonText = File.ReadAllText(Filename).Replace("\r\n", string.Empty);
            JArray Coords = JArray.Parse(JsonText);
            int index = 0;
            foreach (JObject Coord in Coords)
            {
                CoordArray[index, 0] = Coord["x"].Value<float>();
                CoordArray[index, 1] = Coord["y"].Value<float>();
                CoordArray[index, 2] = Coord["z"].Value<float>();
                index++;
            }
            File.Delete(Path.Combine(Path.GetTempPath(), Filename));
        }

        private void Map_Activated(object sender, EventArgs e)
        {
            if (!UpdateMapWorker.IsBusy)
                UpdateMapWorker.RunWorkerAsync();
        }

        void PlaneCoefficients(float[] PointA, float[] PointB, float[] PointC, out float A, out float B, out float C, out float D)
        {
            float a1 = PointB[0] - PointA[0];
            float b1 = PointB[1] - PointA[1];
            float c1 = PointB[2] - PointA[2];
            float a2 = PointC[0] - PointA[0];
            float b2 = PointC[1] - PointA[1];
            float c2 = PointC[2] - PointA[2];
            A = b1 * c2 - b2 * c1;
            B = a2 * c1 - a1 * c2;
            C = a1 * b2 - b1 * a2;
            D = (-A * PointA[0] - B * PointA[1] - C * PointA[2]);
        }

        private void MapBox_MouseClick(object sender, MouseEventArgs e)
        {
            MouseCoordsLabel.Text = "Mouse X: " + e.X.ToString() + "\nMouse Z: " + e.Y.ToString();
            float X = (float)(((e.X * 2) - 1195) * 11.2);
            float Z = (float)(((e.Y * 2) - 615) * -11.2);
            List<float> Distances = new List<float>();
            for (int i = 0; i < 21284; i++)
            {
                Distances.Add((float)Math.Sqrt((float)Math.Pow(X - CoordArray[i, 0], 2)) + (float)Math.Pow(Z - CoordArray[i, 2], 2));
            }

            /*
            float minDist = Distances.Min();
            int Index = Distances.IndexOf(minDist);
            float Y = CoordArray[Index, 1];*/

            List<float> OriginalDistance = new List<float>(Distances);
            Distances.Sort();

            int a = OriginalDistance.IndexOf(Distances[0]);
            int b = OriginalDistance.IndexOf(Distances[1]);
            int c = OriginalDistance.IndexOf(Distances[2]);
            float[] PointA = new float[3];
            PointA[0] = CoordArray[a, 0];
            PointA[1] = CoordArray[a, 1];
            PointA[2] = CoordArray[a, 2];
            float[] PointB = new float[3];
            PointB[0] = CoordArray[b, 0];
            PointB[1] = CoordArray[b, 1];
            PointB[2] = CoordArray[b, 2];
            float[] PointC = new float[3];
            PointC[0] = CoordArray[c, 0];
            PointC[1] = CoordArray[c, 1];
            PointC[2] = CoordArray[c, 2];

            float EQa = (PointA[1] * (PointB[2] - PointC[2])) + (PointB[1] * (PointC[2] - PointA[2])) + (PointC[1] * (PointA[2] - PointB[2]));
            float EQb = (PointA[2] * (PointB[0] - PointC[0])) + (PointB[0] * (PointC[0] - PointA[0])) + (PointC[2] * (PointA[0] - PointB[0]));
            float EQc = (PointA[0] * (PointB[1] - PointC[1])) + (PointB[0] * (PointC[1] - PointA[1])) + (PointC[0] * (PointA[1] - PointB[1]));
            float EQd = -((PointA[0] * ((PointB[1] * PointC[2]) - (PointC[1] * PointB[2]))) + (PointB[0] * ((PointC[1] * PointA[2]) - (PointA[1] * PointC[2]))) + (PointC[0] * ((PointA[1] * PointB[2]) - (PointB[1] * PointA[2]))));

            PlaneCoefficients(PointA, PointB, PointC, out float A, out float B, out float C, out float D);

            float Y = (-(A * X) - (C * Z) - D)/B;

            MainWindow.m.WriteMemory(Speedhack.xAddr, "float", (X).ToString());
            MainWindow.m.WriteMemory(Speedhack.yAddr, "float", (Y + 4).ToString());
            MainWindow.m.WriteMemory(Speedhack.zAddr, "float", (Z).ToString());
            //MainWindow.m.WriteMemory(Speedhack.yVelocityAddr, "float", "-100");
        }
    }
}
