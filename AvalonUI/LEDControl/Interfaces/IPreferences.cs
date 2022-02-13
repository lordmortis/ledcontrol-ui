using System.Threading.Tasks;

namespace LEDControl.Interfaces;

public interface IPreferences
{
    string SerialPort { get; set; }
    
    Task Load();
    Task Save();
}