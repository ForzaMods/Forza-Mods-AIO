using Forza_Mods_AIO.Resources.Theme;
using Forza_Mods_AIO.ViewModels.Pages;

namespace Forza_Mods_AIO.Views.Pages;

public partial class Autoshow
{
    public Autoshow()
    {
        ViewModel = new AutoshowViewModel();
        DataContext = this;
        
        InitializeComponent();
    }

    public AutoshowViewModel ViewModel { get; }
    public Monet Theming => Monet.GetInstance();
}