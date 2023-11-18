using MahApps.Metro.Controls;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Forza_Mods_AIO.Resources;

internal abstract class UpdateUi
{
    public static void UpdateUI(bool enable, Page? page)
    {
        Application.Current.Dispatcher.BeginInvoke(delegate ()
        {
            foreach (var visual in page.GetChildren())
            {
                var element = (FrameworkElement)visual;
                var elementType = element.GetType();

                if (elementType != typeof(ToggleSwitch) &&
                    elementType != typeof(ComboBox) &&
                    elementType != typeof(Button))
                {
                    continue;
                }
                    
                element.IsEnabled = enable;
            }
        });
    }
    public static bool AnimCompleted = true;

    public static void Animate(object sender, bool alreadyOpen, Dictionary<string, double> sizes, Dictionary<string, bool> isClicked, Page page)
    {
        AnimCompleted = false;
            
        foreach (FrameworkElement element in page.GetChildren())
        {
            var senderName = sender.GetType().GetProperty("Name")?.GetValue(sender)!.ToString();
            var elementName = element.GetType().GetProperty("Name")?.GetValue(element)!.ToString();
            var type = element.GetType();

            if (elementName == "PART_ClearText" || (type != typeof(Button) && type != typeof(Frame)))
            {
                continue;
            }

            DoubleAnimation danimationPage;
            ThicknessAnimation tanimationPage;
            ThicknessAnimation tanimationButton;
            var storyboard = new Storyboard();

            const double Duration = 0.1;
                
            if (elementName.Contains("Page") && elementName.Contains(senderName.Replace("Button", string.Empty)))
            {
                storyboard.Completed += (s, e) =>
                {
                    AnimCompleted = true;
                    if (alreadyOpen)
                    {
                        element.Visibility = Visibility.Hidden;
                    }
                };
                element.Visibility = Visibility.Visible;

                //Page move height of button
                var start = (Thickness)element.GetType().GetProperty("Margin").GetValue(element);
                var end = new Thickness(start.Left, start.Top + 25, start.Right, start.Bottom);
                    
                if (alreadyOpen)
                {
                    end = new Thickness(start.Left, start.Top - 25, start.Right, start.Bottom);
                }
                    
                var duration = new Duration(TimeSpan.FromSeconds(Duration));
                    
                tanimationPage = new ThicknessAnimation(end, duration);

                //Page change height
                danimationPage = new DoubleAnimation(sizes[senderName], duration);
                if (alreadyOpen)
                {
                    danimationPage = new DoubleAnimation(25, duration);
                }

                Storyboard.SetTargetName(tanimationPage, elementName);
                Storyboard.SetTargetProperty(tanimationPage, new PropertyPath(Frame.MarginProperty));
                storyboard.Children.Add(tanimationPage);

                Storyboard.SetTargetName(danimationPage, elementName);
                Storyboard.SetTargetProperty(danimationPage, new PropertyPath(Frame.HeightProperty));
                storyboard.Children.Add(danimationPage);
                storyboard.Begin(element);
            }
            else if ((type == typeof(Button)
                      && isClicked.Keys.ToList().IndexOf(elementName) > isClicked.Keys.ToList().IndexOf(senderName))                              // Button is below the button that was clicked
                     || (type == typeof(Frame)
                         && !elementName.Contains(senderName.Replace("Button", String.Empty))                                                        // Page isnt the one being shown
                         && isClicked.Keys.ToList().IndexOf(elementName.Replace("Page", "Button")) > isClicked.Keys.ToList().IndexOf(senderName)))   // Page is below the button that was clicked
            {
                //Move all buttons down by size of page opened
                var start = (Thickness)type.GetProperty("Margin").GetValue(element);
                var end = new Thickness(start.Left, start.Top + sizes[senderName], start.Right, start.Bottom);
                if (alreadyOpen)
                {
                    end = new Thickness(start.Left, start.Top - sizes[senderName], start.Right, start.Bottom);
                }

                tanimationButton = new ThicknessAnimation(end, new Duration(TimeSpan.FromSeconds(Duration)));

                Storyboard.SetTargetName(tanimationButton, elementName);
                Storyboard.SetTargetProperty(tanimationButton, new PropertyPath(Button.MarginProperty));
                storyboard.Children.Add(tanimationButton);
                storyboard.Begin(element);
            }
        }
    }
        
    public static void AddProgress(int scanAmount, ref int index, MetroProgressBar progressBar)
    {
        ++index;
        var idx = index;
            
        Task.Run(() =>
        {
            var progress = (int)(Math.Round((decimal)100 / scanAmount) * idx);
            if (progress > 100)
            {
                progress = 100;
            }

            var currentProgress = 0;
            Application.Current.Dispatcher.Invoke(() => currentProgress = (int)progressBar.Value);
                
            for (var i = currentProgress; i <= progress; ++i)
            {
                var i1 = i;
                Application.Current.Dispatcher.Invoke(() => progressBar.Value = i1);
                Thread.Sleep(15);
            }
        });
    }
}