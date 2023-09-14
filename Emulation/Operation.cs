namespace CS6502.Emulation;

public record Operation(Instruction Instruction, byte[] Operands)
{
    public Operation(Instruction instruction, byte operand) : this(instruction, new[] { operand })
    {
    }
}