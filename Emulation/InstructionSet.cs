namespace CS6502.Emulation;

public class InstructionSet
{
    private readonly Dictionary<Opcode, Instruction> _instructions = new();

    public Instruction this[Opcode opcode]
    {
        get
        {
            if (_instructions.TryGetValue(opcode, out var instruction))
            {
                return instruction;
            }

            throw new UnknownOpcodeException(opcode);
        }
    }

    public bool TryGetInstruction(Opcode opcode, out Instruction? instruction) =>
        _instructions.TryGetValue(opcode, out instruction);

    internal InstructionSet Add(Instruction instruction)
    {
        _instructions[instruction.Opcode] = instruction;

        return this;
    }

    internal InstructionSet Add(Opcode opcode, AddressingMode addressingMode = AddressingMode.Implied)
        => Add(new Instruction(opcode, addressingMode));
}
