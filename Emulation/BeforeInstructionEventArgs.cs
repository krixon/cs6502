namespace CS6502.Emulation;

public class BeforeInstructionEventArgs : EventArgs
{
    public Opcode Opcode { get; }

    public BeforeInstructionEventArgs(Opcode opcode)
    {
        Opcode = opcode;
    }
}