using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Timer = System.Windows.Forms.Timer;

namespace WPF_Mockup.Overlay
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
            public object Value
            {
                get => _value;
                set
                {
                    _value = value;
                    ValueChanged(_value);
                }
            }
            private object _value;
            public string Description { get; }
            // Value changed event handler
            public event EventHandler ValueChangedHandler;
            public void ValueChanged(object value)
            {
                EventHandler handler = ValueChangedHandler;
                if (null != handler) handler(this, EventArgs.Empty);
            }

            //Constructors for different value types
            //float
            public MenuOption(string name, string type, float value, string description = null)
            {
                Name = name;
                Type = type;
                Value = value;
                Description = description;
            }
            //bool
            public MenuOption(string name, string type, bool value, string description = null)
            {
                Name = name;
                Type = type;
                Value = value;
                Description = description;
            }
            //int
            public MenuOption(string name, string type, int value, string description = null)
            {
                Name = name;
                Type = type;
                Value = value;
                Description = description;
            }
            //method
            public MenuOption(string name, string type, Action value, string description = null)
            {
                Name = name;
                Type = type;
                Value = value;
                Description = description;
            }
            //null value
            public MenuOption(string name, string type, string description = null)
            {
                Name = name;
                Type = type;
                Value = null;
                Description = description;
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

        // Menu operational vars
        int SelectedOptionIndex = 0;
        int LevelIndex = 0;
        public string CurrentMenu = "MainOptions";
        bool Hidden = false;
        Dictionary<int, string> History = new Dictionary<int, string>()
        {
            {  0 ,"MainOptions" }
        };
        
        // Opacity vars (not used with fast blur mode)
        private uint _blurBackgroundColor = 0x990000;
        private uint _blurOpacity = 0x4B;
        
        // Key vars
        bool UpKeyDown = false;
        bool DownKeyDown = false;
        bool LeftKeyDown = false;
        bool RightKeyDown = false;

        // Menu theme vars
        public Brush MainBackColour = Brushes.Transparent;
        public Brush DescriptionBackColour = Brushes.Transparent;
        public Brush MainBorderColour = Brushes.Black;
        public Brush DescriptionBorderColour = Brushes.Black;
        
        #endregion
        #region Menus
        // Every single menu/submenu has to be put in here to work
        Dictionary<string, List<MenuOption>> AllMenus = new Dictionary<string, List<MenuOption>>()
        {
            { "MainOptions" , MainOptions },
                { "SelfCarsOptions" , SelfCarMenu.SelfCarMenu.SelfCarsOptions},
                    { "ModifiersOptions" , SelfCarMenu.ModifiersMenu.ModifiersMenu.ModifiersOptions},
                        { "GravityOptions" , SelfCarMenu.ModifiersMenu.ModifiersMenu.GravityOptions},
                        { "AccelerationOptions" , SelfCarMenu.ModifiersMenu.ModifiersMenu.AccelerationOptions},
                    { "SpeedhacksOptions" , SelfCarMenu.SpeedhacksMenu.SpeedhacksMenu.SpeedhacksOptions},
                    { "UnlocksOptions" , UnlocksOptions},
                    { "CameraOptions" , CameraOptions},
                { "SettingsOptions" , SettingsMenu.SettingsMenu.SettingsOptions},
                    { "MainAreaOptions" , SettingsMenu.SettingsMenu.MainAreaOptions},
                    { "DescriptionAreaOptions" , SettingsMenu.SettingsMenu.DescriptionAreaOptions}
        };

        // Main menu items, all submenus have their own class in Tabs.Overlay
        static List<MenuOption> MainOptions = new List<MenuOption>()
        {
            new MenuOption("Autoshow", "MenuButton", "Mods for the Autoshow such as free cars, all cars etc"),
            new MenuOption("Self/Cars", "MenuButton", "Mods for yourself such as speedhack, flyhack etc"),
            new MenuOption("Settings", "MenuButton")
        };
        static List<MenuOption> UnlocksOptions = new List<MenuOption>()
        {
            new MenuOption("Clothes", "MenuButton"),
            new MenuOption("Horns", "MenuButton"),
            new MenuOption("Emotes", "MenuButton")
        };
        static List<MenuOption> CameraOptions = new List<MenuOption>()
        {
            new MenuOption("FOV", "MenuButton")
        };

        // Add all sub menu classes here for event handling
        SelfCarMenu.ModifiersMenu.ModifiersMenu mm = new SelfCarMenu.ModifiersMenu.ModifiersMenu();
        SettingsMenu.SettingsMenu sm = new SettingsMenu.SettingsMenu();
        #endregion
        #region Main
        public Overlay()
        {
            InitializeComponent();
            o = this;
            DataContext = this;
            InitializeAllSubMenus();
        }

        // This is to make sure all sub menus have their event handlers subscribed
        void InitializeAllSubMenus()
        {
            mm.InitiateSubMenu();
            sm.InitiateSubMenu();
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
                Task.Run(() => OverlayPosAndScale(cts.Token));
                Task.Run(() => UpdateMenuOptions(cts.Token));
                Task.Run(() => KeyHandler(cts.Token));
                Task.Run(() => ChangeSelection(cts.Token));
            }
            else
            {
                cts.Cancel();
                cts.Dispose();
            }
        }
        #endregion
        #region Functions

        // Handles the position of the overlay
        void OverlayPosAndScale(CancellationToken ct)
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
                
                // Forza window x and y coords
                double PosTop = ForzaWindow.Top + ((ForzaWindow.Bottom - ForzaWindow.Top - ForzaClientWindow.Bottom) / 1.3) + Offset;
                double PosLeft = ForzaWindow.Left + ((ForzaWindow.Right - ForzaWindow.Left - ForzaClientWindow.Right) / 2) + Offset;

                // Forzas current resolution (doesnt account for scaling)
                double yRes = ForzaClientWindow.Bottom - ((ForzaWindow.Bottom - ForzaWindow.Top - ForzaClientWindow.Bottom) / 1.3);

                // Calculate the right numbers for the menu to scale to resolution
                double HeaderY = yRes / 10.8;
                double HeaderX = HeaderY * 4;


                if (MainWindow.mw.gvp.Process.MainWindowHandle == GetForegroundWindow())
                {
                    Dispatcher.Invoke(delegate ()
                    {
                        if (Visibility == Visibility.Hidden && !Hidden)
                            Show();

                        // Set position
                        Top = PosTop;
                        Left = PosLeft;

                        // Set width of menu and set header size (scale with resolution)
                        Width = HeaderX;
                        TopSection.Height = new GridLength(HeaderY);

                        Header.Width = Width;
                        Header.Height = TopSection.ActualHeight;

                        // Set height of menu depending on items present
                        if (OptionsBlock.Inlines.Count == (AllMenus[CurrentMenu].Count * 2) - 1)
                        {
                            MainSection.Height = new GridLength(5 + OptionsBlock.ActualHeight + 5);
                            if (DescriptionBlock.Text != string.Empty)
                                DescriptionSection.Height = new GridLength(5 + 5 + DescriptionBlock.ActualHeight + 5);
                            else
                                DescriptionSection.Height = new GridLength(0);

                            Height = TopSection.ActualHeight + MainSection.ActualHeight + DescriptionSection.ActualHeight;
                        }

                        // Set colours of menu
                        MainBorder.Background = MainBackColour;
                        MainBorder.BorderBrush = MainBorderColour;

                        DescriptionBorder.Background = DescriptionBackColour;
                        DescriptionBorder.BorderBrush = DescriptionBorderColour;
                    });
                }
                else { Dispatcher.Invoke(delegate() { Hide(); }); }
                Thread.Sleep(1);
            }
        }

        // Updates the menu, eg selected option, values etc
        void UpdateMenuOptions(CancellationToken ct)
        {
            while(true)
            {
                if (ct.IsCancellationRequested)
                    return;
                
                // Clears the menu
                Dispatcher.BeginInvoke((Action)delegate () { OptionsBlock.Inlines.Clear(); ValueBlock.Inlines.Clear(); });
                int index = 0;

                // Gets y resolution of the forza client window
                RECT ForzaWindow = new RECT();
                RECT ForzaClientWindow = new RECT();

                GetWindowRect(MainWindow.mw.gvp.Process.MainWindowHandle, ref ForzaWindow);
                GetClientRect(MainWindow.mw.gvp.Process.MainWindowHandle, out ForzaClientWindow);

                double yRes = ForzaClientWindow.Bottom - ((ForzaWindow.Bottom - ForzaWindow.Top - ForzaClientWindow.Bottom) / 1.3);

                // Adds all menu options to the menu
                foreach (MenuOption item in AllMenus[CurrentMenu])
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

                    Dispatcher.BeginInvoke((Action<int>)delegate (int idx)
                    {
                        try
                        {
                            if (item.Type != "SubHeader")
                            {
                                OptionsBlock.Inlines.Add(new Run(Text) { Foreground = FColour, FontSize = yRes / 45 });
                                ValueBlock.Inlines.Add(new Run(Value) { Foreground = FColour, FontSize = yRes / 45 });
                                if (Description != string.Empty && idx == SelectedOptionIndex)
                                    { DescriptionBlock.Text = Description; DescriptionBlock.FontSize = yRes / 45; DescriptionBlock.Foreground = Brushes.White; }
                                else if (idx == SelectedOptionIndex)
                                    DescriptionBlock.Text = string.Empty;
                            }
                            else
                            {
                                OptionsBlock.Inlines.Add(new InlineUIContainer
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
                                ValueBlock.Inlines.Add(new Run("") { FontSize = yRes / 45 });
                            }
                        }
                        catch { }
                    }, index);

                    if (AllMenus[CurrentMenu].IndexOf(item) != AllMenus[CurrentMenu].Count - 1)
                        Dispatcher.BeginInvoke((Action)delegate () { OptionsBlock.Inlines.Add("\n"); ValueBlock.Inlines.Add("\n"); });
                    index++;
                }

                // Selected option background
                Dispatcher.Invoke((Action)delegate ()
                {
                    if(OptionsBlock.Inlines.Count > 1)
                    {
                        foreach (UIElement Child in Layout.Children)
                        {
                            if (Child.GetType().GetProperty("Name").GetValue(Child) == "Highlight")
                            {
                                Layout.Children.Remove(Child);
                                break;
                            }
                        }
                        float height = (float)(((OptionsBlock.ActualHeight / AllMenus[CurrentMenu].Count) * SelectedOptionIndex) + 5 );
                        Border Highlighted = new Border()
                        {
                            Name = "Highlight",
                            VerticalAlignment = VerticalAlignment.Top,
                            Background = Brushes.Black,
                            Width = Layout.ActualWidth,
                            Height = OptionsBlock.ActualHeight / AllMenus[CurrentMenu].Count,
                            Margin = new Thickness(0, height, 0, 0)
                        };
                        Grid.SetColumn(Highlighted, 0);
                        Grid.SetRow(Highlighted, 1);
                        System.Windows.Controls.Panel.SetZIndex(Highlighted, System.Windows.Controls.Panel.GetZIndex(OptionsBlock) - 1);
                        Layout.Children.Add(Highlighted);
                    }
                });

                Thread.Sleep(10);
            }
        }

        // Handles the input
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
                if (GetAsyncKeyState(Keys.NumPad4) is 1 or Int16.MinValue && !LeftKeyDown)
                    LeftKeyDown= true;
                if (GetAsyncKeyState(Keys.NumPad4) is not 1 and not Int16.MinValue && LeftKeyDown)
                    LeftKeyDown = false;
                if (GetAsyncKeyState(Keys.NumPad6) is 1 or Int16.MinValue && !RightKeyDown)
                    RightKeyDown = true;
                if (GetAsyncKeyState(Keys.NumPad6) is not 1 and not Int16.MinValue && RightKeyDown)
                    RightKeyDown = false;

                if (GetAsyncKeyState(Keys.NumPad5) is 1 or Int16.MinValue)
                {
                    if (AllMenus[CurrentMenu][SelectedOptionIndex].Type == "MenuButton")
                    {
                        LevelIndex++;
                        string[] NameSplit = AllMenus[CurrentMenu][SelectedOptionIndex].Name.Split(new char[] {' ', '/', '[', ']'});
                        CurrentMenu = string.Empty;
                        foreach (string item in NameSplit)
                            CurrentMenu += char.ToUpper(item[0]) + item.Substring(1);
                        CurrentMenu += "Options";
                        History.Add(LevelIndex, CurrentMenu);
                        SelectedOptionIndex = 0;
                    }
                    else if(AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Bool")
                    {
                        if ((bool)AllMenus[CurrentMenu][SelectedOptionIndex].Value == true)
                            AllMenus[CurrentMenu][SelectedOptionIndex].Value = false;
                        else
                            AllMenus[CurrentMenu][SelectedOptionIndex].Value = true;
                    }
                    else if (AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Button")
                    {
                        ((Action)AllMenus[CurrentMenu][SelectedOptionIndex].Value)();
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
                    int count = 0;
                    SelectedOptionIndex++;
                    if (SelectedOptionIndex > AllMenus[CurrentMenu].Count - 1)
                        SelectedOptionIndex = 0;

                    Timer timer = new Timer();
                    timer.Interval = 500;
                    timer.Tick += delegate
                    {
                        if (count == 0)
                            timer.Interval = 100;
                        SelectedOptionIndex++;
                        if (SelectedOptionIndex > AllMenus[CurrentMenu].Count - 1)
                            SelectedOptionIndex = 0;
                        count++;
                        Thread.Sleep(1);
                    };
                    Dispatcher.Invoke(delegate { timer.Start(); });
                    while (DownKeyDown) { Thread.Sleep(1); }
                    Dispatcher.Invoke(delegate { timer.Dispose(); });
                }
                if (UpKeyDown)
                {
                    int count = 0;
                    SelectedOptionIndex--;
                    if (SelectedOptionIndex < 0)
                        SelectedOptionIndex = AllMenus[CurrentMenu].Count - 1;

                    Timer timer = new Timer();
                    timer.Interval = 500;
                    timer.Tick += delegate
                    {
                        if (count == 0)
                            timer.Interval = 100;
                        SelectedOptionIndex--;
                        if (SelectedOptionIndex < 0)
                            SelectedOptionIndex = AllMenus[CurrentMenu].Count - 1;
                        count++;
                        Thread.Sleep(1);
                    };
                    Dispatcher.Invoke(delegate { timer.Start(); });
                    while (UpKeyDown) { Thread.Sleep(1); }
                    Dispatcher.Invoke(delegate { timer.Dispose(); });
                }
                if (RightKeyDown)
                {
                    int count = 0;
                    var Inc = delegate () 
                    {
                        if (AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Float" || AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Int")
                        {
                            if (AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Float")
                                AllMenus[CurrentMenu][SelectedOptionIndex].Value = Convert.ToSingle(Math.Round((float)AllMenus[CurrentMenu][SelectedOptionIndex].Value + 0.1f, 1));
                            else
                                AllMenus[CurrentMenu][SelectedOptionIndex].Value = (int)AllMenus[CurrentMenu][SelectedOptionIndex].Value + 1;
                        }
                    };
                    Inc();
                    
                    Timer timer = new Timer();
                    timer.Interval = 250;
                    timer.Tick += delegate
                    {
                        if (count > 3)
                            timer.Interval = 5;
                        Inc();
                        count++;
                        Thread.Sleep(1);
                    };
                    Dispatcher.Invoke(delegate { timer.Start(); });
                    while (RightKeyDown) { Thread.Sleep(1); }
                    Dispatcher.Invoke(delegate { timer.Dispose(); });
                }
                if (LeftKeyDown)
                {
                    int count = 0;
                    var Dec = delegate ()
                    {
                        if (AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Float" || AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Int")
                        {
                            if (AllMenus[CurrentMenu][SelectedOptionIndex].Type == "Float")
                                AllMenus[CurrentMenu][SelectedOptionIndex].Value = Convert.ToSingle(Math.Round((float)AllMenus[CurrentMenu][SelectedOptionIndex].Value - 0.1f, 1));
                            else
                                AllMenus[CurrentMenu][SelectedOptionIndex].Value = (int)AllMenus[CurrentMenu][SelectedOptionIndex].Value - 1;
                        }
                    };
                    Dec();

                    Timer timer = new Timer();
                    timer.Interval = 250;
                    timer.Tick += delegate
                    {
                        if (count > 3)
                            timer.Interval = 5;
                        Dec();
                        count++;
                        Thread.Sleep(1);
                    };
                    Dispatcher.Invoke(delegate { timer.Start(); });
                    while (LeftKeyDown) { Thread.Sleep(1); }
                    Dispatcher.Invoke(delegate { timer.Dispose(); });
                }
                Thread.Sleep(1);
            }
        }

        // Configs and enables blur
        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

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
        #endregion
    }
}
