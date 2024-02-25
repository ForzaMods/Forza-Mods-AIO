using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Forza_Mods_AIO.Converters;
using Forza_Mods_AIO.Models;
using Forza_Mods_AIO.Resources.Theme;
using Forza_Mods_AIO.ViewModels.Windows;

namespace Forza_Mods_AIO.Views.Windows;

public partial class MainWindow
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    public MainWindowViewModel ViewModel { get; }
    public Monet Theming => Monet.GetInstance();

    private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (WindowState != WindowState.Normal) return;

        var isLeftButton = e.ChangedButton == MouseButton.Left;
        if (!isLeftButton) return;

        var position = e.GetPosition(this);
        var isWithinTopArea = position.Y < 50;
        if (!isWithinTopArea) return;

        DragMove();
    }

    private void WindowStateAction_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button) return;

        switch (button.Tag)
        {
            case "1":
            {
                WindowState = WindowState.Minimized;
                break;
            }
            case "2":
            {
                ViewModel.HandleMaximizeMinimizeCommand.Execute(this);
                break;
            }
            case "3":
            {
                Close();
                Application.Current.Shutdown();
                break;
            }
        }
    }

    private void SearchGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton != MouseButton.Left) return;

        ViewModel.ToggleSearchCommand.Execute(null);
    }

    private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        ViewModel.Search(((TextBox)sender).Text);
    }

    private void ListViewItem_OnDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is not ListViewItem { Content: SearchResult searchResult }) return;

        GoToAndFocusElement(searchResult);
    }

    private void ListViewItem_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key is not Key.Enter) return;

        if (sender is not ListViewItem { Content: SearchResult searchResult }) return;

        GoToAndFocusElement(searchResult);
    }

    private void GoToAndFocusElement(SearchResult searchResult)
    {
        NavigateTo(searchResult.PageType);
        if (searchResult.DropDownExpander != null!)
        {
            searchResult.DropDownExpander.IsExpanded = true;
            searchResult.DropDownExpander.Focus();
        }

        if (searchResult.FrameworkElement != null! && !searchResult.FrameworkElement.Focus())
        {
            searchResult.FrameworkElement.Focus();
        }

        ViewModel.CloseSearchCommand.Execute(null);
        return;

        void NavigateTo(Type pageType)
        {
            var page = Forza_Mods_AIO.Resources.Pages.GetPage(pageType);
            ViewModel.CurrentView = page;
        }
    }
}