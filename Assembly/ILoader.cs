using CS6502.Emulation;

namespace CS6502.Assembly;

public interface ILoader
{
    /// <summary>
    /// Loads a program into memory.
    /// </summary>
    void Load(Program program, Memory memory);
}