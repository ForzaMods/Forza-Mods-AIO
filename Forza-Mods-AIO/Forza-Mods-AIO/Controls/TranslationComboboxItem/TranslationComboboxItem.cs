using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Controls.TranslationComboboxItem;

public class TranslationComboboxItem : ComboBoxItem
{
    public static readonly DependencyProperty TranslatorsProperty
        = DependencyProperty.Register(nameof(Translators),
            typeof(string),
            typeof(TranslationComboboxItem),
            new PropertyMetadata(default(string)));
    
    [Bindable(true)]
    [Category("Forza-Mods-AIO")]
    public string Translators
    {
        get => (string)GetValue(TranslatorsProperty);
        set => SetValue(TranslatorsProperty, value);
    }

    static TranslationComboboxItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TranslationComboboxItem), new FrameworkPropertyMetadata(typeof(TranslationComboboxItem)));
    }
}