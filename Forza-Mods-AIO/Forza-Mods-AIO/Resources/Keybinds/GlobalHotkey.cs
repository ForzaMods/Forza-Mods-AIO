using System.Windows.Input;

namespace Forza_Mods_AIO.Resources.Keybinds;

// https://github.com/AngryCarrot789/KeyDownTester/blob/master/KeyDownTester/Keys/GlobalHotkey.cs
public class GlobalHotkey
{
    private Key _key;
    public Key Key { get => _key; set => SetField(ref _key, value); }
    
    private Action _callback = null!;
    public Action Callback { get => _callback; set => SetField(ref _callback, value); }

    private ModifierKeys _modifier;
    public ModifierKeys Modifier { get => _modifier; set => SetField(ref _modifier, value); }

    private bool _canExecute;
    public bool CanExecute { get => _canExecute; set => SetField(ref _canExecute, value); }
    
    public GlobalHotkey(Key key, Action callback, ModifierKeys modifier = ModifierKeys.None, bool canExecute = true)
    {
        Key = key;
        Callback = callback;
        Modifier = modifier;
        CanExecute = canExecute;
        HotkeysManager.AddHotkey(this);
    }

    private void SetField<T>(ref T field, T value)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        HotkeysManager.RemoveHotkey(this);
        HotkeysManager.AddHotkey(this);
    }
}