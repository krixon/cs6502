using System.Text;
using CS6502.Emulation;

namespace CS6502.Console;

public class MemoryFormatter : ICustomFormatter, IFormatProvider
{
    private const int Width = 8;
    private const int DefaultStart = 0;
    private const int DefaultLength = 64;

    public string Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        if (arg is not Memory memory)
        {
            return $"{arg}";
        }

        var parts = format?.Split(',') ?? Array.Empty<string>();
        var start = DefaultStart;
        var length = DefaultLength;

        if (parts.Length >= 1)
        {
            start = ParseNumber(parts[0]);
        }

        if (parts.Length >= 2)
        {
            length = ParseNumber(parts[1]);
        }

        // Round length to the nearest WIDTH bytes.
        length = (length + Width - 1) / Width * Width;

        var bytes = memory[start..(start + length)];
        var sb = new StringBuilder();

        for (var i = 0; i < length; i += Width)
        {
            // Row prefix (first memory address).
            sb.Append($"${start + i:X4} ");

            var line = bytes.Skip(i).Take(Width).ToArray();

            // Hex values.
            foreach (var b in line)
            {
                sb.Append($"{b:X2} ");
            }

            // Ascii representation.
            foreach (var b in line)
            {
                sb.Append(b is >= 32 and <= 126 ? (char)b : '.');
            }

            // Newline unless last line.
            if (i + Width < length)
            {
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }

    public object? GetFormat(Type? formatType)
    {
        return formatType == typeof(ICustomFormatter) ? this : null;
    }

    private static int ParseNumber(string input)
    {
        if (StartsWith(input, "0x"))
        {
            return Convert.ToInt32(input, 16);
        }

        if (StartsWith(input, "$"))
        {
            return Convert.ToInt32(input[1..], 16);
        }

        if (StartsWith(input, "0b"))
        {
            return Convert.ToInt32(input[2..], 2);
        }

        if (StartsWith(input, "0"))
        {
            return Convert.ToInt32(input, 8);
        }

        return int.Parse(input);
    }

    private static bool StartsWith(string input, string test)
    {
        return input.StartsWith(test, StringComparison.OrdinalIgnoreCase);
    }
}