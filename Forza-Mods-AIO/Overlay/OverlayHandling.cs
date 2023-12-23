using System;
using IniParser;
using System.IO;
using System.Linq;
using System.Windows;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Interop;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using Forza_Mods_AIO.Resources;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Media.Animation;
using Forza_Mods_AIO.Overlay.Menus.SettingsMenu;
using Forza_Mods_AIO.Overlay.Options;
using static System.Convert;
using static System.Environment;
using static System.Math;
using static System.Windows.Visibility;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Overlay.Overlay;
using static System.Windows.HorizontalAlignment;
using static Forza_Mods_AIO.Resources.DllImports;
using Application = System.Windows.Application;
using Brush = System.Windows.Media.Brush;
using Timer = System.Windows.Forms.Timer;
using Brushes = System.Windows.Media.Brushes;
using TextAlignment = System.Windows.TextAlignment;

namespace Forza_Mods_AIO.Overlay;

public partial class OverlayHandling
{
    #region Blur DLLImports
    //Credits to Rafael Rivera for the blur https://github.com/riverar/sample-win32-acrylicblur
    internal enum AccentState
    {
        AccentEnableBlurBehind = 3
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
    public static Keys RapidAdjust = Keys.Alt;
    public static Keys OverlayVisibility = Keys.Subtract;

    public static string ControllerUp = "DPadUp";
    public static string ControllerDown = "DPadDown";
    public static string ControllerLeft = "DPadLeft";
    public static string ControllerRight = "DPadRight";
    public static string ControllerConfirm = "LeftThumb";
    public static string ControllerLeave = "RightThumb";
    public static string ControllerRapidAdjust = "LeftShoulder";
    public static string ControllerOverlayVisibility = "RightShoulder";
    
    // Menu operational vars
    private string[] _menuHeaders = null!;
    private int _selectedOptionIndex, _levelIndex;
    private string _currentMenu = "MainOptions";
    private bool _hidden;
    public float FontSize = 5;
    public string FontWeight = "Normal";
    public string FontStyle = "Normal";
    public int XOffset = 0, YOffset = 0;
    
    private readonly string _settingsFilePath = GetFolderPath(SpecialFolder.MyDocuments) + @"\Forza Mods AIO\Overlay Settings.ini";
    
    private readonly Dictionary<int, string> _history = new()
    {
        {  0 ,"MainOptions" }
    };

    // Key vars
    private bool _upKeyDown,
        _downKeyDown,
        _leftKeyDown,
        _rightKeyDown,
        _rapidKeyDown;
    
    private bool _controllerUpKeyDown,
        _controllerDownKeyDown,
        _controllerLeftKeyDown,
        _controllerRightKeyDown,
        _controllerRapidKeyDown,
        _controllerLeaveKeyDown,
        _controllerConfirmKeyDown,
        _controllerVisibilityKeyDown;

    //Theme vars
    public Brush MainBackColour = Brushes.Transparent;
    public Brush DescriptionBackColour = Brushes.Transparent;
    public Brush MainBorderColour = Brushes.Black;
    public Brush DescriptionBorderColour = Brushes.Black;
    public int HeaderIndex = 0;
    public readonly List<object[]> Headers = new();
    private BitmapImage? _headerImage = null!;
    #endregion
    // Caches all the headers
    public void CacheHeaders()
    {
        if (!Directory.Exists(CurrentDirectory + @"\Overlay\Headers"))
        {
            return;
        }

        _menuHeaders = Directory.GetFiles(CurrentDirectory + @"\Overlay\Headers");
        foreach (var header in _menuHeaders)
        {
            var inCachedBitmaps = Headers.Any(item => item[0].ToString()!.Contains(header.Split('\\').Last().Split('.').First()));
            if (inCachedBitmaps) continue;
            Headers.Add(new object[] { header.Split('\\').Last().Split('.').First(), new BitmapImage(new Uri(header)) });
        }
    }
        
