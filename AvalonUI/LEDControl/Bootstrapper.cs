using LEDControl.Interfaces;
using LEDControl.ViewModels;
using Splat;

namespace LEDControl;

public static class Bootstrapper
{
    public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        services.RegisterLazySingleton<ISerialIO>(() => new DefaultSerialIO());
        services.RegisterLazySingleton<IPreferences>(() => new DefaultPreferences());
        
        services.Register<PreferencesViewModel>(() =>
        {
            var preferences = resolver.GetService<IPreferences>();
            var serialIO = resolver.GetService<ISerialIO>();
            return new PreferencesViewModel(preferences, serialIO);
        });
            
    }
}