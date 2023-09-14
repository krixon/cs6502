namespace CS6502.Emulation;

public class Wdc65C02 : Processor
{
    public Wdc65C02() : base(InstructionSetFactory.Mos65C02)
    {
    }

    public Wdc65C02(IMemory memory, IClock clock) : base(InstructionSetFactory.Mos6502, memory, clock)
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