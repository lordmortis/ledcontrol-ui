using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvalonUI.ViewModels;

namespace AvalonUI.Views;

public partial class PreferencesWindow : ReactiveWindow<PreferencesViewModel>
{
    public PreferencesWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}