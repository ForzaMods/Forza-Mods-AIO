using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Size = System.Windows.Size;
using Timer = System.Windows.Forms.Timer;

namespace Forza_Mods_AIO.Overlay
{
    public class OverlayHandling
    {
        #region DLLImports
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static Keys Up = Keys.NumPad8;
        public static Keys Down = Keys.NumPad2;
        public static Keys Left = Keys.NumPad4;
        public static Keys Right = Keys.NumPad6;
        public static Keys Confirm = Keys.NumPad5;
        public static Keys Leave = Keys.NumPad0;
        public static Keys OverlayVisibility = Keys.Subtract;
        #endregion
        #region Blur DLLImports
        //Credits to Rafael Rivera for the blur https://github.com/riverar/sample-win32-acrylicblur
        internal enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
            ACCENT_INVALID_STATE = 5
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public uint AccentFlags;
            public uint GradientColor;
            public uint AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        internal enum WindowCompositionAttribute
        {
            // ...
            WCA_ACCENT_POLICY = 19
            // ...
        }

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        #endregion
        #region Variables
        // Menu operational vars
        public string[] MenuHeaders;
        int MainAOBScanProg = 0;
        int SelectedOptionIndex = 0;
        int LevelIndex = 0;
        public string CurrentMenu = "MainOptions";
        bool Hidden = false;
        Dictionary<int, string> History = new Dictionary<int, string>()
        {
            {  0 ,"MainOptions" }
        };

        // Key vars
        bool UpKeyDown = false;
        bool DownKeyDown = false;
        bool LeftKeyDown = false;
        bool RightKeyDown = false;

        //Theme vars
        public Brush MainBackColour = Brushes.Transparent;
        public Brush DescriptionBackColour = Brushes.Transparent;
        public Brush MainBorderColour = Brushes.Black;
        public Brush DescriptionBorderColour = Brushes.Black;
        public int HeaderIndex = 0;
        public List<object[]> Headers = new();
        public BitmapImage HeaderImage;

