using MahApps.Metro.Controls;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Linq;
using System.Threading.Tasks;
using static System.Windows.Media.Animation.Storyboard;

namespace Forza_Mods_AIO.Resources;

public class UiManager
{
    private readonly Page? _page;
    private readonly MetroProgressBar? _progressBar;
    private readonly IEnumerable<FrameworkElement> _elements;
    private readonly Dictionary<string, double> _sizes = null!;
    private readonly Dictionary<string, bool> _isClicked = null!;
    public double ScanAmount, Index;
    
    public UiManager(Page? page, 
        MetroProgressBar? progressBar)
    {
        _page = page;
        _progressBar = progressBar;
        _elements = page.GetChildren(page).OfType<FrameworkElement>()
            .Where(element => element.GetType() == typeof(ToggleSwitch) ||
                              element.GetType() == typeof(ComboBox) ||
                              element.GetType() == typeof(Button) && element.Name != "ScanButton");
    }
    
    public UiManager(Page? page,
        MetroProgressBar? progressBar,
        Dictionary<string, double> sizes,
        Dictionary<string, bool> isClicked)
    {
        _page = page;
        _progressBar = progressBar;
        _sizes = sizes;
        _isClicked = isClicked;
        _elements = page.GetChildren(page).OfType<FrameworkElement>()
            .Where(element => (element.GetType() == typeof(ToggleSwitch) ||
                              element.GetType() == typeof(ComboBox) ||
                              element.GetType() == typeof(Button)) && element.Name != "ScanButton");
    }
    
    public UiManager(Page? page,
        Dictionary<string, double> sizes,
        Dictionary<string, bool> isClicked)
    {
        _page = page;
        _sizes = sizes;
        _isClicked = isClicked;
        _elements = null!;
    }

    private const bool Disable = false;
    
    public void ToggleUiElements(bool enable)
    {
        if (_elements == null! || _page == null || Disable)
        {
            return;
        }
        
        _page.Dispatcher.BeginInvoke(delegate ()
        {
            foreach (var element in _elements)
            {
                element.IsEnabled = enable;
            }
        });
    }

    private bool _dropDownCompleted = true;
    public bool ToggleDropDown(object sender)
    {
        if (!_dropDownCompleted || _sizes == null! || _isClicked == null!)
        {
            return false;
        }
        
        _dropDownCompleted = false;
            
        var senderName = sender.GetType().GetProperty("Name")?.GetValue(sender)!.ToString();

        if (senderName == null)
        {
            return false;
        }
        
        var alreadyOpen = _isClicked[senderName];
        
        foreach (var element in _page.GetChildren(_page).Cast<FrameworkElement>())
        {
            var type = element.GetType();
            if (type != typeof(Button) && type != typeof(Frame))
            {
                continue;
            }
            
            var elementName = type.GetProperty("Name")?.GetValue(element)!.ToString();
            if (string.IsNullOrWhiteSpace(elementName) || elementName == "PART_ClearText")
            {
                continue;
            }

            if (!elementName.Contains("Page") || !elementName.Contains(senderName.Replace("Button", string.Empty)))
            {
                continue;
            }

            var storyboard = new Storyboard();
            storyboard.Completed += (_, _) =>
            {
                _dropDownCompleted = true;
                if (!alreadyOpen) return;
                element.Visibility = Visibility.Hidden;
            };
            element.Visibility = Visibility.Visible;

            const double animationDuration = 0.1;
            var duration = new Duration(TimeSpan.FromSeconds(animationDuration));
            var doubleAnimation = alreadyOpen
                ? new DoubleAnimation(0, duration)
                : new DoubleAnimation(_sizes[senderName], duration);

            SetTargetName(doubleAnimation, elementName);
            SetTargetProperty(doubleAnimation, new PropertyPath(FrameworkElement.HeightProperty));
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin(element);
        }
        
        _isClicked[senderName] = !_isClicked[senderName];
        return _isClicked[senderName];
    }

    public void Reset()
    {
        if (_sizes == null! || _isClicked == null!) return;
        
        foreach (var button in _page.GetChildren(_page).Where(button => button.GetType() == typeof(Button)))
        {
            var buttonName = button.GetType().GetProperty("Name")?.GetValue(button)!.ToString();
            
            if (string.IsNullOrWhiteSpace(buttonName) || !_isClicked.ContainsKey(buttonName) || !_isClicked[buttonName])
            {
                continue;
            }
            
            ToggleDropDown(button);
        }
    }
    
    public void AddProgress()
    {
        if (_progressBar == null || _page == null)
        {
            return;
        }
        
        ++Index;
            
        Task.Run(() =>
        {
            var progress = 100d / ScanAmount * Index;
            if (progress > 100)
            {
                progress = 100;
            }

            var currentProgress = _page.Dispatcher.Invoke(() => _progressBar.Value);
            for (var i = currentProgress; i <= progress; ++i)
            {
                var i1 = i;
                _page.Dispatcher.Invoke(() => _progressBar.Value = i1);
                Task.Delay(10).Wait();
            }
        });
    }
}