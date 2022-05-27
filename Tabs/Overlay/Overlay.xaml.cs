using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Timer = System.Windows.Forms.Timer;

namespace WPF_Mockup.Tabs.Overlay
{
    /// <summary>
    /// Interaction logic for Overlay.xaml
    /// </summary>
    public partial class Overlay : Window
    {
        public class MenuOption
        {
            public string Name { get; }
            public string Type { get; }
            public string Value { get; set; }

            public MenuOption(string name, string type, string value)
            {
                Name = name;
                Type = type;
                Value = value;
            }

        }      
        #region DLLImports
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
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
        #region Click Through DLLImports
        //Credits to Oleg Kolosov for the transparency https://stackoverflow.com/a/3367137
        const int WS_EX_TRANSPARENT = 0x00000020;
        const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);
        #endregion
        #region Variables
        static CancellationTokenSource cts = null;
        public static Overlay o;
        int SelectedOptionIndex = 0;
        int LevelIndex = 0;
        string CurrentMenu = "MainOptions";
        bool Hidden = false;
        Dictionary<int, string> History = new Dictionary<int, string>()
        {
            {  0 ,"MainOptions" }
        };
        private uint _blurBackgroundColor = 0x990000;
        bool UpKeyDown = false;
        bool DownKeyDown = false;
        #endregion
        #region Menus
        Dictionary<string, List<MenuOption>> AllMenus = new Dictionary<string, List<MenuOption>>()
        {
            { "MainOptions" , MainOptions },
            { "SelfCarsOptions" , SelfCarMenu.SelfCarMenu.SelfCarsOptions},
            { "SpeedhacksOptions" , SpeedhacksOptions},
            { "UnlocksOptions" , UnlocksOptions},
            { "CameraOptions" , CameraOptions},
            { "ModifiersOptions" , SelfCarMenu.ModifiersMenu.ModifiersMenu.ModifiersOptions},
            { "GravityOptions" , SelfCarMenu.ModifiersMenu.ModifiersMenu.GravityOptions}
        };
        static List<MenuOption> MainOptions = new List<MenuOption>()
        {
            new MenuOption("Autoshow", "MenuButton", null),
            new MenuOption("Self/Cars", "MenuButton", null),
            new MenuOption("Settings", "MenuButton", null)
        };
        static List<MenuOption> SpeedhacksOptions = new List<MenuOption>()
        {
            new MenuOption("Velocity", "MenuButton", null),
            new MenuOption("Wheel Speed", "MenuButton", null),
            new MenuOption("Super Car", "MenuButton", null)
        };
        static List<MenuOption> UnlocksOptions = new List<MenuOption>()
        {
            new MenuOption("Clothes", "MenuButton", null),
            new MenuOption("Horns", "MenuButton", null),
            new MenuOption("Emotes", "MenuButton", null)
        };
        static List<MenuOption> CameraOptions = new List<MenuOption>()
        {
            new MenuOption("FOV", "MenuButton", null)
        };
        #endregion

