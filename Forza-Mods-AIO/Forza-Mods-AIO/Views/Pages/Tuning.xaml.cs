using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Models;
using Forza_Mods_AIO.Resources.Search;
using Forza_Mods_AIO.Resources.Theme;

namespace Forza_Mods_AIO.Views.Pages;

public partial class Tuning : Page
{
    public Monet Theming => Monet.GetInstance();
    
    public Tuning()
    {
        DataContext = this;
        
        InitializeComponent();
    }
    
    
}