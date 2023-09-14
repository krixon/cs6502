namespace CS6502.Emulation;

public class Mos6502 : Processor
{
    public Mos6502() : base(InstructionSetFactory.Mos6502)
    {
    }

    public Mos6502(IMemory memory, IClock clock) : base(InstructionSetFactory.Mos6502, memory, clock)
    {
    }

    // private void LoadAccumulator(AddressingMode addressingMode)
    // {
    //     A = ReadByte(addressingMode);
    //     SetNegativeAndZeroFlags(A);
    // }
    //
    // private void LoadX(AddressingMode addressingMode)
    // {
    //     X = ReadByte(addressingMode);
    //     SetNegativeAndZeroFlags(X);
    // }
    //
    // private void LoadY(AddressingMode addressingMode)
    // {
    //     Y = ReadByte(addressingMode);
    //     SetNegativeAndZeroFlags(Y);
    // }
}