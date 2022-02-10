using Avalonia.ReactiveUI;
using Avalonia.Web.Blazor;

namespace AvalonUI.Web;

public partial class App
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        WebAppBuilder.Configure<LEDControl.App>()
            .UseReactiveUI()
            .SetupWithSingleViewLifetime();
    }
}