using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LEDControl.ViewModels;
using Splat;

namespace LEDControl.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif                     
        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Closing += (sender, args) =>
            {
                if (DataContext is not MainWindowViewModel actualContext) return;
                actualContext.OnClose();
            };
        }
    }
}