using System.Threading.Tasks;
using LEDControl.Interfaces;
using LEDControl.ViewModels;
using Splat;

namespace LEDControl;

public static class Bootstrapper
{
    public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        services.RegisterLazySingleton<ISerialIO>(() => new DefaultSerialIO());
        services.RegisterLazySingleton<IPreferences>(() =>
        {
            var serialIO = resolver.GetService<ISerialIO>();
            return new DefaultPreferences(serialIO);
        });
        
        services.Register<IPreferencesViewModel>(() =>
        {
            var preferences = resolver.GetService<IPreferences>();
            var serialIO = resolver.GetService<ISerialIO>();
            return new PreferencesViewModel(preferences, serialIO);
        });
        
        services.Register<IStatusViewModel>(() =>
        {
            var preferences = resolver.GetService<IPreferences>();
            var serialIO = resolver.GetService<ISerialIO>();
            return new StatusViewModel(serialIO, preferences);
        });
    }

    public static void Init()
    {
        Task.Run(async () =>
        {
            var prefs = Locator.Current.GetService<IPreferences>();
            await prefs.Load();
        }).Wait();
    }
}