    public void LoadSettings()
    {
        if (!File.Exists(_settingsFilePath))
        {
            return;
        }

        var parser = new FileIniDataParser();
        var iniData = parser.ReadFile(_settingsFilePath);

        foreach (var menuOption in typeof(SettingsMenu)
                     .GetFields(BindingFlags.Public | BindingFlags.Static)
                     .Where(f => f.FieldType == typeof(FloatOption) || 
                                 f.FieldType == typeof (SelectionOption) ||
                                 f.FieldType == typeof (IntOption)))
        {
            var name = menuOption.Name;
            var mainQuery = name.Contains("Font") ? "Font" : name.Contains("Main") ? "Main" : "Description";
            var value = iniData[mainQuery][name];
            
            ParseAndSetValueBasedOnTheType((MenuOption?)menuOption.GetValue(menuOption), value);
        }
    }

    private static void ParseAndSetValueBasedOnTheType(MenuOption? menuOption, string? value)
    {
        if (menuOption == null)
        {
            return;
        }
        
        switch (menuOption)
        {
            case FloatOption floatOption:
            {
                if (!float.TryParse(value, out var result)) return;
                floatOption.Value = result;
                break;
            }
            case SelectionOption selectionOption:
            {
                if (!int.TryParse(value, out var result)) return;
                selectionOption.Index = result;                
                break;   
            }
            case IntOption intOption:
            {
                if (!int.TryParse(value, out var result)) return;
                intOption.Value = result;
                break;
            }
        }
    }
    
    public void SaveSettings()
    {
        if (!File.Exists(_settingsFilePath))
        {
            using (File.Create(_settingsFilePath)) ;
        }

        var parser = new FileIniDataParser();
        var iniData = parser.ReadFile(_settingsFilePath);

        foreach (var menuOption in typeof(SettingsMenu)
                     .GetFields(BindingFlags.Public | BindingFlags.Static)
                     .Where(f => f.FieldType == typeof(FloatOption) || 
                                 f.FieldType == typeof (SelectionOption) ||
                                 f.FieldType == typeof (IntOption)))
        {
            var mainQuery = menuOption.Name.Contains("Main") ? "Main" : "Description";

            if (menuOption.Name.Contains("Font"))
            {
                mainQuery = "Font";
            }
                    
            var name = menuOption.GetType().GetProperty("Name")?.GetValue(menuOption)?.ToString();
            var valueProperty = menuOption.FieldType.GetProperty("Value");
            var indexProperty = menuOption.FieldType.GetProperty("Index");
            var value = indexProperty?.GetValue(menuOption.GetValue(null)) ??
                        valueProperty?.GetValue(menuOption.GetValue(null));
            iniData[mainQuery][name] = value?.ToString();
        }
            
        parser.WriteFile(_settingsFilePath, iniData);
    }
        
    // Handles the position of the overlay
    public void OverlayPosAndScale(CancellationToken ct)
    {
        CacheHeaders();
        LoadSettings();

        while (true)
        {
            Task.Delay(10, ct).Wait(ct);
            if (ct.IsCancellationRequested)
            {
                return;
            }

            if (Mw.Gvp.Process == null)
            {
                continue;
            }

            var gameWindow = new DllImports.Rect();

            GetWindowRect(Mw.Gvp.Process.MainWindowHandle, ref gameWindow);
            GetClientRect(Mw.Gvp.Process.MainWindowHandle, out var gameClientWindow);

            var offset = gameClientWindow.Bottom / 20d;
            var posLeft = gameWindow.Left + (gameWindow.Right - gameWindow.Left - gameClientWindow.Right) / 2d + offset;
            var posTop = gameWindow.Top + (gameWindow.Bottom - gameWindow.Top - gameClientWindow.Bottom) / 1.5d + offset;
            
            // top right
            //var xOffset = ForzaClientWindow.Bottom / 2.5d;
            //var PosLeft = ForzaWindow.Right - (ForzaWindow.Right - ForzaWindow.Left - ForzaClientWindow.Right) - xOffset;
            
            var yRes = gameClientWindow.Bottom - (gameWindow.Bottom - gameWindow.Top - gameClientWindow.Bottom) / 1.3;
            double headerY = yRes / 10.8d, headerX = headerY * 4;

            SelectHeader();

            if (!IsGameFocused())
            {
                O?.Dispatcher.Invoke(() => O.Hide());
                continue;
            }

            SetWindowAttributes(posTop, posLeft, headerY, headerX);
        }
    }

