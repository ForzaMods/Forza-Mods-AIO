using System.ComponentModel;
using System.Runtime.CompilerServices;
using Forza_Mods_AIO.Services;
using Forza_Mods_AIO.Views.Windows;
using MahApps.Metro.Controls;

namespace Forza_Mods_AIO.Models;

public class DebugBreakpoint(string name) : INotifyPropertyChanged
{
    public string Name { get; } = name;
    public bool IsEnabled { get; set; }

    private bool _isHit;
    public bool IsHit
    {
        get => _isHit;
        private set => SetField(ref _isHit, value);
    }

    public async void MarkAsHit()
    {
        if (!IsEnabled)
        {
            return;
        }
        
        IsHit = true;
        WindowsProviderService.Show<DebugWindow>();
        
        var navigationWindow = App.GetRequiredService<MetroWindow>();
        navigationWindow.IsEnabled = false;

        while (IsHit)
        {
            await Task.Delay(1);
        }
        
        navigationWindow.IsEnabled = true;
    }

    public void Unpause()
    {
        if (!IsEnabled || !IsHit)
        {
            return;
        }

        IsHit = false;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }
}