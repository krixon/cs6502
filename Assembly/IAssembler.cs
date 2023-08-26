namespace CS6502.Assembly;

public interface IAssembler
{
    /// <summary>
    /// Assembles 6502 machine code.
    /// </summary>
    Program Assemble();
}