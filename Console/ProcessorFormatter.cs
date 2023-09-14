using System.Text;
using System.Text.RegularExpressions;
using CS6502.Emulation;

namespace CS6502.Console;

public partial class ProcessorFormatter : ICustomFormatter, IFormatProvider
{
    public string Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        format ??= "";

        if (arg is Mos6502 processor)
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

    private static string FormatFull(Mos6502 mos6502)
    {
        var sb = new StringBuilder();

        sb.AppendLine("     HEX   uDEC    sDEC                BIN  CHAR");

        sb.AppendFormat(
            "PC: {0:X4}  {0,5}  {1,6}  {2,17}\n",
            mos6502.ProgramCounter,
            (sbyte)mos6502.ProgramCounter,
            FormatBinary(mos6502.ProgramCounter));

        foreach (var (name, value) in Registers(mos6502))
        {
            sb.AppendFormat(
                "{0,-2}:   {1:X2}  {1,5}  {2,6}  {3,17}  {4}\n",
                name, value, (sbyte)value, FormatBinary(value), value is >= 32 and <= 126 ? (char)value : "");
        }

        var flags = Flags(mos6502);
        sb.AppendLine($"FL: {string.Join(" ", flags.Keys)}");
        sb.Append($"  : {string.Join(" ", flags.Values.Select(b => b ? 1 : 0))}");

        return sb.ToString();
    }

    private static string FormatLine(Mos6502 mos6502)
    {
        var sb = new StringBuilder();

        sb.AppendFormat("PC:{0:X4}[U:{0},S:{1}] ", mos6502.ProgramCounter, (sbyte)mos6502.ProgramCounter);

        foreach (var (name, value) in Registers(mos6502))
        {
            sb.AppendFormat("{0}:{1:X2}[U:{1},S:{2}] ", name, value, (sbyte)value);
        }

        foreach (var (name, value) in Flags(mos6502))
        {
            sb.Append($"{name}:{(value ? 1 : 0)} ");
        }

        sb.Append($"CY:{mos6502.Clock.Cycles} ");
        // sb.Append($"IN:{processor6502.InstructionsExecuted} ");

        return sb.ToString();
    }

    private static Dictionary<string, bool> Flags(Mos6502 mos6502) =>
        new()
        {
            { "N", mos6502.Status.Negative },
            { "V", mos6502.Status.Overflow },
            { "B", mos6502.Status.Break },
            { "D", mos6502.Status.Decimal },
            { "I", mos6502.Status.Interrupt },
            { "Z", mos6502.Status.Zero },
            { "C", mos6502.Status.Carry },
        };

    private static Dictionary<string, byte> Registers(Mos6502 mos6502) =>
        new()
        {
            { "SP", mos6502.StackPointer },
            { "A", mos6502.A },
            { "X", mos6502.X },
            { "Y", mos6502.Y },
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