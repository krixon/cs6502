namespace CS6502.Emulation;

public interface IProcessor
{
    public event EventHandler<EventArgs>? BeforeInstruction;
    public event EventHandler<AfterExecuteEventArgs>? AfterInstruction;

    public ushort ProgramCounter { get; }
    public byte StackPointer { get; }
    public StatusRegister Status { get; }
    public byte A { get; }
    public byte X { get; }
    public byte Y { get; }

    void Reset();
    void Step();
    void Interrupt();
    void NonMaskableInterrupt();
}