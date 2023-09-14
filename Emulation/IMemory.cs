namespace CS6502.Emulation;

public interface IMemory
{
    byte this[ushort address] { get; set; }
}