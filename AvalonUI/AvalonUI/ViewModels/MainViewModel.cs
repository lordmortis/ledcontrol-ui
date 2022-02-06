using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using AvalonUI.Views;
using ReactiveUI;

namespace AvalonUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public bool ShowWindowMenu { get; private set; }
        public string Greeting => "Testing";

        public ReactiveCommand<Window, Unit> OnShowPreferences => ReactiveCommand.Create<Window, Unit>( window =>
        {
            _preferencesWindow ??= new PreferencesWindow
            {
                DataContext = new PreferencesViewModel()
            };

            if (_preferencesWindow.IsVisible) return Unit.Default;
            _preferencesWindow.Show();
            _preferencesWindow.Position = window.Position + new PixelVector(50, 50);
            return Unit.Default;
        });
        
        private PreferencesWindow? _preferencesWindow;

        public MainViewModel()
        {
            ShowWindowMenu = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        public void OnClose()
        {
            if (_preferencesWindow != null && _preferencesWindow.IsVisible) _preferencesWindow.Close();
        }
    }
}