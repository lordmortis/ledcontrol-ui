using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using AvalonUI.Interfaces;

namespace AvalonUI;

public class DefaultSerialIO : ISerialIO
{
    public event Action<string>? OnNewData;
    
    private SerialPort? _serialPort;
    private readonly StringBuilder _currentData = new();
    
    public string[] GetAvailablePorts()
    {
        return SerialPort.GetPortNames();
    }

    public void SetPort(string port)
    {
        if (_serialPort != null)
        {
            _serialPort.DataReceived -= SerialPortOnDataReceived;
            _serialPort.ErrorReceived -= SerialPortOnErrorReceived;        
            _serialPort?.Close();
        }
        
        _serialPort = new SerialPort(port, 115200, Parity.None, 8);
        _serialPort.Open();
        _serialPort.DataReceived += SerialPortOnDataReceived;
        _serialPort.ErrorReceived += SerialPortOnErrorReceived;
    }

    private void SerialPortOnErrorReceived(object sender, SerialErrorReceivedEventArgs e)
    {
        Debug.WriteLine(e.ToString());
    }

    private void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        if (_serialPort == null) return;
        var newData = _serialPort.ReadExisting();
        int newLineIndex = -1;
        do
        {
            newLineIndex = newData.IndexOf("\r\n", StringComparison.InvariantCultureIgnoreCase);
            if (newLineIndex == -1)
            {
                if (newData.Length > 0) _currentData.Append(newData);
                continue;
            }

            _currentData.Append(newData.Substring(0, newLineIndex));
            OnNewData?.Invoke(_currentData.ToString());
            _currentData.Clear();
            int newStart = newLineIndex + 2;
            int length = newData.Length - newStart;
            newData = newData.Substring(newStart, length);
        } while (newLineIndex != -1);
        
    }
}