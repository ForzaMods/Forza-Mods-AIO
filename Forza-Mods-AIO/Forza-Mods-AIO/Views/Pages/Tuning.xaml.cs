using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Models;
using Forza_Mods_AIO.Resources.Search;
using Forza_Mods_AIO.Resources.Theme;
using Forza_Mods_AIO.ViewModels.Pages;

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