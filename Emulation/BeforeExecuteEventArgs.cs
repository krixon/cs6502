namespace CS6502.Emulation;

public class BeforeExecuteEventArgs : EventArgs
{
    public Instruction Instruction { get; }

    public BeforeExecuteEventArgs(Instruction instruction)
    {
        Instruction = instruction;
    }
}