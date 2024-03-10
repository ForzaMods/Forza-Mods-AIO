using System.Windows.Controls;
using System.Windows.Input;
using Forza_Mods_AIO.Models;
using Forza_Mods_AIO.Resources.Theme;
using Forza_Mods_AIO.ViewModels.Pages;
using Forza_Mods_AIO.ViewModels.Windows;

namespace Forza_Mods_AIO.Views.Pages;

public partial class Search
{
    public Search()
    {
        ViewModel = new SearchViewModel();
        DataContext = this;
        
        InitializeComponent();
    }

    public Monet Theming => Monet.GetInstance();
    public SearchViewModel ViewModel { get; }
    private MainWindowViewModel MainWindowViewModel => App.GetRequiredService<MainWindowViewModel>();
    
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

        MainWindowViewModel.CloseSearchCommand.Execute(null);
        return;

        void NavigateTo(Type pageType)
        {
            var page = Forza_Mods_AIO.Resources.Pages.GetPage(pageType);
            MainWindowViewModel.CurrentView = page;
        }
    }
}