        // Opacity vars (not used with fast blur mode)
        private uint _blurBackgroundColor = 0x990000;
        private uint _blurOpacity = 0x4B;
        #endregion
        // Caches all the headers
        public void CacheHeaders()
        {
            MenuHeaders = Directory.GetFiles(Environment.CurrentDirectory + @"\Overlay\Headers");
            foreach (string header in MenuHeaders)
            {
                bool InCachedBitmaps = false;
                foreach (object[] item in Headers)
                {
                    if (item[0].ToString().Contains(header.Split('\\').Last().Split('.').First()))
                    {
                        InCachedBitmaps = true;
                        break;
                    }
                }
                if (!InCachedBitmaps)
                {
                    Headers.Add(new object[] { header.Split('\\').Last().Split('.').First(), new BitmapImage(new Uri(header)) });
                }
            }
        }
        // Handles the position of the overlay
        public void OverlayPosAndScale(CancellationToken ct)
        {
            CacheHeaders();
            while (true)
            {
                Thread.Sleep(5);
                if (ct.IsCancellationRequested)
                    return;

                RECT ForzaWindow = new RECT();
                RECT ForzaClientWindow = new RECT();

                GetWindowRect(MainWindow.mw.gvp.Process.MainWindowHandle, ref ForzaWindow);
                GetClientRect(MainWindow.mw.gvp.Process.MainWindowHandle, out ForzaClientWindow);

                double Offset = ForzaClientWindow.Bottom / 20;

                // Forza window x and y coords
                double PosTop = ForzaWindow.Top + ((ForzaWindow.Bottom - ForzaWindow.Top - ForzaClientWindow.Bottom) / 1.3) + Offset;
                double PosLeft = ForzaWindow.Left + ((ForzaWindow.Right - ForzaWindow.Left - ForzaClientWindow.Right) / 2) + Offset;

                // Forzas current resolution (doesnt account for scaling)
                double yRes = ForzaClientWindow.Bottom - ((ForzaWindow.Bottom - ForzaWindow.Top - ForzaClientWindow.Bottom) / 1.3);

                // Calculate the right numbers for the menu to scale to resolution
                double HeaderY = yRes / 10.8;
                double HeaderX = HeaderY * 4;

                // Select header
                if (HeaderImage == null || HeaderImage.UriSource.LocalPath != MenuHeaders[HeaderIndex])
                {
                    if (HeaderImage != null && HeaderImage.IsFrozen)
                        HeaderImage = HeaderImage.Clone();
                    HeaderImage = (BitmapImage)Headers.Find(x => x[0].ToString().Contains(MenuHeaders[HeaderIndex].Split('\\').Last().Split('.').First()))[1];
                    try { HeaderImage.Freeze(); } catch { HeaderImage.Dispatcher.Invoke(() => { HeaderImage.Freeze(); }); }
                }

                if (MainWindow.mw.gvp.Process.MainWindowHandle == GetForegroundWindow())
                {
                    Overlay.o.Dispatcher.Invoke(delegate ()
                    {
                        // Set position
                        Overlay.o.Top = PosTop;
                        Overlay.o.Left = PosLeft;

                        // Set width of menu and set header size (scale with resolution)
                        Overlay.o.Width = HeaderX;
                        Overlay.o.TopSection.Height = new GridLength(HeaderY);

                        Overlay.o.Header.Width = Overlay.o.Width;
                        Overlay.o.Header.Height = Overlay.o.TopSection.ActualHeight;

                        // Set height of menu depending on items present
                        if (Overlay.o.OptionsBlock.Inlines.Count == (Overlay.o.AllMenus[CurrentMenu].Count * 2) - 1)
                        {
                            Overlay.o.MainSection.Height = new GridLength(5 + Overlay.o.OptionsBlock.ActualHeight + 5);
                            if (Overlay.o.DescriptionBlock.Text != string.Empty)
                                Overlay.o.DescriptionSection.Height = new GridLength(5 + 5 + Overlay.o.DescriptionBlock.ActualHeight + 5);
                            else
                                Overlay.o.DescriptionSection.Height = new GridLength(0);

                            Overlay.o.Height = Overlay.o.TopSection.ActualHeight + Overlay.o.MainSection.ActualHeight + Overlay.o.DescriptionSection.ActualHeight;
                        }

                        // Set menu header image
                        Overlay.o.Header.Source = HeaderImage;


                        // Set colours of menu
                        Overlay.o.MainBorder.Background = MainBackColour;
                        Overlay.o.MainBorder.BorderBrush = MainBorderColour;

                        Overlay.o.DescriptionBorder.Background = DescriptionBackColour;
                        Overlay.o.DescriptionBorder.BorderBrush = DescriptionBorderColour;

                        if (Overlay.o.Visibility == Visibility.Hidden && !Hidden)
                            Overlay.o.Show();
                    });
                }
                else { Overlay.o.Dispatcher.Invoke(delegate () { Overlay.o.Hide(); }); }
            }
        }

