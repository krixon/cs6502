namespace CS6502.Emulation;

public interface IInstruction
{
    Opcode Opcode { get; }
    AddressingMode AddressingMode { get; }
}