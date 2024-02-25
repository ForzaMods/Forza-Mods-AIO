using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Forza_Mods_AIO.Models;
using Forza_Mods_AIO.Views.Pages;
using MahApps.Metro.Controls;
using Memory;
using static System.Diagnostics.FileVersionInfo;
using static System.IO.Path;

namespace Forza_Mods_AIO.ViewModels.Windows;

public partial class MainWindowViewModel : ObservableObject
{
    private bool _isInitialized;
    
    #region Constants

    private const string NotAttachedText = "Launch FH4, FH5 or FM8";
    private const double WindowCornerRadiusSize = 10;

    #endregion
    
    #region Misc Vars

    private static double BottomButtonsMarginExpression =>
        WindowCornerRadiusSize * 1.25 + WindowCornerRadiusSize * 1.25 / (WindowCornerRadiusSize / 2);
    
    [ObservableProperty]
    private string _applicationTitle = string.Empty;
    
    [ObservableProperty]
    private string _attachedText = string.Empty;

    [ObservableProperty]
    private object _currentView = new();
    
    [ObservableProperty]
    private bool _attached;
    
    [ObservableProperty]
    private CornerRadius _windowCornerRadius = new(WindowCornerRadiusSize);
    
    [ObservableProperty]
    private CornerRadius _topBarCornerRadius = new(WindowCornerRadiusSize, WindowCornerRadiusSize, 0, 0);

    [ObservableProperty]
    private CornerRadius _sideBarCornerRadius = new(0, 0, 0, WindowCornerRadiusSize);

    [ObservableProperty]
    private Thickness _bottomButtonsMargin = new(0, 0, 0, BottomButtonsMarginExpression);
    
    [ObservableProperty]
    private Mem _mem = new();
    
    [ObservableProperty]
    private GameVerPlat _gameVerPlat = new();

    #endregion

    #region Search

    [ObservableProperty]
    private Visibility _searchVisibility = Visibility.Collapsed;

    [ObservableProperty]
    private double _searchOpacity;
    
    [ObservableProperty]
    private ObservableCollection<SearchResult> _searchResults = [];
    
    #endregion
    
    public MainWindowViewModel()
    {
        if (_isInitialized) return;
        InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        ApplicationTitle = "Forza Mods AIO";
        AttachedText = NotAttachedText;

        CurrentView = Resources.Pages.GetPage(typeof(AioInfo));

#if DEBUG
        Attached = true;        
#else
        Task.Run(Attach);
#endif
        
        _isInitialized = true;
    }
    
