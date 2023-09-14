namespace CS6502.Emulation;

public class StatusRegister
{
    public bool Carry
    {
        get => _value.HasFlag(Flags.Carry);
        internal set => UpdateFlags(Flags.Carry, value);
    }

    public bool Zero
    {
        get => _value.HasFlag(Flags.Zero);
        internal set => UpdateFlags(Flags.Zero, value);
    }

    public bool Interrupt
    {
        get => _value.HasFlag(Flags.Interrupt);
        internal set => UpdateFlags(Flags.Interrupt, value);
    }

    public bool Decimal
    {
        get => _value.HasFlag(Flags.Decimal);
        internal set => UpdateFlags(Flags.Decimal, value);
    }

    public bool Break
    {
        get => _value.HasFlag(Flags.Break);
        internal set => UpdateFlags(Flags.Break, value);
    }

    public bool Overflow
    {
        get => _value.HasFlag(Flags.Overflow);
        internal set => UpdateFlags(Flags.Overflow, value);
    }

    public bool Negative
    {
        get => _value.HasFlag(Flags.Negative);
        internal set => UpdateFlags(Flags.Negative, value);
    }

    private Flags _value = Flags.None;

    internal void ClearAll() => _value = Flags.None;

    private void UpdateFlags(Flags flags, bool set)
    {
        if (set)
        {
            _value |= flags;
        }
        else
        {
            _value &= ~flags;
        }
    }

    [Flags]
    private enum Flags : byte
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
}