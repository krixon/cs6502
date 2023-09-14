namespace CS6502.Emulation;

public record Instruction(Opcode Opcode, AddressingMode AddressingMode) : IInstruction;