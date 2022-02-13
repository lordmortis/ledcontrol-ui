using System.Reactive;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using LEDControl.Views;
using ReactiveUI;
using Splat;

namespace LEDControl.ViewModels;

public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
{
    public bool ShowWindowMenu { get; private set; }
 
    public IStatusViewModel StatusViewModel { get; private set; }

    public ReactiveCommand<Window, Unit> OnShowPreferences {get; private set;}
    
    private PreferencesWindow? _preferencesWindow;

    public MainWindowViewModel()
    {
        ShowWindowMenu = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        StatusViewModel = Locator.Current.GetService<StatusViewModel>();
        OnShowPreferences = ReactiveCommand.Create<Window, Unit>(ShowPreferencesFunc);
    }
    
    public void OnClose()
    {
        if (_preferencesWindow != null && _preferencesWindow.IsVisible) _preferencesWindow.Close();
    }

    private Unit ShowPreferencesFunc(Window window)
    {
        if (_preferencesWindow is {IsVisible: true})
        {
            _preferencesWindow.Position = window.Position + new PixelVector(50, 50);
            return Unit.Default;
        }
            
        _preferencesWindow = new PreferencesWindow
        {
            DataContext = Locator.Current.GetService<PreferencesViewModel>()
        };
            
        _preferencesWindow.Show();
        _preferencesWindow.Position = window.Position + new PixelVector(50, 50);
        return Unit.Default;        
    }
}