using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;

namespace Forza_Mods_AIO.ViewModels.Pages;

public partial class AutoshowViewModel : ObservableObject
{
    private bool _isInitialized;

    [ObservableProperty]
    private bool _uiElementsEnabled = true;

    [ObservableProperty]
    private bool _allCarsEnabled;

    public string AllCarsParameter =>
        AllCarsEnabled
            ? "CREATE TABLE AutoshowTable(Id INT, NotAvailableInAutoshow INT); INSERT INTO AutoshowTable SELECT Id, NotAvailableInAutoshow FROM Data_Car; UPDATE Data_Car SET NotAvailableInAutoshow = 0;"
            : "UPDATE Data_Car SET NotAvailableInAutoshow = (SELECT NotAvailableInAutoshow FROM AutoshowTable WHERE Data_Car.Id == AutoshowTable.Id); DROP TABLE AutoshowTable;";
    
    private static Sql SqlFh5 => Resources.Cheats.GetClass<Sql>();
    
    public AutoshowViewModel()
    {
        if (_isInitialized) return;
        InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        _isInitialized = true;
    }

    [RelayCommand]
    private async Task ExecuteSql(object parameter)
    {
        if (parameter is not string sParam)
        {
            return;
        }
        
        UiElementsEnabled = false;
        await SqlFh5.Query(sParam);
        UiElementsEnabled = true;
    }
}