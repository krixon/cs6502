namespace CS6502.Emulation;

public interface IClock
{
    int Cycles { get; }
    void Cycle();
}