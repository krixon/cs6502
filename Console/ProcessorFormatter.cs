using System.Text;
using System.Text.RegularExpressions;
using CS6502.Emulation;

namespace CS6502.Console;

public partial class ProcessorFormatter : ICustomFormatter, IFormatProvider
{
    public string Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        format ??= "";

        if (arg is Processor processor)
        {
            return format.ToLower() switch
            {
                "full" => FormatFull(processor),
                _ => FormatLine(processor)
            };
        }

        return string.Format(format, arg);
    }

    public object? GetFormat(Type? formatType)
    {
        return formatType == typeof(ICustomFormatter) ? this : null;
    }

    private static string FormatFull(Processor processor)
    {
        var sb = new StringBuilder();

        sb.AppendLine("     HEX   uDEC    sDEC                BIN  CHAR");

        sb.AppendFormat(
            "PC: {0:X4}  {0,5}  {1,6}  {2,17}\n",
            processor.ProgramCounter,
            (sbyte)processor.ProgramCounter,
            FormatBinary(processor.ProgramCounter));

        foreach (var (name, value) in Registers(processor))
        {
            sb.AppendFormat(
                "{0,-2}:   {1:X2}  {1,5}  {2,6}  {3,17}  {4}\n",
                name, value, (sbyte)value, FormatBinary(value), value is >= 32 and <= 126 ? (char)value : "");
        }

        var flags = Flags(processor);
        sb.AppendLine($"FL: {string.Join(" ", flags.Keys)}");
        sb.Append($"  : {string.Join(" ", flags.Values.Select(b => b ? 1 : 0))}");

        return sb.ToString();
    }

    private static string FormatLine(Processor processor)
    {
        var sb = new StringBuilder();

        sb.AppendFormat("PC:{0:X4}[U:{0},S:{1}] ", processor.ProgramCounter, (sbyte)processor.ProgramCounter);

        foreach (var (name, value) in Registers(processor))
        {
            sb.AppendFormat("{0}:{1:X2}[U:{1},S:{2}] ", name, value, (sbyte)value);
        }

        foreach (var (name, value) in Flags(processor))
        {
            sb.Append($"{name}:{(value ? 1 : 0)} ");
        }

        sb.Append($"CY:{processor.Cycles}[{processor.ProgramCycles}] ");
        sb.Append($"IN:{processor.InstructionsExecuted} ");

        return sb.ToString();
    }

    private static Dictionary<string, bool> Flags(Processor processor) =>
        new()
        {
            { "N", processor.Negative },
            { "V", processor.Overflow },
            { "B", processor.Break },
            { "D", processor.Decimal },
            { "I", processor.Interrupt },
            { "Z", processor.Zero },
            { "C", processor.Carry },
        };

    private static Dictionary<string, byte> Registers(Processor processor) =>
        new()
        {
            { "SP", processor.StackPointer },
            { "A", processor.A },
            { "X", processor.X },
            { "Y", processor.Y },
        };

    private static string FormatBinary(int value)
    {
        var numBytes = value == 0 ? 1 : (int)Math.Ceiling(Math.Log(value + 1, 256));
        var binary= Convert.ToString(value, 2).PadLeft(numBytes * 8, '0');

        return string.Join(" ", ByteChunkRegex().Matches(binary));
    }

    [GeneratedRegex(".{1,8}")]
    private static partial Regex ByteChunkRegex();
}