using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Forza_Mods_AIO.Resources;
using static Forza_Mods_AIO.MainWindow;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Timer = System.Windows.Forms.Timer;
using static Forza_Mods_AIO.Overlay.Overlay;
using static Forza_Mods_AIO.Resources.DllImports;

namespace Forza_Mods_AIO.Overlay;

public partial class OverlayHandling
{
    #region Blur DLLImports
    //Credits to Rafael Rivera for the blur https://github.com/riverar/sample-win32-acrylicblur
    internal enum AccentState
    {
        AccentEnableBlurbehind = 3
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct AccentPolicy
    {
        public AccentState AccentState;
        public uint AccentFlags;
        public uint GradientColor;
        public uint AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    private enum WindowCompositionAttribute
    {
        WcaAccentPolicy = 19
    }

    [LibraryImport("user32.dll")]
    private static partial void SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    #endregion
    #region Variables
    public static Keys Up = Keys.NumPad8;
    public static Keys Down = Keys.NumPad2;
    public static Keys Left = Keys.NumPad4;
    public static Keys Right = Keys.NumPad6;
    public static Keys Confirm = Keys.NumPad5;
    public static Keys Leave = Keys.NumPad0;
    public static Keys OverlayVisibility = Keys.Subtract;
    // Menu operational vars
    private string[] _menuHeaders;
    int _selectedOptionIndex;
    int _levelIndex;
    public string CurrentMenu = "MainOptions";
    private bool _hidden;
    Dictionary<int, string> _history = new()
    {
        {  0 ,"MainOptions" }
    };

    // Key vars
    private bool _upKeyDown, _downKeyDown, _leftKeyDown, _rightKeyDown;

    //Theme vars
    public Brush MainBackColour = Brushes.Transparent;
    public Brush DescriptionBackColour = Brushes.Transparent;
    public Brush MainBorderColour = Brushes.Black;
    public Brush DescriptionBorderColour = Brushes.Black;
    public int HeaderIndex = 0;
    public readonly List<object[]> Headers = new();
    private BitmapImage _headerImage;
    #endregion
    // Caches all the headers
    public void CacheHeaders()
    {
        if (!Directory.Exists(Environment.CurrentDirectory + @"\Overlay\Headers"))
        {
            return;
        }

        _menuHeaders = Directory.GetFiles(Environment.CurrentDirectory + @"\Overlay\Headers");
        foreach (string header in _menuHeaders)
        {
            var inCachedBitmaps = Headers.Any(item => item[0].ToString().Contains(header.Split('\\').Last().Split('.').First()));
            if (inCachedBitmaps) continue;
            Headers.Add(new object[] { header.Split('\\').Last().Split('.').First(), new BitmapImage(new Uri(header)) });
        }
    }
        
    public void LoadSettings()
    {
            
    }

    public void SaveSettings()
    {
            
    }
        
    // Handles the position of the overlay
    public void OverlayPosAndScale(CancellationToken ct)
    {
        CacheHeaders();
        while (true)
        {
            Task.Delay(10, ct).Wait(ct);
            if (ct.IsCancellationRequested)
            {
                return;
            }

            DllImports.Rect forzaWindow = new DllImports.Rect();

            GetWindowRect(Mw.Gvp.Process.MainWindowHandle, ref forzaWindow);
            GetClientRect(Mw.Gvp.Process.MainWindowHandle, out var forzaClientWindow);

            double offset = forzaClientWindow.Bottom / 20;

            // Forza window x and y coords
            double posTop = forzaWindow.Top + ((forzaWindow.Bottom - forzaWindow.Top - forzaClientWindow.Bottom) / 1.3) + offset;
            double posLeft = forzaWindow.Left + ((forzaWindow.Right - forzaWindow.Left - forzaClientWindow.Right) / 2) + offset;

            // Forzas current resolution (doesnt account for scaling)
            double yRes = forzaClientWindow.Bottom - ((forzaWindow.Bottom - forzaWindow.Top - forzaClientWindow.Bottom) / 1.3);

            // Calculate the right numbers for the menu to scale to resolution
            double headerY = yRes / 10.8;
            double headerX = headerY * 4;

            // Select header
            if (!Directory.Exists(Environment.CurrentDirectory + @"\Overlay\Headers") || _menuHeaders.Length == 0)
            {
                if (_headerImage is { IsFrozen: true })
                {
                    _headerImage = _headerImage.Clone();
                }

                _headerImage = new BitmapImage(new Uri("pack://application:,,,/Overlay/Headers/pog header.png", UriKind.RelativeOrAbsolute));
                try
                {
                    _headerImage.Freeze();
                }
                catch
                {
                    _headerImage.Dispatcher.Invoke(() => { _headerImage.Freeze(); });
                }
            }
            else if (_headerImage.UriSource.LocalPath != _menuHeaders[HeaderIndex])
            {
                if (_headerImage is { IsFrozen: true })
                {
                    _headerImage = _headerImage.Clone();
                }

                _headerImage = (BitmapImage)Headers.Find(x => x[0].ToString().Contains(_menuHeaders[HeaderIndex].Split('\\').Last().Split('.').First()))[1];
                try
                {
                    _headerImage.Freeze();
                }
                catch
                {
                    _headerImage.Dispatcher.Invoke(() => { _headerImage.Freeze(); });
                }
            }

            if (Mw.Gvp.Process.MainWindowHandle == GetForegroundWindow())
            {
                o?.Dispatcher.Invoke(delegate ()
                {
                    // Set position
                    o.Top = posTop;
                    o.Left = posLeft;

                    // Set width of menu and set header size (scale with resolution)
                    o.Width = headerX;
                    o.TopSection.Height = new GridLength(headerY);

                    o.Header.Width = o.Width;
                    o.Header.Height = o.TopSection.ActualHeight;

                    // Set height of menu depending on items present
                    if (o.OptionsBlock.Inlines.Count == o.AllMenus[CurrentMenu].Count * 2 - 1)
                    {
                        o.MainSection.Height = new GridLength(5 + o.OptionsBlock.ActualHeight + 5);
                        o.DescriptionSection.Height = o.DescriptionBlock.Text != string.Empty ? new GridLength(5 + 5 + o.DescriptionBlock.ActualHeight + 5) : new GridLength(0);

                        o.Height = o.TopSection.ActualHeight + o.MainSection.ActualHeight + o.DescriptionSection.ActualHeight;
                    }

                    // Set menu header image
                    o.Header.Source = _headerImage;
                    
                    // Set colours of menu
                    o.MainBorder.Background = MainBackColour;
                    o.MainBorder.BorderBrush = MainBorderColour;

                    o.DescriptionBorder.Background = DescriptionBackColour;
                    o.DescriptionBorder.BorderBrush = DescriptionBorderColour;

                    if (o.Visibility == Visibility.Hidden && !_hidden)
                    {
                        o.Show();
                    }
                });
            }
            else
            {
                o?.Dispatcher.Invoke(delegate
                {
                    o.Hide();
                });
            }
        }
    }

    // Updates the menu, eg selected option, values etc
    public void UpdateMenuOptions(CancellationToken ct)
    {
        while (true)
        {
            Task.Delay(10, ct).Wait(ct);
            if (ct.IsCancellationRequested)
                return;
            
            // Clears the menu
            o.Dispatcher.BeginInvoke((Action)delegate
            {
                o.OptionsBlock.Inlines.Clear(); 
                o.ValueBlock.Inlines.Clear();
            });
            int index = 0;

            // Gets y resolution of the forza client window
            DllImports.Rect forzaWindow = new DllImports.Rect();

            GetWindowRect(Mw.Gvp.Process.MainWindowHandle, ref forzaWindow);
            GetClientRect(Mw.Gvp.Process.MainWindowHandle, out var forzaClientWindow);

            double yRes = forzaClientWindow.Bottom - ((forzaWindow.Bottom - forzaWindow.Top - forzaClientWindow.Bottom) / 1.3);

            // Selected option background
            o.Dispatcher.Invoke((Action)delegate ()
            {
                if (o.OptionsBlock.Inlines.Count <= 1) return;
                // Remove previous highlight box
                foreach (UIElement child in o.Layout.Children)
                {
                    if ((string)child.GetType().GetProperty("Name")?.GetValue(child)! != "Highlight")
                    {
                        continue;
                    }
                    o.Layout.Children.Remove(child);
                    break;
                }
                // Create new highlight box
                var height = (float)(o.OptionsBlock.ActualHeight / o.AllMenus[CurrentMenu].Count * _selectedOptionIndex + 5);
                var highlighted = new Border
                {
                    Name = "Highlight",
                    VerticalAlignment = VerticalAlignment.Top,
                    Background = Brushes.Black,
                    Width = o.Layout.ActualWidth,
                    Height = o.OptionsBlock.ActualHeight / o.AllMenus[CurrentMenu].Count,
                    Margin = new Thickness(0, height, 0, 0)
                };
                Grid.SetColumn(highlighted, 0);
                Grid.SetRow(highlighted, 1);

                // Put highlight box behind text, add to layout
                System.Windows.Controls.Panel.SetZIndex(highlighted, 1);
                o.Layout.Children.Add(highlighted);
            });

            // Adds all menu options to the menu
            foreach (var item in o.AllMenus[CurrentMenu])
            {
                string text = string.Empty, value = string.Empty, description = string.Empty;
                SolidColorBrush fColour;
                if (index == _selectedOptionIndex)
                {
                    text = $"[{item.Name}]";
                    fColour = item.IsEnabled ? Brushes.Green : Brushes.DarkOliveGreen;
                    description = item.Description;
                }
                else
                {
                    text = $"{item.Name}";
                    fColour = item.IsEnabled ? Brushes.White : Brushes.DimGray;
                }

                value = item.Type switch
                {
                    OptionType.MenuButton => ">",
                    OptionType.Float => $"<{item.Value:0.00000}>",
                    OptionType.Int => $"<{item.Value}>",
                    OptionType.Bool when (bool)item.Value => "[X]",
                    OptionType.Bool when (bool)item.Value == false => "[ ]",
                    _ => value
                };

                o.Dispatcher.BeginInvoke((Action<int>)delegate (int idx)
                {
                    try
                    {
                        if (item.Type != OptionType.SubHeader)
                        {
                            o.OptionsBlock.Inlines.Add(new Run(text)
                            {
                                Foreground = fColour, 
                                FontSize = yRes / 45
                            });
                            
                            o.ValueBlock.Inlines.Add(new Run(value)
                            {
                                Foreground = fColour,
                                FontSize = yRes / 45
                            });
                            
                            if (description != string.Empty && idx == _selectedOptionIndex)
                            {
                                o.DescriptionBlock.Text = description; 
                                o.DescriptionBlock.FontSize = yRes / 45; 
                                o.DescriptionBlock.Foreground = Brushes.White;
                            }
                            else if (idx == _selectedOptionIndex)
                            {
                                o.DescriptionBlock.Text = string.Empty;
                            }
                        }
                        else
                        {
                            o.OptionsBlock.Inlines.Add(new InlineUIContainer
                            {
                                Child = new TextBlock
                                {
                                    Text = text,
                                    Foreground = fColour,
                                    FontSize = yRes / 45,
                                    Width = o.Width - 10,
                                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                                    TextAlignment = TextAlignment.Center
                                }
                            });
                            o.ValueBlock.Inlines.Add(new Run("") { FontSize = yRes / 45 });
                        }
                    }
                    catch { }
                }, index);

                if (o.AllMenus[CurrentMenu].IndexOf(item) != o.AllMenus[CurrentMenu].Count - 1)
                {
                    o.Dispatcher.BeginInvoke((Action)delegate
                    {
                        o.OptionsBlock.Inlines.Add("\n"); 
                        o.ValueBlock.Inlines.Add("\n");
                    });
                }
                index++;
            }
        }
    }

    // Handles the input
    public void KeyHandler(CancellationToken ct)
    {
        while (true)
        {
            Task.Delay(10, ct).Wait(ct);
            if (ct.IsCancellationRequested)
            {
                return;
            }

            if (_hidden)
            {
                continue;
            }

            var isGameFocused = Mw.Gvp.Process.MainWindowHandle == GetForegroundWindow();

            if (!isGameFocused)
            {
                continue;
            }
            
            if (GetAsyncKeyState(Down) is 1 or short.MinValue && !_downKeyDown )
            {
                _downKeyDown = true;
            }

            if (GetAsyncKeyState(Down) is not 1 and not short.MinValue && _downKeyDown )
            {
                _downKeyDown = false;
            }

            if (GetAsyncKeyState(Up) is 1 or short.MinValue && !_upKeyDown )
            {
                _upKeyDown = true;
            }

            if (GetAsyncKeyState(Up) is not 1 and not short.MinValue && _upKeyDown )
            {
                _upKeyDown = false;
            }

            if (GetAsyncKeyState(Left) is 1 or short.MinValue && !_leftKeyDown )
            {
                _leftKeyDown = true;
            }

            if (GetAsyncKeyState(Left) is not 1 and not short.MinValue && _leftKeyDown )
            {
                _leftKeyDown = false;
            }

            if (GetAsyncKeyState(Right) is 1 or short.MinValue && !_rightKeyDown )
            {
                _rightKeyDown = true;
            }

            if (GetAsyncKeyState(Right) is not 1 and not short.MinValue && _rightKeyDown )
            {
                _rightKeyDown = false;
            }
            
            var currentOption = o.AllMenus[CurrentMenu][_selectedOptionIndex];
            
            if (GetAsyncKeyState(Confirm) is 1 or short.MinValue && currentOption.IsEnabled)
            {
                switch (currentOption.Type)
                {
                    case OptionType.MenuButton:
                    {
                        _levelIndex++;
                        
                        var nameSplit = currentOption.Name.Split(' ', '/', '[', ']', '&');
                        CurrentMenu = string.Empty;
                            
                        foreach (var item in nameSplit)
                        {
                            CurrentMenu += char.ToUpper(item[0]) + item[1..];
                        }

                        CurrentMenu += "Options";
                        _history.Add(_levelIndex, CurrentMenu);
                        _selectedOptionIndex = 0;
                        break;
                    }
                    case OptionType.Bool:
                    {
                        currentOption.Value = !(bool)currentOption.Value;
                        break;
                    }
                    case OptionType.Button:
                    {
                        ((Action)currentOption.Value)();
                        break;
                    }
                }

                while (GetAsyncKeyState(Confirm) is 1 or short.MinValue)
                {
                    Task.Delay(10, ct).Wait(ct);
                }
            }
            else if (GetAsyncKeyState(Leave) is 1 or short.MinValue)
            {
                if (_levelIndex == 0)
                {
                    o.Dispatcher.Invoke(delegate { o.Hide(); });
                    _hidden = true;
                    continue;
                }
                    
                _levelIndex--;
                CurrentMenu = _history[_levelIndex];
                _history.Remove(_levelIndex + 1);
                _selectedOptionIndex = 0;
                while (GetAsyncKeyState(Leave) is 1 or short.MinValue)
                {
                    Task.Delay(10, ct).Wait(ct);
                }
            }
            else if (GetAsyncKeyState(OverlayVisibility) is 1 or short.MinValue)
            {
                    
                if (o.Visibility == Visibility.Visible)
                {
                    o.Dispatcher.Invoke(delegate { o.Hide(); });
                }
                else
                {
                    o.Dispatcher.Invoke(delegate { o.Show(); });
                }

                _hidden = !_hidden;
                while (GetAsyncKeyState(OverlayVisibility) is 1 or short.MinValue)
                {
                    Task.Delay(10, ct).Wait(ct);
                }
            }
        }
    }
    public void ChangeSelection(CancellationToken ct)
    {
        while (true)
        {
            Task.Delay(10, ct).Wait(ct);
                
            if (ct.IsCancellationRequested)
            {
                break;
            }

            if (_hidden)
            {
                continue;
            }
            
            var isGameFocused = Mw.Gvp.Process.MainWindowHandle == GetForegroundWindow();

            if (!isGameFocused)
            {
                continue;
            }
            
            if (_downKeyDown && isGameFocused)
            {
                void Down()
                {
                    _selectedOptionIndex++;
                    if (_selectedOptionIndex > o.AllMenus[CurrentMenu].Count - 1)
                        _selectedOptionIndex = 0;
                }
                Down();

                var timer = new Timer();
                timer.Interval = 150;
                timer.Tick += delegate
                {
                    Down();
                    Task.Delay(5, ct).Wait(ct);
                };
                
                o.Dispatcher.Invoke(delegate
                {
                    timer.Start();
                });
                
                while (_downKeyDown)
                {
                    Task.Delay(1, ct).Wait(ct);
                }
                
                o.Dispatcher.Invoke(delegate
                {
                    timer.Dispose();
                });
            }
            else if (_upKeyDown && isGameFocused)
            {
                void Up()
                {
                    _selectedOptionIndex--;
                    if (_selectedOptionIndex < 0)
                        _selectedOptionIndex = o.AllMenus[CurrentMenu].Count - 1;
                }
                Up();

                var timer = new Timer();
                timer.Interval = 150;
                timer.Tick += delegate
                { 
                    Up();
                    Task.Delay(5, ct).Wait(ct);
                };
                
                o.Dispatcher.Invoke(delegate
                {
                    timer.Start();
                });
                
                while (_upKeyDown)
                {
                    Task.Delay(1, ct).Wait(ct);
                }
                
                o.Dispatcher.Invoke(delegate
                {
                    timer.Dispose();
                });
            }
        }
    }

    public void ChangeValue(CancellationToken ct)
    {
        while (true)
        {
            Task.Delay(50, ct).Wait(ct);
                
            if (ct.IsCancellationRequested)
            {
                return;
            }

            var isGameFocused = Mw.Gvp.Process.MainWindowHandle == GetForegroundWindow();

            var currentOption = o.AllMenus[CurrentMenu][_selectedOptionIndex];
            
            if (_rightKeyDown && currentOption.IsEnabled && isGameFocused)
            {
                currentOption.Value = currentOption.Type switch
                {
                    OptionType.Float => Convert.ToSingle(Math.Round((float)currentOption.Value + 0.1f, 1)),
                    OptionType.Int => (int)currentOption.Value + 1,
                    _ => currentOption.Value
                };
                    
            }
            else if (_leftKeyDown && currentOption.IsEnabled && isGameFocused)
            {
                currentOption.Value = currentOption.Type switch
                {
                    OptionType.Float => Convert.ToSingle(Math.Round((float)currentOption.Value - 0.1f, 1)), 
                    OptionType.Int => (int)currentOption.Value - 1,
                    _ => currentOption.Value
                };
            }

        }
    }

    // Configs and enables blur
    internal static void EnableBlur()
    {
        var windowHelper = new WindowInteropHelper(o);

        var accent = new AccentPolicy
        {
            AccentState = AccentState.AccentEnableBlurbehind
        };

        var accentStructSize = Marshal.SizeOf(accent);

        var accentPtr = Marshal.AllocHGlobal(accentStructSize);
        Marshal.StructureToPtr(accent, accentPtr, false);

        var data = new WindowCompositionAttributeData
        {
            Attribute = WindowCompositionAttribute.WcaAccentPolicy,
            SizeOfData = accentStructSize,
            Data = accentPtr
        };

        SetWindowCompositionAttribute(windowHelper.Handle, ref data);
            
        Marshal.FreeHGlobal(accentPtr);
    }
}