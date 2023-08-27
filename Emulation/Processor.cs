namespace CS6502.Emulation;

public class Processor
{
    public event EventHandler<BeforeInstructionEventArgs>? BeforeInstruction;
    public event EventHandler<AfterInstructionEventArgs>? AfterInstruction;

    public Memory Memory { get; }
    public ushort ProgramCounter { get; private set; }
    public byte A { get; private set; }
    public byte X { get; private set; }
    public byte Y { get; private set; }
    public byte StackPointer { get; private set; }
    public int InstructionsExecuted { get; private set; }
    public bool Carry => Status.HasFlag(Flags.Carry);
    public bool Zero => Status.HasFlag(Flags.Zero);
    public bool Interrupt => Status.HasFlag(Flags.Interrupt);
    public bool Decimal => Status.HasFlag(Flags.Decimal);
    public bool Break => Status.HasFlag(Flags.Break);
    public bool Overflow => Status.HasFlag(Flags.Overflow);
    public bool Negative => Status.HasFlag(Flags.Negative);
    public int Cycles { get; private set; }
    public int ProgramCycles => Cycles - 8;

    private Flags Status { get; set; }

    public Processor() : this(new Memory())
    {
    }

    public Processor(Memory memory)
    {
        Memory = memory;
        Reset();
    }


    public void Reset()
    {
        // An active-low reset line allows to hold the processor in a known disabled
        // state, while the system is initialized. As the reset line goes high, the
        // processor performs a start sequence of 7 cycles, at the end of which the
        // program counter (PC) is read from the address provided in the 16-bit reset
        // vector at $FFFC (LB-HB). Then, at the eighth cycle, the processor transfers
        // control by performing a JMP to the provided address.
        // Any other initialisations are left to the thus executed program. (Notably,
        // instructions exist for the initialisation and loading of all registers, except
        // for the program counter, which is provided by the reset vector at $FFFC).

        ProgramCounter = Memory.ReadWord(0xFFFC);
        StackPointer = 0xFF;
        Status = Flags.None;
        A = X = Y = 0;
        Cycles = 8;
    }

