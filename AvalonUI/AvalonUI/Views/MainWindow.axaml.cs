using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvalonUI.ViewModels;

namespace AvalonUI.Views
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
                if (DataContext is not MainViewModel actualContext) return;
                actualContext.OnClose();
            };
        }
    }
}