using System.Collections.Generic;
using ReactiveUI;

namespace LEDControl.ViewModels.Test;

public class PreferencesViewModel : ReactiveObject, IPreferencesViewModel
{
    public IReadOnlyList<string> Ports { get; }
    public int CurrentPortIndex { get; set; } = 1;

    public PreferencesViewModel()
    {
        var ports = new List<string>();
        ports.Add("Com 1");
        ports.Add("Com 2");
        ports.Add("Junk");
        ports.Add("/dev/cu.usbmodem999999");
        Ports = ports;
    }
}