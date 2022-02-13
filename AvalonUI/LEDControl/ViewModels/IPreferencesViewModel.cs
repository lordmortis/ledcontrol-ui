using System.Collections.Generic;

namespace LEDControl.ViewModels;

public interface IPreferencesViewModel
{
    IReadOnlyList<string> Ports { get; }
    public int CurrentPortIndex { get; set; }
}