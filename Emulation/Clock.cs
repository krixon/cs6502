namespace CS6502.Emulation;

public class Clock
{
    /// <summary>
    /// The frequency of the clock in milliseconds per tick.
    /// If 0, the clock runs at maximum speed.
    /// </summary>
    public int Frequency { get; set; }

    /// <summary>
    /// The number of cycles taken to initialise the CPU to the point
    /// it can begin executing program instructions.
    /// </summary>
    public int StartupCycles { get; internal set; }

    /// <summary>
    /// The number of clock cycles which have occurred.
    /// This includes the cycles used to initialise the processor.
    /// </summary>
    public int Cycles { get; private set; }

    /// <summary>
    /// The number of clock cycles which have occured during execution of
    /// program instructions.
    /// This does not include the cycles used to initialise the processor.
    /// </summary>
    public int ProgramCycles => Cycles - StartupCycles;

    public Clock() : this(0, 0)
    {
    }

    public Clock(int frequency) : this(0, frequency)
    {
    }

    public Clock(int startupCycles, int frequency)
    {
        StartupCycles = startupCycles;
        Frequency = frequency;
    }

    internal void Tick(int cycles = 1)
    {
        Cycles += cycles;

        if (Frequency > 0)
        {
            Thread.Sleep(Frequency * cycles);
        }
    }

    internal void Reset()
    {
        Cycles = 0;
    }
}