    private void SetWindowAttributes(double posTop, double posLeft, double headerY, double headerX)
    {
        O.Dispatcher.Invoke(() =>
        {
            O.Top = posTop;
            O.Left = posLeft;

            HandleFontSettings();
                    
            O.Width = headerX;
            O.TopSection.Height = new GridLength(headerY);

            O.Header.Width = O.Width;
            O.Header.Height = O.TopSection.ActualHeight;
            
            if (O.OptionsBlock.Inlines.Count == O.AllMenus[_currentMenu].Count * 2 - 1)
            {
                O.MainSection.Height = new GridLength(O.OptionsBlock.ActualHeight + 10);
                O.DescriptionSection.Height = O.DescriptionBlock.Text != string.Empty 
                    ? new GridLength(O.DescriptionBlock.ActualHeight + 15)
                    : new GridLength(0);

                O.Height = O.TopSection.ActualHeight + O.MainSection.ActualHeight + O.DescriptionSection.ActualHeight;
            }
            
            O.Header.Source = _headerImage;
                    
            O.MainBorder.Background = MainBackColour;
            O.MainBorder.BorderBrush = MainBorderColour;

            O.DescriptionBorder.Background = DescriptionBackColour;
            O.DescriptionBorder.BorderBrush = DescriptionBorderColour;

            if (O.Visibility != Hidden || _hidden) return Task.CompletedTask;
                
            O.Show();
            return Task.CompletedTask;
        });
    }

    private void SelectHeader()
    {
        if (!Directory.Exists(CurrentDirectory + @"\Overlay\Headers") || _menuHeaders.Length == 0)
        {
            if (_headerImage is { IsFrozen: true })
            {
                _headerImage = _headerImage.Clone();
            }
            _headerImage = new BitmapImage(new Uri("pack://application:,,,/Overlay/Headers/pog header.png", UriKind.RelativeOrAbsolute));
            _headerImage.Dispatcher.Invoke(() => _headerImage.Freeze());
        }
        else if (_headerImage?.UriSource.LocalPath != _menuHeaders[HeaderIndex])
        {
            if (_headerImage is { IsFrozen: true })
            {
                _headerImage = _headerImage.Clone();
            }
            _headerImage = (BitmapImage)Headers.Find(x => x[0].ToString().Contains(_menuHeaders[HeaderIndex].Split('\\').Last().Split('.').First()))?[1];
            _headerImage?.Dispatcher.Invoke(() => _headerImage.Freeze());
        }
    }

    private void HandleFontSettings()
    {
        HandleFontWeight();
        HandleFontStyle();
    }

    private void HandleFontWeight()
    {
        foreach (var field in typeof(FontWeights).GetProperties(BindingFlags.Public | BindingFlags.Static))
        {
            if (field.PropertyType != typeof(FontWeight))
            {
                continue;
            }
    
            var fontWeight = (FontWeight)(field.GetValue(null) ?? FontWeights.Normal);
            var cleanFontWeight = FontWeight.Replace(" ", "");
    
            if (!string.Equals(fontWeight.ToString(), cleanFontWeight, StringComparison.CurrentCultureIgnoreCase))
            {
                continue;
            }
            O.FontWeight = fontWeight;
            break;
        }
    }

    private void HandleFontStyle()
    {
        foreach (var field in typeof(FontStyles).GetProperties(BindingFlags.Public | BindingFlags.Static))
        {
            if (field.PropertyType != typeof(FontStyle))
            {
                continue;
            }
            var fontStyle = (FontStyle)(field.GetValue(null) ?? FontStyles.Normal);
    
            if (!string.Equals(fontStyle.ToString(), FontStyle, StringComparison.CurrentCultureIgnoreCase))
            {
                continue;
            }
            O.FontStyle = fontStyle;
            break;
        }
    }

    public void UpdateMenuOptions(CancellationToken ct)
    {
        while (true)
        {
            Task.Delay(10, ct).Wait(ct);
            
            if (ct.IsCancellationRequested)
            {
                return;
            }

            ClearMenuAndHighlightOption();
            AddAllMenuOptions();
        }
    }

