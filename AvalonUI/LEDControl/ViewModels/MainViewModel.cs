using System.Reactive;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using LEDControl.Interfaces;
using LEDControl.Views;
using ReactiveUI;
using Splat;

namespace LEDControl.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public bool ShowWindowMenu { get; private set; }
        public string Greeting { get; private set; } = "";
        
        public MainViewModel(ISerialIO serialIO)
        {
            serialIO.OnNewStatus += OnNewStatus;
            ShowWindowMenu = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        private void OnNewStatus(Proto.Status status)
        {
            Greeting = $"{status.Current}";
            this.RaisePropertyChanged(nameof(Greeting));
        }

        public ReactiveCommand<Window, Unit> OnShowPreferences => ReactiveCommand.Create<Window, Unit>( window =>
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
        });
        
        private PreferencesWindow? _preferencesWindow;

        public void OnClose()
        {
            if (_preferencesWindow != null && _preferencesWindow.IsVisible) _preferencesWindow.Close();
        }
    }
}