using Forza_Mods_AIO.Resources.Theme;

namespace Forza_Mods_AIO.Views.SubPages.SelfVehicle;

public partial class Unlocks
{
    public Unlocks()
    {
        DataContext = this;
        
        InitializeComponent();
    }

    public Monet Theming => Monet.GetInstance();
}