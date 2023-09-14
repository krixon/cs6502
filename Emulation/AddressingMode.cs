namespace CS6502.Emulation;

/// <summary>
/// The addressing mode refers to the source of the data used in an instruction.
///
/// Most of the addressing modes are used to access memory, but Accumulator, Immediate and Implied are not.
///
/// Addressing modes are also categorised as indexed or non-indexed, where indexed modes use the X and Y
/// (index) registers to obtain offsets from a base value.
/// </summary>
public enum AddressingMode
{
    /// <summary>
    /// Operand is a 16-bit memory address immediately following the opcode.
    /// Non-indexed, memory.
    /// </summary>
    /// <example>JMP $4032</example>
    Absolute,

    /// <summary>
    /// As Absolute, but offsets the address by the value of the X register.
    /// Indexed, memory.
    /// </summary>
    /// <example>STA $1000,X</example>
    AbsoluteX,

    /// <summary>
    /// As Absolute, but offsets the address by the value of the Y register.
    /// Indexed, memory.
    /// </summary>
    /// <example>STA $1000,Y</example>
    AbsoluteY,

    /// <summary>
    /// Operand is read from the accumulator.
    /// Non-indexed, non-memory.
    /// </summary>
    /// <example>LSR A</example>
    Accumulator,

    /// <summary>
    /// Operand is the next byte after the opcode.
    /// Non-indexed, non-memory.
    /// </summary>
    /// <example>ORA #$B2</example>
    Immediate,

    /// <summary>
    /// The instruction does not have an explicit operand; the opcode
    /// alone fully defines the instruction.
    /// Non-indexed, non-memory.
    /// </summary>
    /// <example>CLC</example>
    Implied,

    /// <summary>
    /// Operand is a 16-bit memory address immediately following the opcode. A 16-bit address is read
    /// from memory at this address and loaded into the PC.
    /// The JMP instruction is the only instruction that uses this addressing mode. It effectively acts as
    /// a pointer.
    /// Non-indexed, memory.
    /// </summary>
    Indirect,
    IndirectX,
    IndirectY,

    /// <summary>
    /// Only used for branch operations. The byte after the opcode is the branch offset.
    /// If the branch is taken, the new address will be the current PC plus the offset.
    /// The offset is a signed byte, so it can jump a maximum of 127 bytes forward, or 128 bytes backward.
    /// Non-indexed, memory.
    /// </summary>
    Relative,

    /// <summary>
    /// Operand is the last byte of a memory address immediately following the opcode.
    /// The first byte is implied as $00, so given an operand of $FE, the resulting address is $00FE (actually
    /// $FE00 since the architecture is little-endian).
    /// Effectively this is absolute addressing for the first (zero) page of memory, allowing it to be accessed
    /// using a 2-byte instruction rather than the usual 3 bytes required for absolute addressing. This mode
    /// consumes fewer cycles and can result in smaller binaries.
    /// </summary>
    ZeroPage,
    ZeroPageX,
    ZeroPageY,
}