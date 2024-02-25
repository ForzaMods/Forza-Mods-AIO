using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Forza_Mods_AIO.Models;
using Forza_Mods_AIO.Resources;
using MahApps.Metro.Controls;

namespace Forza_Mods_AIO.ViewModels.Windows;

public partial class DebugWindowViewModel : ObservableObject
{
    private bool _isInitialized;

    public ObservableCollection<DebugSession> DebugSessions => Forza_Mods_AIO.Resources.DebugSessions.GetInstance().EveryDebugSession;
    
    [ObservableProperty]
    private DebugSession _currentDebugSession = null!;

    [ObservableProperty]
    private bool _areAnyBreakpointsAvailable;

    [ObservableProperty]
    private string _windowTitle = string.Empty;
    
    public DebugWindowViewModel()
    {
        if (_isInitialized) return;
        InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        WindowTitle = $"{App.GetRequiredService<MetroWindow>().Title} - Debug Window";
        _isInitialized = true;
    }
}