    public void Step()
    {
        var previousCycles = Cycles;
        var opcode = FetchOpcode();

        OnBeforeInstruction(new BeforeInstructionEventArgs(opcode));

        switch (opcode)
        {
            case Opcode.AdcAbs:
                break;
            case Opcode.AdcImm:
                break;
            case Opcode.AdcIndX:
                break;
            case Opcode.AdcIndY:
                break;
            case Opcode.AdcZp:
                break;
            case Opcode.AdcZpX:
                break;
            case Opcode.AdcAbsX:
                break;
            case Opcode.AdcAbsY:
                break;
            case Opcode.AndImm:
                break;
            case Opcode.AndZp:
                break;
            case Opcode.AndZpX:
                break;
            case Opcode.AndAbs:
                break;
            case Opcode.AndAbsX:
                break;
            case Opcode.AndAbsY:
                break;
            case Opcode.AndIndX:
                break;
            case Opcode.AndIndY:
                break;
            case Opcode.AslA:
                break;
            case Opcode.AslZp:
                break;
            case Opcode.AslZpX:
                break;
            case Opcode.AslAbs:
                break;
            case Opcode.AslAbsX:
                break;
            case Opcode.Bcc:
                break;
            case Opcode.Bcs:
                break;
            case Opcode.Beq:
                break;
            case Opcode.BitZp:
                break;
            case Opcode.BitAbs:
                break;
            case Opcode.Bmi:
                break;
            case Opcode.Bne:
                break;
            case Opcode.Bpl:
                break;
            case Opcode.Brk:
                // For now we use BRK to simply halt execution.
                // TODO: Proper implementation.
                break;
            case Opcode.Bvs:
                break;
            case Opcode.Bvc:
                break;
            case Opcode.Clc:
                break;
            case Opcode.Cld:
                break;
            case Opcode.Cli:
                break;
            case Opcode.Clv:
                break;
            case Opcode.CmpImm:
                break;
            case Opcode.CmpZp:
                break;
            case Opcode.CmpZpX:
                break;
            case Opcode.CmpAbs:
                break;
            case Opcode.CmpAbsX:
                break;
            case Opcode.CmpAbsY:
                break;
            case Opcode.CmpIndX:
                break;
            case Opcode.CmpIndY:
                break;
            case Opcode.CpxImm:
                break;
            case Opcode.CpxZp:
                break;
            case Opcode.CpxAbs:
                break;
            case Opcode.CpyImm:
                break;
            case Opcode.CpyZp:
                break;
            case Opcode.CpyAbs:
                break;
            case Opcode.DecZp:
                break;
            case Opcode.DecZpX:
                break;
            case Opcode.DecAbs:
                break;
            case Opcode.DecAbsX:
                break;
            case Opcode.Dex:
                break;
            case Opcode.Dey:
                break;
            case Opcode.EorImm:
                break;
            case Opcode.EorZp:
                break;
            case Opcode.EorZpX:
                break;
            case Opcode.EorAbs:
                break;
            case Opcode.EorAbsX:
                break;
            case Opcode.EorAbsY:
                break;
            case Opcode.EorIndX:
                break;
            case Opcode.EorIndY:
                break;
            case Opcode.IncZp:
                break;
            case Opcode.IncZpX:
                break;
            case Opcode.IncAbs:
                break;
            case Opcode.IncAbsX:
                break;
            case Opcode.IndX:
                break;
            case Opcode.IndY:
                break;
            case Opcode.JmpAbs:
                break;
            case Opcode.JmpInd:
                break;
            case Opcode.Jsr:
                break;
            case Opcode.LdaAbs:
                Lda(AddressMode.Absolute);
                break;
            case Opcode.LdaAbsX:
                Lda(AddressMode.AbsoluteX);
                break;
            case Opcode.LdaAbsY:
                Lda(AddressMode.AbsoluteY);
                break;
            case Opcode.LdaImm:
                Lda(AddressMode.Immediate);
                break;
            case Opcode.LdaIndX:
                Lda(AddressMode.IndirectX);
                break;
            case Opcode.LdaIndY:
                Lda(AddressMode.IndirectY);
                break;
            case Opcode.LdaZp:
                break;
            case Opcode.LdaZpX:
                break;
            case Opcode.LdxAbs:
                break;
            case Opcode.LdxImm:
                X = FetchByte();
                SetNegativeAndZeroFlags(X);
                break;
            case Opcode.LdxZp:
                break;
            case Opcode.LdxZpY:
                break;
            case Opcode.LdxAbsY:
                break;
            case Opcode.LdyImm:
                break;
            case Opcode.LdyZp:
                break;
            case Opcode.LdyZpX:
                break;
            case Opcode.LdyAbs:
                break;
            case Opcode.LdyAbsX:
                break;
            case Opcode.LsrA:
                break;
            case Opcode.LsrZp:
                break;
            case Opcode.LsrZpX:
                break;
            case Opcode.LsrAbs:
                break;
            case Opcode.LsrAbsX:
                break;
            case Opcode.Nop:
                break;
            case Opcode.OraImm:
                break;
            case Opcode.OraZp:
                break;
            case Opcode.OraZpX:
                break;
            case Opcode.OraAbs:
                break;
            case Opcode.OraAbsX:
                break;
            case Opcode.OraAbsY:
                break;
            case Opcode.OraIndX:
                break;
            case Opcode.OraIndY:
                break;
            case Opcode.Pha:
                break;
            case Opcode.Php:
                break;
            case Opcode.Pla:
                break;
            case Opcode.Plp:
                break;
            case Opcode.RolA:
                break;
            case Opcode.RolZp:
                break;
            case Opcode.RolZpX:
                break;
            case Opcode.RolAbs:
                break;
            case Opcode.RolAbsX:
                break;
            case Opcode.RorA:
                break;
            case Opcode.RorZp:
                break;
            case Opcode.RorZpX:
                break;
            case Opcode.RorAbs:
                break;
            case Opcode.RorAbsX:
                break;
            case Opcode.Rti:
                break;
            case Opcode.Rts:
                break;
            case Opcode.SbcImm:
                break;
            case Opcode.SbcZp:
                break;
            case Opcode.SbcZpX:
                break;
            case Opcode.SbcAbs:
                break;
            case Opcode.SbcAbsX:
                break;
            case Opcode.SbcAbsY:
                break;
            case Opcode.SbcIndX:
                break;
            case Opcode.SbcIndY:
                break;
            case Opcode.Sec:
                break;
            case Opcode.Sed:
                break;
            case Opcode.Sei:
                break;
            case Opcode.StaZp:
                break;
            case Opcode.StaZpX:
                break;
            case Opcode.StaAbs:
                break;
            case Opcode.StaAbsX:
                break;
            case Opcode.StaAbsY:
                break;
            case Opcode.StaIndX:
                break;
            case Opcode.StaIndY:
                break;
            case Opcode.StxZp:
                break;
            case Opcode.StxZpY:
                break;
            case Opcode.StxAbs:
                break;
            case Opcode.StyZp:
                break;
            case Opcode.StyZpY:
                break;
            case Opcode.StyAbs:
                break;
            case Opcode.Tax:
                break;
            case Opcode.Tay:
                break;
            case Opcode.Tsx:
                break;
            case Opcode.Txa:
                break;
            case Opcode.Txs:
                break;
            case Opcode.Tya:
                break;
            default:
                throw new NotImplementedException($"Opcode {opcode:2X} is not implemented.");
        }

        ++InstructionsExecuted;

        // TODO: Include entire instruction, not just opcode. e.g. STA is useful only if we know the address.
        //       Include addressing mode enum.
        var instructionCycles = Cycles - previousCycles;

        OnAfterInstruction(new AfterInstructionEventArgs(opcode, instructionCycles));
    }

