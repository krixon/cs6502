namespace CS6502.Emulation;

public class Memory
{
    private readonly byte[] _bytes = new byte[ushort.MaxValue];

    public byte this[ushort address]
    {
        get => _bytes[address];
        set => _bytes[address] = value;
    }

    public byte[] this[Range addresses] => _bytes[addresses];

    public byte ReadByte(ushort address) => this[address];

    public ushort ReadWord(ushort address)
    {
        var lo = ReadByte(address);
        var hi = ReadByte((ushort)(address + 1));
        return (ushort)(lo | (hi << 8));
    }

    public void WriteByte(ushort address, byte value) => this[address] = value;

    public void WriteWord(ushort address, ushort value)
    {
        WriteByte(address, (byte)value);
        WriteByte((ushort)(address + 1), (byte)(value >> 8));
    }
}