    private void ClearMenuAndHighlightOption()
    {
        O.Dispatcher.BeginInvoke((Action)delegate
        {
            O.OptionsBlock.Inlines.Clear(); 
            O.ValueBlock.Inlines.Clear();
        });

        O.Dispatcher.Invoke((Action)delegate
        {
            if (O.OptionsBlock.Inlines.Count <= 1) return;
            foreach (UIElement child in O.Layout.Children)
            {
                if ((string)child.GetType().GetProperty("Name")?.GetValue(child)! != "Highlight")
                {   
                    continue;
                }

                O.Layout.Children.Remove(child);
                break;
            }
            
            var height = (float)(O.OptionsBlock.ActualHeight / O.AllMenus[_currentMenu].Count * _selectedOptionIndex + 5);
            var highlighted = new Border
            {
                Name = "Highlight",
                VerticalAlignment = VerticalAlignment.Top,
                Background = Brushes.Black,
                Width = O.Layout.ActualWidth,
                Height = O.OptionsBlock.ActualHeight / O.AllMenus[_currentMenu].Count,
                Margin = new Thickness(0, height, 0, 0)
            };
            Grid.SetColumn(highlighted, 0);
            Grid.SetRow(highlighted, 1);

            System.Windows.Controls.Panel.SetZIndex(highlighted, 1);
            O.Layout.Children.Add(highlighted);
        });
    }
    
    private void AddAllMenuOptions()
    {
        if (Mw.Gvp.Process == null)
        {
            return;
        }
        
        var gameWindow = new DllImports.Rect();
        GetWindowRect(Mw.Gvp.Process.MainWindowHandle, ref gameWindow);
        GetClientRect(Mw.Gvp.Process.MainWindowHandle, out var gameClientWindow);
        var yRes = gameClientWindow.Bottom - (gameWindow.Bottom - gameWindow.Top - gameClientWindow.Bottom) / 1.3;
        var index = 0;
        
        foreach (var item in O.AllMenus[_currentMenu])
        {
            string? text, value = string.Empty, description = string.Empty;
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

            value = item switch
            {
                MenuButtonOption => ">",
                IntOption intOption => $"<{intOption.Value}>",
                FloatOption floatOption => $"<{floatOption.Value}>",
                SelectionOption selectionOption => $"<{selectionOption.Selections[selectionOption.Index]}>",
                ToggleOption toggleOption => toggleOption.IsOn ? "[X]" : "[ ]",
                _ => value
            };

            AddMenuOption(item, index, text, description, value, fColour, yRes);
            index++;
        }
    }

    private void AddMenuOption(
        MenuOption item,
        int index,
        string text,
        string? description,
        string value,
        Brush fColour,
        double yRes)
    {
        O.Dispatcher.BeginInvoke((Action<int>)delegate (int idx)
        {
            if (item is SubHeaderOption)
            {
                var child = new TextBlock
                {
                    Text = text,
                    Foreground = fColour,
                    FontSize = yRes / (5d / FontSize * 45d),
                    Width = O.Width - 10,
                    HorizontalAlignment = Center,
                    TextAlignment = TextAlignment.Center
                };
                    
                O.OptionsBlock.Inlines.Add(new InlineUIContainer(child));
                O.ValueBlock.Inlines.Add(new Run("") { FontSize = yRes / (5d / FontSize * 45d) });
                return;
            }

            O.OptionsBlock.Inlines.Add(new Run(text)
            {
                Foreground = fColour,
                FontSize = yRes / (5d / FontSize * 45d)
            });

            O.ValueBlock.Inlines.Add(new Run(value)
            {
                Foreground = fColour,
                FontSize = yRes / (5d / FontSize * 45d)
            });

            if (description != string.Empty && idx == _selectedOptionIndex)
            {
                O.DescriptionBlock.Text = description;
                O.DescriptionBlock.FontSize = yRes / (5d / FontSize * 45d);
                O.DescriptionBlock.Foreground = Brushes.White;
                return;
            }

            if (idx != _selectedOptionIndex) return;
            O.DescriptionBlock.Text = string.Empty;
        }, index);

        if (O.AllMenus[_currentMenu].IndexOf(item) == O.AllMenus[_currentMenu].Count - 1) return;
        
        O.Dispatcher.BeginInvoke((Action)delegate
        {
            O.OptionsBlock.Inlines.Add("\n"); 
            O.ValueBlock.Inlines.Add("\n");
        });
    }

