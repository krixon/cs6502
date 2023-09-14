namespace CS6502.Emulation;

public class AfterExecuteEventArgs : EventArgs
{
    public Operation Operation { get; }

    public AfterExecuteEventArgs(Operation operation)
    {
        Operation = operation;
    }
}