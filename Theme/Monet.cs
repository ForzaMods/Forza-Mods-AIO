using ColorThiefDotNet;
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Media;
using WPF_Mockup.Tabs.AIO_Info;

namespace WPF_Mockup.CustomTheming
{
    internal class Monet
    {
        #region Variables
        public static Brush MainColour = new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF111111"));
        public static Brush DarkishColour = new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF101010"));
        public static Brush DarkColour = new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF090909"));
        public static Brush DarkerColour = new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF080808"));
        #endregion
        #region DLL imports
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);
        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr handle, ref System.Drawing.Rectangle rect);
        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr GetShellWindow();
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);        

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        #endregion
        #region Functions
        public static System.Drawing.Bitmap CaptureWindow(IntPtr handle)
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle();
            GetWindowRect(handle, ref rect);

            rect.Width = rect.Width - rect.X;
            rect.Height = rect.Height - rect.Y;

            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(rect.Width, rect.Height);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap))
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
            IntPtr Window = IntPtr.Zero;
            var Windows = EnumWindows(new EnumWindowsProc((hwnd, lParam) =>
            {
                IntPtr ParentHandle = hwnd;
                EnumChildWindows(hwnd, new EnumWindowsProc((hwnd, lParam) =>
                {
                    var sb = new StringBuilder(GetWindowTextLength(hwnd) + 1);
                    GetWindowText(hwnd, sb, sb.Capacity);
                    if (sb.ToString().Contains("WPE"))
                    {
                        Window = ParentHandle;
                        return true;
                    }
                    return true;
                }), (IntPtr)0);
                return true;
            }), (IntPtr)0);
            
            var colorThief = new ColorThief();
            if (Window == IntPtr.Zero)
                Window = GetShellWindow();

            System.Drawing.Bitmap DesktopWallpaper = CaptureWindow(Window);
            QuantizedColor Colour = colorThief.GetColor(DesktopWallpaper);
            DesktopWallpaper.Dispose();
            ColorThiefDotNet.Color Colour2 = Colour.Color;

            double H; double S; double V1; double V2; double V3; double V4;

            MainWindow.mw.Dispatcher.BeginInvoke((Action)delegate ()
            {
                ColorToHSV(System.Drawing.ColorTranslator.FromHtml(Colour2.ToHexString()), out H, out S, out V1);
                V2 = V1;
                V3 = V1;
                V4 = V1;
                V1 *= AIO_Info.ai.LightnessSlider.Value / AIO_Info.ai.LightnessSlider.Maximum;
                if (V1 < 0) V1 = 0;
                V2 *= (AIO_Info.ai.LightnessSlider.Value / 1.25) / AIO_Info.ai.LightnessSlider.Maximum;
                if (V2 < 0) V2 = 0;
                V3 *= (AIO_Info.ai.LightnessSlider.Value / 1.5) / AIO_Info.ai.LightnessSlider.Maximum;
                if (V3 < 0) V3 = 0;
                V4 *= (AIO_Info.ai.LightnessSlider.Value / 2) / AIO_Info.ai.LightnessSlider.Maximum;
                if (V4 < 0) V4 = 0;

                System.Drawing.Color FinalColour1 = ColorFromHSV(H, S, V1);
                System.Drawing.Color FinalColour2 = ColorFromHSV(H, S, V2);
                System.Drawing.Color FinalColour3 = ColorFromHSV(H, S, V3);
                System.Drawing.Color FinalColour4 = ColorFromHSV(H, S, V4);
                string ColourHex1 = System.Drawing.ColorTranslator.ToHtml(FinalColour1);
                string ColourHex2 = System.Drawing.ColorTranslator.ToHtml(FinalColour2);
                string ColourHex3 = System.Drawing.ColorTranslator.ToHtml(FinalColour3);
                string ColourHex4 = System.Drawing.ColorTranslator.ToHtml(FinalColour4);

                var converter = new BrushConverter();
                MainColour = (Brush)converter.ConvertFromString(ColourHex1);
                DarkishColour = (Brush)converter.ConvertFromString(ColourHex2);
                DarkColour = (Brush)converter.ConvertFromString(ColourHex3);
                DarkerColour = (Brush)converter.ConvertFromString(ColourHex4);

                if (MainWindow.mw.Background.Background.ToString() == MainColour.ToString())
                    return;

                MainWindow.mw.Background.Background = MainColour;
                MainWindow.mw.FrameBorder.Background = MainColour;
                MainWindow.mw.SideBar.Background = DarkishColour;
                MainWindow.mw.TopBar1.Background = DarkColour;
                MainWindow.mw.TopBar2.Background = DarkColour;

                string RandName = Guid.NewGuid().ToString();
                ThemeManager.Current.ClearThemes();
                ThemeManager.Current.AddTheme(new Theme(RandName, RandName, "Dark", "Red", (System.Windows.Media.Color)ColorConverter.ConvertFromString(ColourHex4), DarkerColour, true, false));
                ThemeManager.Current.ChangeTheme(Application.Current, RandName);

                MainWindow.mw.CategoryButton_Click(new Object(), new RoutedEventArgs());
                foreach (FrameworkElement Element in MainWindow.mw.Window.GetChildren(true))
                {
                    if (Element.GetType() == typeof(System.Windows.Controls.Button))
                    {
                        Element.GetType().GetProperty("Background").SetValue(Element, DarkerColour);
                        Element.GetType().GetProperty("BorderBrush").SetValue(Element, DarkerColour);
                    }
                    if (Element.GetType() == typeof(System.Windows.Controls.Border))
                    {
                        Element.GetType().GetProperty("BorderBrush").SetValue(Element, DarkerColour);
                    }
                    if (Element.GetType() == typeof(NumericUpDown))
                    {
                        Element.GetType().GetProperty("Background").SetValue(Element, DarkerColour);
                        Element.GetType().GetProperty("BorderBrush").SetValue(Element, DarkerColour);
                    }
                }
            });
        }
        #endregion
    }
}
