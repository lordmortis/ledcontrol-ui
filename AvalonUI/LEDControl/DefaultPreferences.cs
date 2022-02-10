using LEDControl.Interfaces;

namespace LEDControl;

public class DefaultPreferences : IPreferences
{
    public string SerialPort { get; set; } = "";
}