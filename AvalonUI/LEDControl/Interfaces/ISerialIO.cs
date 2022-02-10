using System;

namespace LEDControl.Interfaces;

public interface ISerialIO
{
    event Action<string> OnNewData;
    string[] GetAvailablePorts();
    void SetPort(string port);
}