using Forza_Mods_AIO.Resources.Theme;
using Forza_Mods_AIO.ViewModels.Pages;

namespace Forza_Mods_AIO.Views.Pages;

public partial class AioInfo
{
    public AioInfo()
    {
        ViewModel = new AioInfoViewModel();
        DataContext = this;

        InitializeComponent();
    }

    public AioInfoViewModel ViewModel { get; }
    public Monet Theming => Monet.GetInstance();
}