using Forza_Mods_AIO.Resources.Theme;

namespace Forza_Mods_AIO.Views.Pages;

public partial class Tuning
{
    public Tuning()
    {
        DataContext = this;
        
        InitializeComponent();
    }
    
    public Monet Theming => Monet.GetInstance();
}