using System.Collections.Generic;
using LEDControl.Interfaces;

namespace LEDControl.ViewModels;

public class PreferencesViewModel : ViewModelBase
{
    public IReadOnlyList<string> Ports { get; private set; }

    public int CurrentPortIndex
    {
        get => _currentPortIndex;
        set
        {
            if (_currentPortIndex == value) return;
            _preferences.SerialPort = _ports[value];
            _serialIO.SetPort(_ports[value]);
            _currentPortIndex = value;
        }
    }
    
    private ISerialIO _serialIO;
    private IPreferences _preferences;

    private readonly List<string> _ports = new();
    private int _currentPortIndex;
    
    public PreferencesViewModel(IPreferences preferences, ISerialIO serialIO)
    {
        _preferences = preferences;
        _serialIO = serialIO;

        Ports = _ports.AsReadOnly();

        _ports.Clear();
        _currentPortIndex = -1;
        int index = 0;
        foreach (var port in _serialIO.GetAvailablePorts())
        {
            _ports.Add(port);
            if (port == _preferences.SerialPort) CurrentPortIndex = index;
            index++;
        }
    }
}