        // Updates the menu, eg selected option, values etc
        public void UpdateMenuOptions(CancellationToken ct)
        {
            string LastMenu = string.Empty;
            int LastSelectedOptionIndex = -1;
            object LastValue = null;
            Size MenuSize = new Size();
            while (true)
            {
                if (ct.IsCancellationRequested)
                    return;

                Thread.Sleep(10);

                // Clears the menu
                Overlay.o.Dispatcher.BeginInvoke((Action)delegate () { Overlay.o.OptionsBlock.Inlines.Clear(); Overlay.o.ValueBlock.Inlines.Clear(); });
                int index = 0;

                // Gets y resolution of the forza client window
                RECT ForzaWindow = new RECT();
                RECT ForzaClientWindow = new RECT();

                GetWindowRect(MainWindow.mw.gvp.Process.MainWindowHandle, ref ForzaWindow);
                GetClientRect(MainWindow.mw.gvp.Process.MainWindowHandle, out ForzaClientWindow);

                double yRes = ForzaClientWindow.Bottom - ((ForzaWindow.Bottom - ForzaWindow.Top - ForzaClientWindow.Bottom) / 1.3);

                // Selected option background
                Overlay.o.Dispatcher.Invoke((Action)delegate ()
                {
                    if (Overlay.o.OptionsBlock.Inlines.Count > 1)
                    {
                        // Remove previous highlight box
                        foreach (UIElement Child in Overlay.o.Layout.Children)
                        {
                            if (Child.GetType().GetProperty("Name").GetValue(Child) == "Highlight")
                            {
                                Overlay.o.Layout.Children.Remove(Child);
                                break;
                            }
                        }
                        // Create new highlight box
                        float height = (float)(((Overlay.o.OptionsBlock.ActualHeight / Overlay.o.AllMenus[CurrentMenu].Count) * SelectedOptionIndex) + 5);
                        Border Highlighted = new Border()
                        {
                            Name = "Highlight",
                            VerticalAlignment = VerticalAlignment.Top,
                            Background = Brushes.Black,
                            Width = Overlay.o.Layout.ActualWidth,
                            Height = Overlay.o.OptionsBlock.ActualHeight / Overlay.o.AllMenus[CurrentMenu].Count,
                            Margin = new Thickness(0, height, 0, 0)
                        };
                        Grid.SetColumn(Highlighted, 0);
                        Grid.SetRow(Highlighted, 1);

                        // Put highlight box behind text, add to layout
                        System.Windows.Controls.Panel.SetZIndex(Highlighted, 1);
                        Overlay.o.Layout.Children.Add(Highlighted);
                    }
                });

                // Adds all menu options to the menu
                foreach (Overlay.MenuOption item in Overlay.o.AllMenus[CurrentMenu])
                {
                    string Text = string.Empty;
                    string Value = string.Empty;
                    string Description = string.Empty;
                    SolidColorBrush FColour = Brushes.White;
                    if (index == SelectedOptionIndex)
                    {
                        Text = $"[{item.Name}]";
                        FColour = Brushes.Green;
                        Description = item.Description;
                    }
                    else
                        Text += $"{item.Name}";

                    if (item.Type == "MenuButton")
                        Value = ">";
                    else if (item.Type == "Float" && item.Value.GetType() == typeof(object))
                        Value = String.Format("<{0:0.00000}>", item.Value.GetType().GetProperty("Value").GetValue(item));
                    else if (item.Type == "Float")
                        Value = String.Format("<{0:0.00000}>", item.Value);
                    else if (item.Type == "Int" && item.Value.GetType() == typeof(object))
                        Value = $"<{item.Value.GetType().GetProperty("Value").GetValue(item)}>";
                    else if (item.Type == "Int")
                        Value = $"<{item.Value}>";
                    else if (item.Type == "Bool" && (bool)item.Value == true)
                        Value = "[X]";
                    else if (item.Type == "Bool" && (bool)item.Value == false)
                        Value = "[ ]";

                    Overlay.o.Dispatcher.BeginInvoke((Action<int>)delegate (int idx)
                    {
                        try
                        {
                            if (item.Type != "SubHeader")
                            {
                                Overlay.o.OptionsBlock.Inlines.Add(new Run(Text) { Foreground = FColour, FontSize = yRes / 45 });
                                Overlay.o.ValueBlock.Inlines.Add(new Run(Value) { Foreground = FColour, FontSize = yRes / 45 });
                                if (Description != string.Empty && idx == SelectedOptionIndex)
                                { Overlay.o.DescriptionBlock.Text = Description; Overlay.o.DescriptionBlock.FontSize = yRes / 45; Overlay.o.DescriptionBlock.Foreground = Brushes.White; }
                                else if (idx == SelectedOptionIndex)
                                    Overlay.o.DescriptionBlock.Text = string.Empty;
                            }
                            else
                            {
                                Overlay.o.OptionsBlock.Inlines.Add(new InlineUIContainer
                                {
                                    Child = new TextBlock
                                    {
                                        Text = Text,
                                        Foreground = FColour,
                                        FontSize = yRes / 45,
                                        Width = Overlay.o.Width - 10,
                                        HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                                        TextAlignment = TextAlignment.Center
                                    }
                                });
                                Overlay.o.ValueBlock.Inlines.Add(new Run("") { FontSize = yRes / 45 });
                            }
                        }
                        catch { }
                    }, index);

                    if (Overlay.o.AllMenus[CurrentMenu].IndexOf(item) != Overlay.o.AllMenus[CurrentMenu].Count - 1)
                        Overlay.o.Dispatcher.BeginInvoke((Action)delegate () { Overlay.o.OptionsBlock.Inlines.Add("\n"); Overlay.o.ValueBlock.Inlines.Add("\n"); });
                    index++;
                }

                LastMenu = CurrentMenu;
                LastSelectedOptionIndex = SelectedOptionIndex;
                LastValue = Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Value;
                MenuSize = Overlay.o.RenderSize;
            }
        }

