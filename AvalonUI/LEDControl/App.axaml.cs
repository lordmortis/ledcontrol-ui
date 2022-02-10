using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LEDControl.Interfaces;
using LEDControl.ViewModels;
using LEDControl.Views;
using Splat;

namespace LEDControl
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            Bootstrapper.Register(Locator.CurrentMutable, Locator.Current);
            
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainViewModel(Locator.Current.GetService<ISerialIO>())
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainView
                {
                    DataContext = new MainViewModel(Locator.Current.GetService<ISerialIO>())
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}