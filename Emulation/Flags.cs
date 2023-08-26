namespace CS6502.Emulation;

[Flags]
public enum Flags : byte
{
    None = 0,
    Carry = 1,
    Zero = 2,
    Interrupt = 4,
    Decimal = 8,
    Break = 16,
    Overflow = 32,
    Negative = 64
}
