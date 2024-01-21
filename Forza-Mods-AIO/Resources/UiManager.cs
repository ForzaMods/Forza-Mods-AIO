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

    private const bool Disable = true;
    
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
        
        foreach (var visual in _page.GetChildren(_page))
        {
            var element = (FrameworkElement)visual;
            var elementName = element.GetType().GetProperty("Name")?.GetValue(element)!.ToString();
            var type = element.GetType();

            if (string.IsNullOrWhiteSpace(elementName))
            {
                continue;
            }
            
            if (elementName == "PART_ClearText" || (type != typeof(Button) && type != typeof(Frame)))
            {
                continue;
            }

            var storyboard = new Storyboard();
            const double animationDuration = 0.1;
            var duration = new Duration(TimeSpan.FromSeconds(animationDuration));
                
            if (elementName.Contains("Page") && elementName.Contains(senderName.Replace("Button", string.Empty)))
            {
                storyboard.Completed += (_, _) =>
                {
                    _dropDownCompleted = true;
                    if (!alreadyOpen) return;
                    element.Visibility = Visibility.Hidden;
                };
                element.Visibility = Visibility.Visible;

                //Page move height of button
                var start = (Thickness)(type.GetProperty("Margin")?.GetValue(element) ?? throw new InvalidOperationException());
                var end = new Thickness(start.Left, start.Top + 25, start.Right, start.Bottom);
                    
                if (alreadyOpen)
                {
                    end = new Thickness(start.Left, start.Top - 25, start.Right, start.Bottom);
                }
                    
                var thicknessAnimation = new ThicknessAnimation(end, duration);

                //Page change height
                var doubleAnimation = new DoubleAnimation(_sizes[senderName], duration);
                if (alreadyOpen)
                {
                    doubleAnimation = new DoubleAnimation(25, duration);
                }

                SetTargetName(thicknessAnimation, elementName);
                SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
                storyboard.Children.Add(thicknessAnimation);

                SetTargetName(doubleAnimation, elementName);
                SetTargetProperty(doubleAnimation, new PropertyPath(FrameworkElement.HeightProperty));
                storyboard.Children.Add(doubleAnimation);
                storyboard.Begin(element);
            }
            else if ((type == typeof(Button)
                      && _isClicked.Keys.ToList().IndexOf(elementName) > _isClicked.Keys.ToList().IndexOf(senderName))                                 // Button is below the button that was clicked
                     || (type == typeof(Frame) 
                         && !elementName.Contains(senderName.Replace("Button", String.Empty))                                                          // Page isnt the one being shown
                         && _isClicked.Keys.ToList().IndexOf(elementName.Replace("Page", "Button")) > _isClicked.Keys.ToList().IndexOf(senderName)))   // Page is below the button that was clicked
            {
                //Move all buttons down by size of page opened
                var start = (Thickness)(type.GetProperty("Margin")?.GetValue(element) ?? throw new InvalidOperationException());
                var end = new Thickness(start.Left, start.Top + _sizes[senderName], start.Right, start.Bottom);
                if (alreadyOpen)
                {
                    end = new Thickness(start.Left, start.Top - _sizes[senderName], start.Right, start.Bottom);
                }

                var thicknessAnimation = new ThicknessAnimation(end, duration);

                SetTargetName(thicknessAnimation, elementName);
                SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
                storyboard.Children.Add(thicknessAnimation);
                storyboard.Begin(element);
            }
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