    private static bool IsGameFocused()
    {
        if (Mw.Gvp.Process == null)
        {
            return false;
        }
        
        return Mw.Gvp.Process.MainWindowHandle == GetForegroundWindow();
    }
    
    public void KeyHandler(CancellationToken ct)
    {
        while (true)
        {
            Task.Delay(10, ct).Wait(ct);
            if (ct.IsCancellationRequested)
            {
                return;
            }

            if (!IsGameFocused())
            {
                continue;
            }

            UpdateKeyStates();
            HandleKeyEvents();
        }
    }
    
    public void ControllerKeyHandler(CancellationToken ct)
    {
        while (true)
        {
            Task.Delay(10, ct).Wait(ct);
            if (ct.IsCancellationRequested)
            {
                return;
            }

            if (!IsGameFocused())
            {
                continue;
            }

            UpdateControllerStates();
            HandleControllerEvents();
        }
    }

    private void HandleKeyEvents()
    {
        var currentOption = O.AllMenus[_currentMenu][_selectedOptionIndex];
        if (IsKeyPressed(Confirm) && currentOption.IsEnabled)
        {
            HandleConfirmPress(currentOption);
            while (IsKeyPressed(Confirm))
            {
                Task.Delay(10).Wait();
            }
        }
        else if (IsKeyPressed(Leave))
        {
            HandleLeavePress();
            while (IsKeyPressed(Leave))
            {
                Task.Delay(10).Wait();
            }
        }
        else if (IsKeyPressed(OverlayVisibility))
        {
            HandleVisibilityPress();
            while (IsKeyPressed(OverlayVisibility))
            {
                Task.Delay(10).Wait();
            }
        }
    }

    
    private void HandleControllerEvents()
    {
        var currentOption = O.AllMenus[_currentMenu][_selectedOptionIndex];
        if (_controllerConfirmKeyDown && currentOption.IsEnabled)
        {
            HandleConfirmPress(currentOption);
            while (true)
            {
                Task.Delay(10).Wait();
                UpdateControllerStates();
                if (_controllerConfirmKeyDown) continue;
                break;
            }
        }
        else if (_controllerLeaveKeyDown)
        {
            HandleLeavePress();
            while (true)
            {
                Task.Delay(10).Wait();
                UpdateControllerStates();
                if (_controllerLeaveKeyDown) continue;
                break;
            }
        }
        else if (_controllerVisibilityKeyDown)
        {
            HandleVisibilityPress();
            while (true)
            {
                Task.Delay(10).Wait();
                UpdateControllerStates();
                if (_controllerVisibilityKeyDown) continue;
                break;
            }
        }
    }
    private void HandleConfirmPress(MenuOption currentOption)
    {
        switch (currentOption)
        {
            case MenuButtonOption menuButtonOption:
            {
                _levelIndex++;
                var nameSplit = currentOption.Name.Split(' ', '/', '[', ']', '&');
                _currentMenu = string.Concat(nameSplit.Select(item => char.ToUpper(item[0]) + item[1..])) + "Options";
                _history.Add(_levelIndex, _currentMenu);
                _selectedOptionIndex = 0;
                if (menuButtonOption.Action == null) return;
                Application.Current.Dispatcher.BeginInvoke(() => menuButtonOption.Action());
                break;
            }
            case ToggleOption toggleOption:
            {
                toggleOption.IsOn = !toggleOption.IsOn;
                break;
            }
            case ButtonOption buttonOption:
            {
                Application.Current.Dispatcher.BeginInvoke(() => buttonOption.Action());
                break;
            }
        }
    }

    private void HandleLeavePress()
    {
        if (_levelIndex == 0)
        {
            return;
        }

        _levelIndex--;
        _currentMenu = _history[_levelIndex];
        _history.Remove(_levelIndex + 1);
        _selectedOptionIndex = 0;
    }

