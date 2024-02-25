using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Models;

public readonly struct SearchResult(
    string name,
    string category,
    string feature,
    string page,
    Type pageType,
    FrameworkElement frameworkElement = null!,
    Expander dropDownExpander = null!)
{
    public string Name { get; } = name;
    public string Category { get; } = category;
    public string Feature { get; } = feature;
    public string Page { get; } = page;
    public Type PageType { get; } = pageType;
    public FrameworkElement FrameworkElement { get; } = frameworkElement;
    public Expander DropDownExpander { get; } = dropDownExpander;
}