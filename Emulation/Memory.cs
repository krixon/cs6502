namespace CS6502.Emulation;

public class Memory : IMemory
{
    private readonly byte[] _bytes = new byte[ushort.MaxValue];

    public byte this[ushort address]
    {
        get => _bytes[address];
        set => _bytes[address] = value;
    }

    public byte[] this[Range range] => _bytes[range];
}