    private void HandleVisibilityPress()
    {
        if (O.Visibility == Visible)
        {
            O.Dispatcher.Invoke(delegate { O.Hide(); });
        }
        else
        {
            O.Dispatcher.Invoke(delegate { O.Show(); });
        }

        _hidden = !_hidden;
    }
    private static void UpdateKeyState(Keys key, ref bool keyDownBool)
    {
        keyDownBool = IsKeyPressed(key) switch
        {
            true when !keyDownBool => true,
            false when keyDownBool => false,
            _ => keyDownBool
        };
    }

    private static bool IsKeyPressed(Keys key)
    {
        return (GetAsyncKeyState(key) & (1 | short.MinValue)) != 0;
    }
    
    private void UpdateKeyStates()
    {
        UpdateKeyState(Down, ref _downKeyDown);
        UpdateKeyState(Up, ref _upKeyDown);
        UpdateKeyState(Left, ref _leftKeyDown);
        UpdateKeyState(Right, ref _rightKeyDown);
        UpdateKeyState(RapidAdjust, ref _rapidKeyDown);
    }
    
    private void UpdateControllerStates()
    {
        Mw.Gamepad.InitializeControllersAndInput();
        
        if (!Mw.Gamepad.IsControllerConnected)
        {
            return;
        }

        _controllerDownKeyDown = Mw.Gamepad.IsButtonPressed(ControllerDown);
        _controllerUpKeyDown = Mw.Gamepad.IsButtonPressed(ControllerUp);
        _controllerLeftKeyDown = Mw.Gamepad.IsButtonPressed(ControllerLeft);
        _controllerRightKeyDown = Mw.Gamepad.IsButtonPressed(ControllerRight);
        _controllerRapidKeyDown = Mw.Gamepad.IsButtonPressed(ControllerRapidAdjust);
        _controllerConfirmKeyDown = Mw.Gamepad.IsButtonPressed(ControllerConfirm);
        _controllerLeaveKeyDown = Mw.Gamepad.IsButtonPressed(ControllerLeave);
        _controllerVisibilityKeyDown = Mw.Gamepad.IsButtonPressed(ControllerOverlayVisibility);
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

            if (_hidden || !IsGameFocused())
            {
                continue;
            }
            
            if (_downKeyDown || _controllerDownKeyDown)
            {
                HandleDownNavigation(ct);    
            }
            else if (_upKeyDown || _controllerUpKeyDown)
            {
                HandleUpNavigation(ct);
            }
        }
    }

    private void HandleDownNavigation(CancellationToken ct)
    {
        NavigateDown();

        var timer = new Timer();
        timer.Interval = 150;
        timer.Tick += delegate
        {
            NavigateDown();
            Task.Delay(5, ct).Wait(ct);
        };
                
        O.Dispatcher.Invoke(delegate
        {
            timer.Start();
        });
                
        while (_downKeyDown || _controllerDownKeyDown)
        {
            Task.Delay(1, ct).Wait(ct);
        }
                
        O.Dispatcher.Invoke(delegate
        {
            timer.Dispose();
        });
    }
    
    private void NavigateDown()
    {
        _selectedOptionIndex++;
        if (_selectedOptionIndex <= O.AllMenus[_currentMenu].Count - 1) return;
        _selectedOptionIndex = 0;
    }
    
    private void HandleUpNavigation(CancellationToken ct)
    {
        NavigateUp();
        
        var timer = new Timer();
        timer.Interval = 150;
        timer.Tick += delegate
        { 
            NavigateUp();
            Task.Delay(5, ct).Wait(ct);
        };
                
        O.Dispatcher.Invoke(delegate
        {
            timer.Start();
        });
                
        while (_upKeyDown || _controllerUpKeyDown)
        {
            Task.Delay(1, ct).Wait(ct);
        }
                
        O.Dispatcher.Invoke(delegate
        {
            timer.Dispose();
        });
    }
    
    
    private void NavigateUp()
    {
        _selectedOptionIndex--;
        if (_selectedOptionIndex >= 0) return;
        _selectedOptionIndex = O.AllMenus[_currentMenu].Count - 1;
    }
    
