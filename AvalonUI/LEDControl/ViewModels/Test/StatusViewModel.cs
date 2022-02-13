using System;
using ReactiveUI;

namespace LEDControl.ViewModels.Test;

public class StatusViewModel : ReactiveObject, IStatusViewModel
{
    public string Current { get; } = "1.0A";
    public string Time { get; } = (new DateTimeOffset()).ToString();
}