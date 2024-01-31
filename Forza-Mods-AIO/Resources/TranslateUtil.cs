using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace Forza_Mods_AIO.Resources;

public class TranslateUtil
{
    private Dictionary<string, string> _translation = Translations.FrenchTranslation;

    public void SetLanguage(Dictionary<string, string> translation)
    {
        _translation = translation;
    }
    
    private class OriginalValues
    {
        public string Content { get; set; } = string.Empty;
        public string ToolTip { get; set; } = string.Empty;
    }
    
    private class OriginalComboBoxValues : OriginalValues
    {
        public new List<string> Content { get; } = new();
    }

    private Dictionary<FrameworkElement, OriginalValues> OriginalSnapshot { get; set; } = new();
    
    public void Translate()
    {
        var snapshot = CreateSnapshot();

        foreach (var element in MainWindow.Mw.GetChildren().Cast<FrameworkElement>())
        {
            if (element is ComboBox box)
            {
                TranslateComboBoxItems(box);
                continue;
            }

            TranslateElementContent(element);
            TranslateElementToolTip(element);
        }

        OriginalSnapshot = snapshot;
    }

    private Dictionary<FrameworkElement, OriginalValues> CreateSnapshot()
    {
        var snapshot = new Dictionary<FrameworkElement, OriginalValues>();
        foreach (var element in MainWindow.Mw.GetChildren().Cast<FrameworkElement>())
        {
            var isComboBox = element.GetType() == typeof(ComboBox);
            if (isComboBox)
            {
                snapshot.Add(element, CreateComboBoxSnapshot((ComboBox)element));
                continue;
            }

            var originalValues = new OriginalValues();
            var contentProperty = element.GetType().GetProperty("Content");
            var contentValue = contentProperty?.GetValue(element)?.ToString();
            if (contentValue != null && _translation.ContainsKey(contentValue))
            {
                originalValues.Content = contentValue;
            }

            var toolTipProperty = element.GetType().GetProperty("ToolTip");
            var toolTipValue = toolTipProperty?.GetValue(element)?.ToString();
            if (toolTipValue != null && _translation.ContainsKey(toolTipValue))
            {
                originalValues.ToolTip = toolTipValue;
            }

            snapshot.Add(element, originalValues);
        }

        return snapshot;
    }

    private static OriginalComboBoxValues CreateComboBoxSnapshot(ItemsControl comboBox)
    {
        var result = new OriginalComboBoxValues();
        foreach (var item in comboBox.Items)
        {
            if (item is not ComboBoxItem comboBoxItem)
            {
                continue;
            }
            
            result.Content.Add(comboBoxItem.Content.ToString() ?? string.Empty);    
        }
        
        return result;
    }
    
    public void RevertToEnglish()
    {
        if (OriginalSnapshot.Count == 0)
        {
            return;
        }

        foreach (var (element, originalValues) in OriginalSnapshot)
        {
            if (element.GetType() == typeof(ComboBox))
            {
                TranslateComboBoxBack((ComboBox)element, (OriginalComboBoxValues)originalValues);
                continue;                
            }
            
            var contentProperty = element.GetType().GetProperty("Content");
            if (contentProperty != null && _translation.ContainsKey(originalValues.Content))
            {
                contentProperty.SetValue(element, originalValues.Content);
            }

            var toolTipProperty = element.GetType().GetProperty("ToolTip");
            if (toolTipProperty != null && _translation.ContainsKey(originalValues.ToolTip))
            {
                toolTipProperty.SetValue(element, originalValues.ToolTip);
            }
        }
    }

    private static void TranslateComboBoxBack(ItemsControl comboBox, OriginalComboBoxValues originalComboBoxValues)
    {
        for (var i = 0; i < comboBox.Items.Count; i++)
        {
            if (comboBox.Items[i] is not ComboBoxItem comboBoxItem)
            {
                continue;
            }

            comboBoxItem.Content = originalComboBoxValues.Content[i];
        }
    }

    private void TranslateComboBoxItems(ItemsControl comboBox)
    {
        foreach (var item in comboBox.Items)
        {
            if (item is ComboBoxItem comboBoxItem && _translation.ContainsKey((string)comboBoxItem.Content))
            {
                comboBoxItem.Content = _translation[(string)comboBoxItem.Content];
            }
        }
    }

    private void TranslateElementContent(FrameworkElement element)
    {
        var contentProperty = element.GetType().GetProperty("Content");
        if (contentProperty != null && contentProperty.GetValue(element) is string key)
        {
            TranslateAndSetValue(element, contentProperty, key);
        }
    }

    private void TranslateElementToolTip(FrameworkElement element)
    {
        var toolTipProperty = element.GetType().GetProperty("ToolTip");
        if (toolTipProperty != null && toolTipProperty.GetValue(element) is string key)
        {
            TranslateAndSetValue(element, toolTipProperty, key);
        }
    }

    private void TranslateAndSetValue(FrameworkElement element, PropertyInfo property, string key)
    {
        if (_translation.TryGetValue(key, out var value))
        {
            property.SetValue(element, value);
        }
    }
}