    public void ChangeValue(CancellationToken ct)
    {
        while (true)
        {
            Task.Delay(_rapidKeyDown || _controllerRapidKeyDown ? 5 : 100, ct).Wait(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }
            
            var currentOption = O.AllMenus[_currentMenu][_selectedOptionIndex];
            if (!IsGameFocused() || !currentOption.IsEnabled)
            {
                continue;
            }
            
            if (_rightKeyDown || _controllerRightKeyDown)
            {
                IncreaseValue(currentOption);
            }
            else if (_leftKeyDown || _controllerLeftKeyDown)
            {
                DecreaseValue(currentOption);
            }
        }
    }

    private static void IncreaseValue(MenuOption currentOption)
    {
        switch (currentOption)
        {
            case FloatOption floatOption when floatOption.Minimum != float.MinValue &&
                                              floatOption.Maximum != float.MaxValue &&
                                              floatOption.Value >= floatOption.Maximum:
            {
                floatOption.Value = floatOption.Minimum;
                break;
            }
            case IntOption intOption when intOption.Minimum != int.MinValue &&
                                          intOption.Maximum != int.MaxValue &&
                                          intOption.Value >= intOption.Maximum:
            {
                intOption.Value = intOption.Minimum;
                break;
            }
            case FloatOption floatOption when floatOption.Minimum != float.MinValue &&
                                              floatOption.Value >= floatOption.Maximum:
            {
                floatOption.Value = floatOption.Maximum;
                break;
            }
            case IntOption intOption when intOption.Minimum != int.MinValue &&
                                          intOption.Value >= intOption.Maximum:
            {
                intOption.Value = intOption.Maximum;
                break;
            }
            case FloatOption floatOption:
            {
                floatOption.Value = ToSingle(Round(floatOption.Value + 0.1f, 1));
                break;
            }
            case IntOption intOption:
            {
                intOption.Value += 1;
                break;
            }
            case SelectionOption selectionOption when selectionOption.Index >= selectionOption.Selections.Length - 1:
            {
                selectionOption.Index = 0;
                break;
            }
            case SelectionOption selectionOption:
            {
                selectionOption.Index += 1;
                break;
            }
            case ToggleOption toggleOption:
            {
                toggleOption.IsOn = true;
                break;
            }
        }
    }

    private static void DecreaseValue(MenuOption currentOption)
    {
        switch (currentOption)
        {
            case FloatOption floatOption when floatOption.Minimum != float.MinValue &&
                                              floatOption.Maximum != float.MaxValue &&
                                              floatOption.Value <= floatOption.Minimum:
            {
                floatOption.Value = floatOption.Maximum;
                break;
            }
            case IntOption intOption when intOption.Minimum != int.MinValue &&
                                          intOption.Maximum != int.MaxValue &&
                                          intOption.Value <= intOption.Minimum:
            {
                intOption.Value = intOption.Maximum;
                break;
            }
            case FloatOption floatOption when floatOption.Minimum != float.MinValue &&
                                              floatOption.Value <= floatOption.Minimum:
            {
                floatOption.Value = floatOption.Maximum;
                break;
            }
            case IntOption intOption when intOption.Minimum != int.MinValue &&
                                          intOption.Value <= intOption.Minimum:
            {
                intOption.Value = intOption.Maximum;
                break;
            }
            case FloatOption floatOption:
            {
                floatOption.Value = ToSingle(Round(floatOption.Value - 0.1f, 1));
                break;
            }
            case IntOption intOption:
            {
                intOption.Value -= 1;
                break;
            }
            case SelectionOption { Index: <= 0 } selectionOption:
            {
                selectionOption.Index = selectionOption.Selections.Length - 1;
                break;
            }
            case SelectionOption selectionOption:
            {
                selectionOption.Index -= 1;
                break;
            }
            case ToggleOption toggleOption:
            {
                toggleOption.IsOn = false;
                break;
            }
        }
    }

    internal static void EnableBlur()
    {
        var windowHelper = new WindowInteropHelper(O);
        var accent = new AccentPolicy
        {
            AccentState = AccentState.AccentEnableBlurBehind
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