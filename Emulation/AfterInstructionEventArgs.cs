namespace CS6502.Emulation;

public class AfterInstructionEventArgs : EventArgs
{
    public Opcode Opcode { get; }
    public int Cycles { get; }

    public AfterInstructionEventArgs(Opcode opcode, int cycles)
    {
        Opcode = opcode;
        Cycles = cycles;
    }
}