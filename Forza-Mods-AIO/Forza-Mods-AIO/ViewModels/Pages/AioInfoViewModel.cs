using System.Diagnostics;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Forza_Mods_AIO.Resources.Theme;
using Forza_Mods_AIO.Services;
using Forza_Mods_AIO.Views.Windows;

namespace Forza_Mods_AIO.ViewModels.Pages;

public partial class AioInfoViewModel : ObservableObject
{
    [ObservableProperty]
    private string _version = $"Version: {Assembly.GetExecutingAssembly().GetName().Version!.ToString()}";
    
    [RelayCommand]
    private static void LaunchUrl(string param) => Process.Start("explorer.exe",$"\"{param}\"");

    [RelayCommand]
    private static void ChangeMonet() => Monet.GetInstance().ChangeColor();

    [RelayCommand]
    private static void ShowDebugWindow() => WindowsProviderService.Show<DebugWindow>();
}