    private void Lda(AddressMode addressMode) => LoadAccumulator(addressMode);


    private void LoadAccumulator(AddressMode addressMode)
    {
        A = ReadByte(addressMode);
        SetNegativeAndZeroFlags(A);
    }

    private void LoadX(AddressMode addressMode)
    {
        X = ReadByte(addressMode);
        SetNegativeAndZeroFlags(X);
    }

    private void LoadY(AddressMode addressMode)
    {
        Y = ReadByte(addressMode);
        SetNegativeAndZeroFlags(Y);
    }

    private Opcode FetchOpcode()
    {
        var opcodeAddress = ProgramCounter;
        var rawOpcode = FetchByte();

        if (!Enum.IsDefined(typeof(Opcode), rawOpcode))
        {
            throw new Exception($"Unsupported opcode {rawOpcode:2X} at ${opcodeAddress:4X}.");
        }

        return (Opcode)rawOpcode;
    }

    /// <summary>
    /// Fetches a byte from memory using the program counter.
    /// This operation takes 1 cycle.
    /// </summary>
    private byte FetchByte()
    {
        return ReadByte(ProgramCounter++);
    }

    /// <summary>
    /// Fetches a word from memory using the program counter.
    /// This operation takes 2 cycles.
    /// </summary>
    private ushort FetchWord()
    {
        var data = ReadWord(ProgramCounter);
        ProgramCounter += 2;
        return data;
    }

    /// <summary>
    /// Reads a byte from memory at the specified address.
    /// This operation takes 1 cycle.
    /// </summary>
    private byte ReadByte(ushort address)
    {
        var data = Memory[address];
        Cycle();
        return data;
    }

    /// <summary>
    /// Reads a byte from memory at the address resolved using the specified address mode.
    /// This number of cycles taken depends on the address mode.
    /// </summary>
    private byte ReadByte(AddressMode mode)
        => mode == AddressMode.Immediate ? FetchByte() : ReadByte(Address(mode));

