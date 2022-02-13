using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;

namespace LEDControl.ViewModels.Test;

public class MainWindowViewModel : ReactiveObject, IMainWindowViewModel
{
    public bool ShowWindowMenu { get; } = false;

    public IStatusViewModel StatusViewModel { get; } = new StatusViewModel();

    public ReactiveCommand<Window, Unit> OnShowPreferences { get; } =
        ReactiveCommand.Create<Window, Unit>(window => Unit.Default);
    
    public void OnClose() { }
}