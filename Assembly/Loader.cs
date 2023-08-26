using CS6502.Emulation;

namespace CS6502.Assembly;

public class Loader: ILoader
{
    public void Load(Program program, Memory memory)
    {
        // Populate the reset vector with the address of the first instruction.
        memory.WriteWord(0xFFFC, program.Org);

        // Write the program to memory, placing the first instruction at the address
        // pointed to by the reset vector.
        var address = program.Org;
        foreach (var b in program.Bytes)
        {
            memory[address++] = b;
        }
    }
}