        public Overlay()
        {
            InitializeComponent();
            o = this;
            DataContext = this;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
            var windowHwnd = new WindowInteropHelper(this).Handle;
            var extendedStyle = GetWindowLong(windowHwnd, GWL_EXSTYLE);
            SetWindowLong(windowHwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
        }
        public void OverlayToggle(bool Toggle)
        {
            if (Toggle)
            {
                cts = new CancellationTokenSource();
                Task.Run(() => OverlayPos(cts.Token));
                Task.Run(() => UpdateMenuOptionsAsync(cts.Token));
                Task.Run(() => KeyHandler(cts.Token));
                Task.Run(() => ChangeSelection(cts.Token));
            }
            else
            {
                cts.Cancel();
                cts.Dispose();
            }
        }
        #region Functions
        void OverlayPos(CancellationToken ct)
        {
            while (true)
            {
                if (ct.IsCancellationRequested)
                    return;

                RECT ForzaWindow = new RECT();
                RECT ForzaClientWindow = new RECT();

                GetWindowRect(MainWindow.mw.gvp.Process.MainWindowHandle, ref ForzaWindow);
                GetClientRect(MainWindow.mw.gvp.Process.MainWindowHandle, out ForzaClientWindow);
                
                double Offset = ForzaClientWindow.Bottom / 20;

                double PosTop = ForzaWindow.Top + ((ForzaWindow.Bottom - ForzaWindow.Top - ForzaClientWindow.Bottom) / 1.3) + Offset;
                double PosLeft = ForzaWindow.Left + ((ForzaWindow.Right - ForzaWindow.Left - ForzaClientWindow.Right) / 2) + Offset;

                if (MainWindow.mw.gvp.Process.MainWindowHandle == GetForegroundWindow())
                {
                    Dispatcher.Invoke(delegate ()
                    {
                        if (Visibility == Visibility.Hidden && !Hidden)
                            Show();
                        Top = PosTop;
                        Left = PosLeft;
                        Width = ForzaClientWindow.Right / 4;
                    });
                }
                else
                {
                    Dispatcher.Invoke(delegate ()
                    {
                        Hide();
                    });
                }
                Thread.Sleep(1);
            }
        }
        void UpdateMenuOptionsAsync(CancellationToken ct)
        {
            while(true)
            {
                if (ct.IsCancellationRequested)
                    return;
                Dispatcher.BeginInvoke((Action)delegate () { OptionsBlock.Inlines.Clear(); });
                int index = 0;
                
                foreach (MenuOption item in AllMenus[CurrentMenu])
                {
                    string Item = string.Empty;
                    SolidColorBrush Colour = Brushes.White;
                    if (index == SelectedOptionIndex)
                    {
                        Item += $"[{item.Name}]";
                        Colour = Brushes.Green;
                    }
                    else
                        Item += $"{item.Name}";

                    if (item.Type == "Bool" && item.Value == "True")
                        Item += "   ■";
                    else if (item.Type == "Bool" && item.Value == "False")
                        Item += "   □";
                    else if (item.Type == "Float")
                        Item += $"  {item.Value}";
                    
                    Dispatcher.BeginInvoke((Action<string, SolidColorBrush>) ((string value1, SolidColorBrush Value2) =>
                    {
                        OptionsBlock.Inlines.Add(new Run(value1) { Foreground = Value2 });
                    }), Item, Colour);
                    
                    if (AllMenus[CurrentMenu].IndexOf(item) != AllMenus[CurrentMenu].Count - 1)
                        Dispatcher.BeginInvoke((Action)delegate () { OptionsBlock.Inlines.Add("\n"); });
                    index++;
                }

                Dispatcher.Invoke(delegate () { Height = OptionsBlock.ActualHeight + TopSection.ActualHeight + BottomSection.ActualHeight; });
                
                Thread.Sleep(10);
            }
        }
        void KeyHandler(CancellationToken ct)
        {
            while(true)
            {
                if (ct.IsCancellationRequested)
                    return;
                if (GetAsyncKeyState(Keys.NumPad2) is 1 or Int16.MinValue && !DownKeyDown)
                    DownKeyDown = true;
                if (GetAsyncKeyState(Keys.NumPad2) is not 1 and not Int16.MinValue && DownKeyDown)
                    DownKeyDown = false;
                if (GetAsyncKeyState(Keys.NumPad8) is 1 or Int16.MinValue && !UpKeyDown)
                    UpKeyDown = true;
                if (GetAsyncKeyState(Keys.NumPad8) is not 1 and not Int16.MinValue && UpKeyDown)
                    UpKeyDown = false;

                if (GetAsyncKeyState(Keys.NumPad5) is 1 or Int16.MinValue)
                {
                    if(AllMenus[CurrentMenu][SelectedOptionIndex].Type == "MenuButton")
                    {
                        LevelIndex++;
                        CurrentMenu = AllMenus[CurrentMenu][SelectedOptionIndex].Name.Replace("<", string.Empty).Replace("<", string.Empty).Replace("/", string.Empty) + "Options";
                        History.Add(LevelIndex, CurrentMenu);
                        SelectedOptionIndex = 0;
                    }
                    else if(AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Bool")
                    {
                        if (AllMenus[CurrentMenu][SelectedOptionIndex].Value == "True")
                            AllMenus[CurrentMenu][SelectedOptionIndex].Value = "False";
                        else
                            AllMenus[CurrentMenu][SelectedOptionIndex].Value = "True";
                    }
                    while (GetAsyncKeyState(Keys.NumPad5) is 1 or Int16.MinValue)
                        Thread.Sleep(10);
                }
                if (GetAsyncKeyState(Keys.NumPad0) is 1 or Int16.MinValue)
                {
                    if(LevelIndex == 0)
                    {
                        Dispatcher.Invoke(delegate { Hide(); });
                        Hidden = true;
                        continue;
                    }
                    LevelIndex--;
                    CurrentMenu = History[LevelIndex];
                    History.Remove(LevelIndex + 1);
                    SelectedOptionIndex = 0;
                    while (GetAsyncKeyState(Keys.NumPad0) is 1 or Int16.MinValue)
                        Thread.Sleep(10);
                }
                if (GetAsyncKeyState(Keys.Subtract) is 1 or Int16.MinValue)
                {
                    if (Visibility == Visibility.Visible && !Hidden)
                        Dispatcher.Invoke(delegate { Hide(); });
                    else
                        Dispatcher.Invoke(delegate { Show(); });
                    Hidden = !Hidden;
                    while (GetAsyncKeyState(Keys.Subtract) is 1 or Int16.MinValue)
                        Thread.Sleep(10);
                }
                Thread.Sleep(10);
            }
        }
        void ChangeSelection(CancellationToken ct)
        {
            while (true)
            {
                if (ct.IsCancellationRequested)
                    return;
                if (DownKeyDown)
                {
                    SelectedOptionIndex++;
                    if (SelectedOptionIndex > AllMenus[CurrentMenu].Count - 1)
                        SelectedOptionIndex = 0;

                    Timer timer = new Timer();
                    timer.Interval = 100;
                    timer.Tick += delegate
                    {
                        SelectedOptionIndex++;
                        if (SelectedOptionIndex > AllMenus[CurrentMenu].Count - 1)
                            SelectedOptionIndex = 0;
                        Thread.Sleep(1);
                    };
                    Dispatcher.Invoke(delegate { timer.Start(); });
                    while (DownKeyDown) { Thread.Sleep(1); }
                    Dispatcher.Invoke(delegate { timer.Dispose(); });
                }
                if (UpKeyDown)
                {
                    SelectedOptionIndex--;
                    if (SelectedOptionIndex < 0)
                        SelectedOptionIndex = AllMenus[CurrentMenu].Count - 1;

                    Timer timer = new Timer();
                    timer.Interval = 100;
                    timer.Tick += delegate
                    {
                        SelectedOptionIndex--;
                        if (SelectedOptionIndex < 0)
                            SelectedOptionIndex = AllMenus[CurrentMenu].Count - 1;
                        Thread.Sleep(1);
                    };
                    Dispatcher.Invoke(delegate { timer.Start(); });
                    while (UpKeyDown) { Thread.Sleep(1); }
                    Dispatcher.Invoke(delegate { timer.Dispose(); });
                }
                Thread.Sleep(1);
            }
        }
        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;
            accent.GradientColor = (uint)((128 << 24) | (_blurBackgroundColor & 0xFFFFFF));

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
        #endregion
    }
}
