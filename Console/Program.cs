using System.Text;
using CS6502.Assembly;
using CS6502.Console;
using CS6502.Emulation;
using ProcessorFormatter = CS6502.Console.ProcessorFormatter;

var asm = new Assembler(0x800);
var loader = new Loader();
var ram = new Memory();
var mpu = new Processor(ram, new Clock());
var cpuFormatter = new ProcessorFormatter();
var ramFormatter = new MemoryFormatter();

var brk = true;

mpu.BeforeInstruction += (_, args) =>
{
    // TODO: Support breaking on RTS and RTI for debugging.
    if (args.Opcode == Opcode.Brk)
    {
        brk = true;
    }
};

mpu.AfterInstruction += (_, args) =>
{
    var sb = new StringBuilder();

    sb.AppendLine($"Executed {args.Opcode.ToString().ToUpperInvariant()}\n");
    sb.AppendLine(cpuFormatter, $"{mpu:full}\n");
    sb.Append(ramFormatter, $"{ram:$800}");

    Update(sb.ToString());
};

asm.LDA_IMM(-42);
asm.LDX_IMM(42);
asm.BRK();

loader.Load(asm.Assemble(), ram);

mpu.Reset();

var end = false;

while (true)
{
    // Prompt to step or run.
    // If step, step then prompt again.
    // If run, loop until BRK.
    // Need to be able to manually interrupt run and prompt.
    // Need to be able to automatically interrupt and prompt via breakpoints (PCs).

    if (brk)
    {
        Console.WriteLine("\n\nSpace: Step, Enter: Run, R: Reset, X: Exit");
        Console.Write("> ");

        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.Spacebar:
                brk = true;
                break;
            case ConsoleKey.Enter:
                brk = false;
                break;
            case ConsoleKey.X:
                end = true;
                break;
            case ConsoleKey.R:
                mpu.Reset();
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                continue;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    if (end)
    {
        break;
    }

    mpu.Step();
}

return;

void Update(string buffer)
{
    Console.Clear();
    Console.SetCursorPosition(0, 0);
    Console.Write(buffer);
}