    /// <summary>
    /// Reads a word from memory at the specified address.
    /// This operation takes 2 cycles.
    /// </summary>
    private ushort ReadWord(ushort address)
    {
        var value = Memory.ReadWord(address);
        Cycle(2);
        return value;
    }

    private void Cycle(int times = 1)
    {
        Cycles += times;
    }

    private ushort Address(AddressMode mode)
    {
        return mode switch
        {
            AddressMode.Absolute => AbsoluteAddress(),
            AddressMode.AbsoluteX => AbsoluteAddress(X),
            AddressMode.AbsoluteY => AbsoluteAddress(Y),
            AddressMode.Immediate
                => throw new InvalidOperationException("Immediate addressing does not use a memory address."),
            AddressMode.Implied
                => throw new InvalidOperationException("Implied addressing does not use a memory address."),
            AddressMode.Indirect => IndirectAddress(),
            AddressMode.IndirectX => IndirectXAddress(),
            AddressMode.IndirectY => IndirectYAddress(),
            AddressMode.ZeroPage => ZeroPageAddress(),
            AddressMode.ZeroPageX => ZeroPageAddress(X),
            AddressMode.ZeroPageY => ZeroPageAddress(Y),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, "Unknown address mode.")
        };
    }

    /// <summary>
    /// Provides the abs address of the program counter.
    /// </summary>
    private ushort AbsoluteAddress(byte offset = 0)
    {
        var address = FetchWord();

        if (offset == 0)
        {
            return address;
        }

        var offsetAddress = (ushort)(address + offset);
        HandleCrossedPageBoundary(address, offsetAddress);
        return offsetAddress;
    }

    private ushort IndirectAddress() => ReadWord(AbsoluteAddress());

    private ushort IndirectXAddress()
    {
        var address = FetchByte();
        address += X;
        Cycle();
        return ReadWord(address);
    }

    private ushort IndirectYAddress()
    {
        var address = ReadWord(FetchByte());
        var offsetAddress = (byte)(address + Y);
        HandleCrossedPageBoundary(address, offsetAddress);
        return offsetAddress;
    }

    private ushort ZeroPageAddress(byte offset = 0)
    {
        var address = FetchByte();

        if (offset > 0)
        {
            address += offset;
            Cycle();
        }

        return address;
    }

    /// <summary>
    /// Consumes a cycle if a page boundary was crossed.
    /// </summary>
    private void HandleCrossedPageBoundary(ushort startAddress, ushort endAddress)
    {
        if (PageOffset(startAddress, endAddress) > 0)
        {
            Cycle();
        }
    }

    /// <summary>
    /// Returns the number of pages between two addresses.
    /// </summary>
    private static int PageOffset(ushort fromAddress, ushort toAddress) => (fromAddress ^ toAddress) >> 8;

    private void SetNegativeAndZeroFlags(byte value)
    {
        SetNegativeFlag(value);
        SetZeroFlag(value);
    }

    private void SetNegativeFlag(byte value) => SetFlag(Flags.Negative, IsNegative(value));

    private void SetZeroFlag(byte value) => SetFlag(Flags.Zero, value == 0);

    private void SetFlag(Flags flag, bool set)
    {
        if (set)
        {
            Status |= flag;
        }
        else
        {
            Status &= ~flag;
        }
    }

    private static bool IsNegative(byte value) => IsNthBitSet(value, 7);

    private static bool IsNthBitSet(byte value, int n)
    {
        if (n is < 0 or > 7)
        {
            throw new ArgumentOutOfRangeException(nameof(n), "Bit index must be between 0 and 7.");
        }

        return (value & (byte)(1 << n)) != 0;
    }

    private void OnBeforeInstruction(BeforeInstructionEventArgs args) => BeforeInstruction?.Invoke(this, args);

    private void OnAfterInstruction(AfterInstructionEventArgs args) => AfterInstruction?.Invoke(this, args);
}