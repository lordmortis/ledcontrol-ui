using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;

namespace LEDControl.ViewModels;

public interface IMainWindowViewModel
{
    bool ShowWindowMenu { get; }
 
    IStatusViewModel StatusViewModel { get; }

    ReactiveCommand<Window, Unit> OnShowPreferences { get; }

    void OnClose();
}