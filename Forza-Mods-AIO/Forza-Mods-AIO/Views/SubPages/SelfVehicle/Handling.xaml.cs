using Forza_Mods_AIO.Resources.Theme;

namespace Forza_Mods_AIO.Views.SubPages.SelfVehicle;

public partial class Handling
{
    public Handling()
    {
        DataContext = this;
        InitializeComponent();
    }

    public Monet Theming => Monet.GetInstance();
}