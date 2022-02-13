using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LEDControl.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LEDControl;

public class DefaultPreferences : IPreferences
{
    private const string FileName = "Preferences.json";
    
    public string SerialPort { get; set; } = "";

    private ISerialIO serialIO;
    private readonly string path;
    
    public DefaultPreferences(ISerialIO serialIO)
    {
        path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LEDControl" ,FileName);
        Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LEDControl"));
        Debug.WriteLine($"Path is {path}");
        this.serialIO = serialIO;
    }

    public async Task Load()
    {
        if (!File.Exists(path)) return;
        using StreamReader file = File.OpenText(path);
        using JsonTextReader reader = new JsonTextReader(file);
        JObject jsonPrefs = (JObject)await JToken.ReadFromAsync(reader);
        if (jsonPrefs.ContainsKey("serialPort"))
        {
            if (jsonPrefs["serialPort"] is JValue value)
            {
                if (value.Type == JTokenType.String && value.Value != null)
                {
                    SerialPort = (string) value.Value;
                    var ports = serialIO.GetAvailablePorts();
                    if (ports.Contains(SerialPort)) return;
                    SerialPort = "";
                }
            }
        }
    }

    public async Task Save()
    {
        JObject jsonPrefs = new JObject(
            new JProperty("serialPort", SerialPort)
            );
        using StreamWriter file = File.CreateText(path);
        using JsonTextWriter writer = new JsonTextWriter(file);
        await jsonPrefs.WriteToAsync(writer);
    }
}