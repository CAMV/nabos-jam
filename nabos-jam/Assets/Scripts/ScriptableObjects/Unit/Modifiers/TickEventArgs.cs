using System;

/// <summary>
/// EventArg clas to send ticks through event handler.
/// </summary>
public class TickEventArgs : EventArgs
{
    // Delta time to send
    public float value;

    public TickEventArgs(float tick)
    {
        value = tick;
    }
}