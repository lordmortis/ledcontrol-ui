using AvalonUI.Interfaces;

namespace AvalonUI;

public class DefaultPreferences : IPreferences
{
    public string SerialPort { get; set; } = "";
}