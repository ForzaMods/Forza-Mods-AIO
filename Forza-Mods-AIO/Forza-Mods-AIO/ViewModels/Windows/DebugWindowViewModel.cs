using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Forza_Mods_AIO.Models;
using MahApps.Metro.Controls;

namespace Forza_Mods_AIO.ViewModels.Windows;

public partial class DebugWindowViewModel : ObservableObject
{
    private bool _isInitialized;

    public ObservableCollection<DebugSession> DebugSessions => Resources.DebugSessions.GetInstance().EveryDebugSession;
    
    [ObservableProperty]
    private DebugSession _currentDebugSession = null!;

    [ObservableProperty]
    private bool _areAnyBreakpointsAvailable;

    [ObservableProperty]
    private string _windowTitle = string.Empty;

    [RelayCommand]
    private void UnpauseBreakpoint() => CurrentDebugSession.DebugBreakpoints.First(s => s.IsHit).Unpause();
    
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