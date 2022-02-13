using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Text;
using LEDControl.Interfaces;

namespace LEDControl;

public class DefaultSerialIO : ISerialIO
{
    public event Action<Proto.Status> OnNewStatus;
    
    private SerialPort? _serialPort;
    private readonly StringBuilder _currentData = new();
    
    private byte[] inputBuffer = new byte[1024];
    private int inputBufferPos = 0;
    
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
        
        _serialPort = new SerialPort(port, 115200, Parity.None, 8, StopBits.One);
        _serialPort.DiscardNull = false;
        _serialPort.Encoding = Encoding.GetEncoding("ISO-8859-1");
        _serialPort.Open();
        _serialPort.DataReceived += SerialPortOnDataReceived;
        _serialPort.ErrorReceived += SerialPortOnErrorReceived;
        _serialPort.DtrEnable = true;
    }

    private void SerialPortOnErrorReceived(object sender, SerialErrorReceivedEventArgs e)
    {
        Debug.WriteLine(e.ToString());
    }

    private void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        if (_serialPort == null) return;
        var readLength = _serialPort.Read(inputBuffer, inputBufferPos, inputBuffer.Length - inputBufferPos);
        inputBufferPos += readLength;
        if (inputBufferPos < 2) return;
        MemoryStream tempStream = new MemoryStream(inputBuffer);
        try
        {
            var status = Proto.Status.Parser.ParseDelimitedFrom(tempStream);
            OnNewStatus?.Invoke(status);
        }
        catch (Exception err)
        {
            Debug.WriteLine("Ruh roh");
            Debug.Write(err.ToString());
            Debug.Write(err.StackTrace);
        }

    }
}