        // Handles the input
        public void KeyHandler(CancellationToken ct)
        {
            while (true)
            {
                if (MainWindow.mw.gvp.Process.MainWindowHandle == GetForegroundWindow())
                {
                    if (ct.IsCancellationRequested)
                        return;
                    if (GetAsyncKeyState(Down) is 1 or Int16.MinValue && !DownKeyDown)
                        DownKeyDown = true;
                    if (GetAsyncKeyState(Down) is not 1 and not Int16.MinValue && DownKeyDown)
                        DownKeyDown = false;
                    if (GetAsyncKeyState(Up) is 1 or Int16.MinValue && !UpKeyDown)
                        UpKeyDown = true;
                    if (GetAsyncKeyState(Up) is not 1 and not Int16.MinValue && UpKeyDown)
                        UpKeyDown = false;
                    if (GetAsyncKeyState(Left) is 1 or Int16.MinValue && !LeftKeyDown)
                        LeftKeyDown = true;
                    if (GetAsyncKeyState(Left) is not 1 and not Int16.MinValue && LeftKeyDown)
                        LeftKeyDown = false;
                    if (GetAsyncKeyState(Right) is 1 or Int16.MinValue && !RightKeyDown)
                        RightKeyDown = true;
                    if (GetAsyncKeyState(Right) is not 1 and not Int16.MinValue && RightKeyDown)
                        RightKeyDown = false;

                    if (GetAsyncKeyState(Confirm) is 1 or Int16.MinValue)
                    {
                        if (Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Type == "MenuButton")
                        {
                            LevelIndex++;
                            string[] NameSplit = Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Name.Split(new char[] { ' ', '/', '[', ']' });
                            CurrentMenu = string.Empty;
                            foreach (string item in NameSplit)
                                CurrentMenu += char.ToUpper(item[0]) + item.Substring(1);
                            CurrentMenu += "Options";
                            History.Add(LevelIndex, CurrentMenu);
                            SelectedOptionIndex = 0;
                        }
                        else if (Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Bool")
                        {
                            if ((bool)Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Value == true)
                                Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Value = false;
                            else
                                Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Value = true;
                        }
                        else if (Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Button")
                        {
                            ((Action)Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Value)();
                        }
                        while (GetAsyncKeyState(Confirm) is 1 or Int16.MinValue)
                            Thread.Sleep(10);
                    }
                    if (GetAsyncKeyState(Leave) is 1 or Int16.MinValue)
                    {
                        if (LevelIndex == 0)
                        {
                            Overlay.o.Dispatcher.Invoke(delegate { Overlay.o.Hide(); });
                            Hidden = true;
                            continue;
                        }
                        LevelIndex--;
                        CurrentMenu = History[LevelIndex];
                        History.Remove(LevelIndex + 1);
                        SelectedOptionIndex = 0;
                        while (GetAsyncKeyState(Leave) is 1 or Int16.MinValue)
                            Thread.Sleep(10);
                    }
                    if (GetAsyncKeyState(OverlayVisibility) is 1 or Int16.MinValue)
                    {
                        if (Overlay.o.Visibility == Visibility.Visible && !Hidden)
                            Overlay.o.Dispatcher.Invoke(delegate { Overlay.o.Hide(); });
                        else
                            Overlay.o.Dispatcher.Invoke(delegate { Overlay.o.Show(); });
                        Hidden = !Hidden;
                        while (GetAsyncKeyState(OverlayVisibility) is 1 or Int16.MinValue)
                            Thread.Sleep(10);
                    }
                    Thread.Sleep(10);
                }
            }
        }
        public void ChangeSelection(CancellationToken ct)
        {
            while (true)
            {
                if (ct.IsCancellationRequested)
                    return;
                if (DownKeyDown)
                {
                    int count = 0;
                    SelectedOptionIndex++;
                    if (SelectedOptionIndex > Overlay.o.AllMenus[CurrentMenu].Count - 1)
                        SelectedOptionIndex = 0;

                    Timer timer = new Timer();
                    timer.Interval = 500;
                    timer.Tick += delegate
                    {
                        if (count == 0)
                            timer.Interval = 100;
                        SelectedOptionIndex++;
                        if (SelectedOptionIndex > Overlay.o.AllMenus[CurrentMenu].Count - 1)
                            SelectedOptionIndex = 0;
                        count++;
                        Thread.Sleep(1);
                    };
                    Overlay.o.Dispatcher.Invoke(delegate { timer.Start(); });
                    while (DownKeyDown) { Thread.Sleep(1); }
                    Overlay.o.Dispatcher.Invoke(delegate { timer.Dispose(); });
                }
                if (UpKeyDown)
                {
                    int count = 0;
                    SelectedOptionIndex--;
                    if (SelectedOptionIndex < 0)
                        SelectedOptionIndex = Overlay.o.AllMenus[CurrentMenu].Count - 1;

                    Timer timer = new Timer();
                    timer.Interval = 500;
                    timer.Tick += delegate
                    {
                        if (count == 0)
                            timer.Interval = 100;
                        SelectedOptionIndex--;
                        if (SelectedOptionIndex < 0)
                            SelectedOptionIndex = Overlay.o.AllMenus[CurrentMenu].Count - 1;
                        count++;
                        Thread.Sleep(1);
                    };
                    Overlay.o.Dispatcher.Invoke(delegate { timer.Start(); });
                    while (UpKeyDown) { Thread.Sleep(1); }
                    Overlay.o.Dispatcher.Invoke(delegate { timer.Dispose(); });
                }
                if (RightKeyDown)
                {
                    int count = 0;
                    var Inc = delegate ()
                    {
                        if (Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Float" || Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Int")
                        {
                            if (Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Float")
                                Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Value = Convert.ToSingle(Math.Round((float)Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Value + 0.1f, 1));
                            else
                                Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Value = (int)Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Value + 1;
                        }
                    };
                    Inc();

                    Timer timer = new Timer();
                    timer.Interval = 250;
                    timer.Tick += delegate
                    {
                        if (count > 0)
                            timer.Interval = 5;
                        Inc();
                        count++;
                        Thread.Sleep(1);
                    };
                    Overlay.o.Dispatcher.Invoke(delegate { timer.Start(); });
                    while (RightKeyDown) { Thread.Sleep(1); }
                    Overlay.o.Dispatcher.Invoke(delegate { timer.Dispose(); });
                }
                if (LeftKeyDown)
                {
                    int count = 0;
                    var Dec = delegate ()
                    {
                        if (Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Float" || Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Int")
                        {
                            if (Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Float")
                                Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Value = Convert.ToSingle(Math.Round((float)Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Value - 0.1f, 1));
                            else
                                Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Value = (int)Overlay.o.AllMenus[CurrentMenu][SelectedOptionIndex].Value - 1;
                        }
                    };
                    Dec();

                    Timer timer = new Timer();
                    timer.Interval = 250;
                    timer.Tick += delegate
                    {
                        if (count > 0)
                            timer.Interval = 5;
                        Dec();
                        count++;
                        Thread.Sleep(1);
                    };
                    Overlay.o.Dispatcher.Invoke(delegate { timer.Start(); });
                    while (LeftKeyDown) { Thread.Sleep(1); }
                    Overlay.o.Dispatcher.Invoke(delegate { timer.Dispose(); });
                }
                Thread.Sleep(10);
            }
        }

        // Configs and enables blur
        internal static void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(Overlay.o);

            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;
            //accent.GradientColor = ((_blurOpacity << 24) | (_blurBackgroundColor & 0xFFFFFF));

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }
    }
}
