using System;
using LEDControl.Interfaces;
using ReactiveUI;

namespace LEDControl.ViewModels
{
    public class StatusViewModel : ViewModelBase, IStatusViewModel
    {
        public string Current { get; private set; } = "";
        public string Time { get; private set; } = "";

        public StatusViewModel(ISerialIO serialIO, IPreferences preferences)
        {
            serialIO.OnNewStatus += OnNewStatus;
            if (!string.IsNullOrWhiteSpace(preferences.SerialPort)) serialIO.SetPort(preferences.SerialPort);
        }
        
        private void OnNewStatus(Proto.Status status)
        {
            Current = $"{status.Current}";
            this.RaisePropertyChanged(nameof(Current));
            if (status.Time != null)
            {
                var time = DateTimeOffset.FromUnixTimeSeconds((long)status.Time.Seconds);
                Time = time.ToString();
                this.RaisePropertyChanged(nameof(Time));
            }
        }
    }
}