    [RelayCommand]
    private void HandleMaximizeMinimize(object mainWindow)
    {
        if (mainWindow is not MetroWindow metroWindow)
        {
            return;
        }
        
        switch (metroWindow.WindowState)
        {
            case WindowState.Maximized:
            {
                metroWindow.WindowState = WindowState.Normal;
                WindowCornerRadius = new CornerRadius(WindowCornerRadiusSize);
                SideBarCornerRadius = new CornerRadius(0, 0, 0, WindowCornerRadiusSize);
                TopBarCornerRadius = new CornerRadius(WindowCornerRadiusSize, WindowCornerRadiusSize, 0, 0);
                BottomButtonsMargin = new Thickness(0, 0, 0, BottomButtonsMarginExpression);
                break;
            }
            case WindowState.Normal:
            {
                metroWindow.WindowState = WindowState.Maximized;
                WindowCornerRadius = new CornerRadius(0);
                SideBarCornerRadius = new CornerRadius(0);
                TopBarCornerRadius = new CornerRadius(0);
                BottomButtonsMargin = new Thickness(0);
                break;
            }
            case WindowState.Minimized:
            default:
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
    
    private void Attach()
    {
        var gamesDictionary = new Dictionary<string, string>
        {
            { "Forza Horizon 4", "forzahorizon4" },
            { "Forza Horizon 5", "forzahorizon5" },
            { "Forza Motorsport 8", "forza_gaming.desktop.x64_release_final" }
        };
        
        var currentOpen = string.Empty;
        while (true)
        {
            Task.Delay(Attached ? 1000 : 500).Wait();
            
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (Attached && Mem.OpenProcess(currentOpen) != Mem.OpenProcessResults.Success)
            {
                Attached = false;
                AttachedText = NotAttachedText;
                CurrentView = Resources.Pages.GetPage(typeof(AioInfo));
                Resources.Pages.Clear();
            }
            else if (Attached)
            {
                continue;
            }
            
            foreach (var element in gamesDictionary.Where(element => Mem.OpenProcess(element.Value) == Mem.OpenProcessResults.Success))
            {
                currentOpen = element.Value;
                Task.Run(() => GvpMaker(element.Key));
                Attached = true;
                break;
            }
        }
    }
    
    private void GvpMaker(string name)
    {
        var process = Mem.MProc.Process;
        if (process.MainModule == null)
        {
            return;
        }
        
        string platform;
        string update;    
        var gamePath = process.MainModule.FileName;

        if (gamePath.Contains("Microsoft.624F8B84B80") || gamePath.Contains("Microsoft.SunriseBaseGame") ||
            gamePath.Contains("Microsoft.ForzaMotorsport"))
        {
            platform = "MS";
            var filePath = Combine(GetDirectoryName(gamePath) ?? string.Empty, "appxmanifest.xml");
            var xml = XElement.Load(filePath);
            var descendants = xml.Descendants().Where(e => e.Name.LocalName == "Identity");
            var version = descendants.Select(e => e.Attribute("Version")).FirstOrDefault();
            update = version == null ? "Unable to get update info" : version.Value;
        }
        else
        {
            var filePath = Combine(GetDirectoryName(gamePath) ?? string.Empty, "OnlineFix64.dll");
            platform = File.Exists(filePath) ? "OnlineFix - Steam" : "Steam";
            update = GetVersionInfo(process.MainModule.FileName).FileVersion ?? "Unable to get update info";
        }

        var type = GetTypeFromName(name);
        GameVerPlat = new GameVerPlat(name, platform, update, process, type);
        AttachedText = $"{GameVerPlat.Name}, {GameVerPlat.Plat}, {GameVerPlat.Update}";
    }

    private static GameVerPlat.GameType GetTypeFromName(string name)
    {
        return name switch
        {
            "Forza Horizon 4" => GameVerPlat.GameType.Fh4,
            "Forza Horizon 5" => GameVerPlat.GameType.Fh5,
            "Forza Motorsport 8" => GameVerPlat.GameType.Fm8,
            _ => GameVerPlat.GameType.None
        };
    }

    [RelayCommand]
    private void ToggleSearch(UIElement? textBox)
    {
        switch (SearchVisibility)
        {
            case Visibility.Collapsed:
            {
                HandleCollapsed();
                if (textBox != null)
                {
                    textBox.ClearValue(TextBox.TextProperty);
                    textBox.Focus();
                }
                break;
            }
            case Visibility.Visible:
            {
                HandleVisible();
                break;
            }
            case Visibility.Hidden:
            default:
            {
                throw new IndexOutOfRangeException();
            }
        }
    }

    [RelayCommand]
    private void CloseSearch()
    {
        if (SearchVisibility != Visibility.Visible)
        {
            return;
        }
        
        HandleVisible();
    }

    private async void HandleCollapsed()
    {
        SearchVisibility = Visibility.Visible;
        
        while (SearchOpacity < 0.5)
        {
            SearchOpacity += 0.05;
            await Task.Delay(15);
        }
    }

    private async void HandleVisible()
    {
        while (SearchOpacity > 0)
        {
            SearchOpacity -= 0.05;
            await Task.Delay(15);
        }
        
        SearchVisibility = Visibility.Collapsed;
    }

    public void Search(string text)
    {
        SearchResults.Clear();
        if (text == string.Empty)
        {
            return;
        }

        var search = Resources.Search.SearchResults.EverySearchResult.Where(i =>
            i.Name.Contains(text, StringComparison.InvariantCultureIgnoreCase) ||
            i.Feature.Contains(text, StringComparison.InvariantCultureIgnoreCase) ||
            i.Category.Contains(text, StringComparison.InvariantCultureIgnoreCase) ||
            i.Page.Contains(text, StringComparison.InvariantCultureIgnoreCase));
        
        var searchResults = search as SearchResult[] ?? search.ToArray();
        foreach (var element in searchResults)
        {
            if (!Attached && element.PageType != typeof(AioInfo) && element.PageType != typeof(Keybindings))
            {
                continue;
            }
            
            SearchResults.Add(element);
        }
    }
}