using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;

namespace Forza_Mods_AIO.ViewModels.Pages;

public partial class AutoshowViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _uiElementsEnabled = true;
    
    private static Sql SqlFh5 => Resources.Cheats.GetClass<Sql>();
    

    [RelayCommand]
    private async Task ExecuteSql(object parameter)
    {
        if (parameter is not string sParam)
        {
            return;
        }
        
        UiElementsEnabled = false;
        if (SqlFh5.CDatabaseAddress == 0)
        {
            await SqlFh5.SqlExecAobScan();
        }

        if (SqlFh5.CDatabaseAddress == 0)
        {
            goto SkipQuerying;
        }
        
        await SqlFh5.Query(sParam);
        
        SkipQuerying:
        UiElementsEnabled = true;
    }
}