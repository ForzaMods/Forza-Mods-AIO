using ColorThiefDotNet;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;

namespace WPF_Mockup.CustomTheming
{
    internal class Monet
    {
        #region Variables
        public static System.Windows.Media.Brush MainColour = null;
        public static System.Windows.Media.Brush DarkerColour = null;
        #endregion
        #region DLL imports
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr handle, ref System.Drawing.Rectangle rect);
        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr GetShellWindow();
        #endregion
        #region Functions
        public static Bitmap CaptureWindow(IntPtr handle)
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle();
            GetWindowRect(handle, ref rect);

            rect.Width = rect.Width - rect.X;
            rect.Height = rect.Height - rect.Y;

            Bitmap bitmap = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                IntPtr hdc = g.GetHdc();
                if (!PrintWindow(handle, hdc, 0))
                {
                    int error = Marshal.GetLastWin32Error();
                    var exception = new System.ComponentModel.Win32Exception(error);
                }
                g.ReleaseHdc(hdc);
            }
            return bitmap;
        }
        private static System.Drawing.Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return System.Drawing.Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return System.Drawing.Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return System.Drawing.Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return System.Drawing.Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return System.Drawing.Color.FromArgb(255, t, p, v);
            else
                return System.Drawing.Color.FromArgb(255, v, p, q);
        }
        private static void ColorToHSV(System.Drawing.Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }
        public static void ApplyMonet()
        {
            var colorThief = new ColorThief();
            Bitmap DesktopWallpaper = CaptureWindow(GetShellWindow());
            DesktopWallpaper.Save("test.bmp");

            QuantizedColor Colour = colorThief.GetColor(DesktopWallpaper);
            ColorThiefDotNet.Color Colour2 = Colour.Color;

            double H; double S; double V1; double V2;

            ColorToHSV(ColorTranslator.FromHtml(Colour2.ToHexString()), out H, out S, out V1);
            V2 = V1;
            V1 *= MainWindow.mw.LightnessSlider.Value / MainWindow.mw.LightnessSlider.Maximum;
            if (V1 < 0) V1 = 0;
            V2 *= (MainWindow.mw.LightnessSlider.Value / 2) / MainWindow.mw.LightnessSlider.Maximum;
            if (V2 < 0) V2 = 0;

            System.Drawing.Color FinalColour1 = ColorFromHSV(H, S, V1);
            System.Drawing.Color FinalColour2 = ColorFromHSV(H, S, V2);
            string ColourHex1 = ColorTranslator.ToHtml(FinalColour1);
            string ColourHex2 = ColorTranslator.ToHtml(FinalColour2);

            var converter = new BrushConverter();
            MainColour = (System.Windows.Media.Brush)converter.ConvertFromString(ColourHex1);
            DarkerColour = (System.Windows.Media.Brush)converter.ConvertFromString(ColourHex2);

            MainWindow.mw.Background.Background = MainColour;
            MainWindow.mw.TopBar1.Background = DarkerColour;
            MainWindow.mw.TopBar2.Background = DarkerColour;

            foreach (FrameworkElement Element in MainWindow.mw.Window.GetChildren(true))
            {
                if (Element.GetType() == typeof(System.Windows.Controls.Button))
                    Element.GetType().GetProperty("Background").SetValue(Element, DarkerColour);
                if (Element.GetType() == typeof(System.Windows.Controls.Slider))
                    Element.GetType().GetProperty("Foreground").SetValue(Element, DarkerColour);
            }
        }
        #endregion
    }
}
