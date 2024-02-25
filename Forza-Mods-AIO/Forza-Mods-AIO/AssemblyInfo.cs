using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None,ResourceDictionaryLocation.SourceAssembly)]

#if !RELEASE
[assembly: XmlnsDefinition("debug-mode", "Namespace")]
#endif
