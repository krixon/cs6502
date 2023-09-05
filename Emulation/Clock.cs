namespace CS6502.Emulation;

public class Clock : IClock
{
    public int Cycles { get; private set; }

    public void Cycle()
    {
        ++Cycles;
    }
}