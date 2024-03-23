using Forza_Mods_AIO.Resources.Theme;
using Forza_Mods_AIO.ViewModels.Pages;

namespace Forza_Mods_AIO.Views.Pages;

public partial class Tuning
{
    public Tuning()
    {
        ViewModel = new TuningViewModel();
        DataContext = this;
        
        InitializeComponent();
    }
    
    public TuningViewModel ViewModel { get; }
    public Monet Theming => Monet.GetInstance();
}