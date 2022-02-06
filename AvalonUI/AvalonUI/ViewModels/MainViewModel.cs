using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AvalonUI.Views;

namespace AvalonUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public bool ShowWindowMenu { get; private set; }
        public string Greeting => "Testing";

        public MainViewModel()
        {
            ShowWindowMenu = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }
    }
}