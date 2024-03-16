using System.Windows;

namespace Forza_Mods_AIO.Services;

public static class WindowsProviderService
{
    public static void Show<T>() where T : class
    {
        if (!typeof(Window).IsAssignableFrom(typeof(T)))
        {
            throw new InvalidOperationException($"The window class should be derived from {typeof(Window)}.");
        }

        if (App.GetRequiredService<T>() is not Window windowInstance)
        {
            throw new InvalidOperationException("Window is not registered as service.");
        }

        windowInstance.Owner = Application.Current.MainWindow;
        windowInstance.Show();
    }
}