using System.Text;
using CS6502.Assembly;
using CS6502.Console;
using CS6502.Emulation;
using ProcessorFormatter = CS6502.Console.ProcessorFormatter;

var asm = new Assembler(0x800);
var loader = new Loader();
var ram = new Memory();
var cpu = new Processor();
var cpuFormatter = new ProcessorFormatter();
var ramFormatter = new MemoryFormatter();

cpu.Clock.Frequency = 500;

var run = true;

cpu.InstructionExecuted += (_, args) =>
{
    var sb = new StringBuilder();

    sb.AppendLine($"Executed {args.Execution.Opcode}!\n");
    sb.AppendLine(cpuFormatter, $"{args.Processor:full}\n");
    sb.Append(ramFormatter, $"{args.Memory:$800}");

    Update(sb.ToString());

    if (run)
    {
        return;
    }

    // TODO: Reserve the last line for a prompt.
    //       Display output above the prompt, filling any blank lines between end of output and prompt.
    //       Read commands from the prompt and update output.
    //       Commands:
    //       s: Step (execute 1 instruction then wait).
    //       r: Run (execute instructions, int arg is number of ms to sleep between each execution).
    // Some of this stuff will need support from the processor, e.g. SetClockSpeed(ms) for sleeping.
    // Probably want to not block the prompt too so may need a thread.

    Console.WriteLine("\n\nSpace: Step");
    Console.WriteLine("Enter: Run");
    Console.WriteLine("M: Show memory contents");
    Console.Write("> ");

    var keyInfo = Console.ReadKey();
    Console.WriteLine();

    switch (keyInfo.Key) {
        case ConsoleKey.Spacebar:
            break;
        case ConsoleKey.Enter:
            run = true;
            break;
        case ConsoleKey.M:
            // TODO
            break;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
};

asm.LDA_IMM(-42);
asm.LDX_IMM(42);
asm.BRK();

loader.Load(asm.Assemble(), ram);

cpu.Execute(ram);

return;

void Update(string buffer)
{
    Console.Clear();
    Console.SetCursorPosition(0, 0);
    Console.Write(buffer);
}
