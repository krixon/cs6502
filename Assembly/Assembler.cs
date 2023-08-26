using CS6502.Emulation;

namespace CS6502.Assembly;

public class Assembler : IAssembler
{
    private readonly ushort _org;
    private readonly List<byte> _bytes = new();

    public Assembler(ushort org)
    {
        _org = org;
    }

    public void LDA_IMM(byte value)
    {
        _bytes.Add((byte)Opcode.LdaImm);
        _bytes.Add(value);
    }

    public void LDA_IMM(sbyte value) => LDA_IMM((byte)value);

    public void LDX_IMM(byte value)
    {
        _bytes.Add((byte)Opcode.LdxImm);
        _bytes.Add(value);
    }

    public void LDX_IMM(sbyte value) => LDX_IMM((byte)value);

    public void BRK() => _bytes.Add((byte)Opcode.Brk);

    public Program Assemble() => new(_org, _bytes.ToArray());
}