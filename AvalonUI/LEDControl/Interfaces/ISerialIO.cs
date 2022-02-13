using System;

namespace LEDControl.Interfaces;

public interface ISerialIO
{
    event Action<Proto.Status> OnNewStatus;
    string[] GetAvailablePorts();
    void SetPort(string port);
}