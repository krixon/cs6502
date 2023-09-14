namespace CS6502.Emulation;

public class UnknownOpcodeException : Exception
{
    public Opcode Opcode { get; }

    public UnknownOpcodeException(Opcode opcode) : base($"Unknown opcode {opcode:X2}")
    {
        Opcode = opcode;
    }
}