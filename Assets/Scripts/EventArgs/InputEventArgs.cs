using System;

public class InputEventArgs : EventArgs
{
    private EInputEvent inputEvent;

    public EInputEvent InputEvent
    {
        get { return inputEvent; }
    }

    public InputEventArgs(EInputEvent inputEvent)
    {
        this.inputEvent = inputEvent;
    }
}