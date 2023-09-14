using CS6502.Emulation;

namespace CS6502.Assembly;

public class Loader : ILoader
{
    public void Load(Program program, IMemory memory)
    {
        var address = program.Org;

        // Populate the reset vector with the address of the first instruction.
        memory[0xFFFC] = (byte)address;
        memory[0xFFFD] = (byte)(address >> 8);

        // Write the program to memory, placing the first instruction at the address
        // pointed to by the reset vector.
        foreach (var b in program.Bytes)
        {
            memory[address++] = b;
        }
    }
}