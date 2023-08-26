namespace CS6502.Assembly;

public class Program
{
    public ushort Org { get; }
    public byte[] Bytes { get; }

    public Program(ushort org, byte[] bytes)
    {
        Org = org;
        Bytes = bytes;
    }
}