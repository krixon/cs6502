namespace CS6502.Emulation;

public abstract class Processor : IProcessor
{
    public event EventHandler<EventArgs>? BeforeInstruction;
    public event EventHandler<AfterExecuteEventArgs>? AfterInstruction;

    public ushort ProgramCounter { get; private set; }
    public byte StackPointer { get; private set; }
    public StatusRegister Status { get; } = new();
    public byte A { get; private set; }
    public byte X { get; private set; }
    public byte Y { get; private set; }
    public IMemory Memory { get; }
    public IClock Clock { get; }

    private readonly InstructionSet _instructionSet;

    protected Processor(InstructionSet instructionSet) : this(instructionSet, new Memory(), new Clock())
    {
    }

    protected Processor(InstructionSet instructionSet, IMemory memory, IClock clock)
    {
        _instructionSet = instructionSet;

        Memory = memory;
        Clock = clock;

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

        ProgramCounter = ReadAddress(0xFFFC);
        StackPointer = 0xFF;
        Status.ClearAll();
        A = X = Y = 0;

        // Simulate the 8 startup cycles, including the first JMP.
        Cycle(8);
    }

    public void Step()
    {
        var instruction = Fetch();
        ExecuteWithEvents(instruction);
    }

    public void Interrupt()
    {
        throw new NotImplementedException();
    }

    public void NonMaskableInterrupt()
    {
        throw new NotImplementedException();
    }

    protected void Cycle(int times = 1)
    {
        for (var i = 0; i < times; i++)
        {
            Clock.Cycle();
        }
    }

    private Instruction Fetch()
    {
        // OnBeforeFetch();

        var address = ProgramCounter;
        var opcode = (Opcode)FetchByte();

        if (!_instructionSet.TryGetInstruction(opcode, out var instruction))
        {
            // TODO: Better exception.
            // TODO: What does the processor actually do in this scenario? undefined?
            throw new Exception(
                $"Unsupported opcode ${(byte)opcode:X2} ({opcode.ToString().ToUpperInvariant()}) at ${address:X4}.");
        }

        // OnAfterFetch(instruction);

        return instruction!;
    }

    /// <summary>
    /// Executes an instruction.
    /// </summary>
    private void ExecuteWithEvents(Instruction instruction)
    {
        OnBeforeExecute(instruction);

        var operation = Execute(instruction);

        OnAfterExecute(operation);
    }

    protected virtual Operation Execute(Instruction instruction)
    {
        switch (instruction.Opcode)
        {
            case Opcode.LdaAbs:
            case Opcode.LdaImm:
            case Opcode.LdaAbsX:
            case Opcode.LdaAbsY:
            case Opcode.LdaZp:
            case Opcode.LdaZpX:
            case Opcode.LdaIndX:
            case Opcode.LdaIndY:
                return ExecuteLda(instruction);
            case Opcode.AdcImm:
            case Opcode.AdcZp:
            case Opcode.AdcZpX:
            case Opcode.AdcAbs:
            case Opcode.AdcAbsX:
            case Opcode.AdcAbsY:
            case Opcode.AdcIndX:
            case Opcode.AdcIndY:
                break;
            case Opcode.AndImm:
            case Opcode.AndZp:
            case Opcode.AndZpX:
            case Opcode.AndAbs:
            case Opcode.AndAbsX:
            case Opcode.AndAbsY:
            case Opcode.AndIndX:
            case Opcode.AndIndY:
                break;
            case Opcode.AslA:
            case Opcode.AslZp:
            case Opcode.AslZpX:
            case Opcode.AslAbs:
            case Opcode.AslAbsX:
                break;
            case Opcode.Bcc:
                break;
            case Opcode.Bcs:
                break;
            case Opcode.Beq:
                break;
            case Opcode.BitZp:
            case Opcode.BitAbs:
                break;
            case Opcode.Bmi:
                break;
            case Opcode.Bne:
                break;
            case Opcode.Bpl:
                break;
            case Opcode.Brk:
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
            case Opcode.CmpZp:
            case Opcode.CmpZpX:
            case Opcode.CmpAbs:
            case Opcode.CmpAbsX:
            case Opcode.CmpAbsY:
            case Opcode.CmpIndX:
            case Opcode.CmpIndY:
                break;
            case Opcode.CpxImm:
            case Opcode.CpxZp:
            case Opcode.CpxAbs:
                break;
            case Opcode.CpyImm:
            case Opcode.CpyZp:
            case Opcode.CpyAbs:
                break;
            case Opcode.DecZp:
            case Opcode.DecZpX:
            case Opcode.DecAbs:
            case Opcode.DecAbsX:
                break;
            case Opcode.Dex:
                break;
            case Opcode.Dey:
                break;
            case Opcode.EorImm:
            case Opcode.EorZp:
            case Opcode.EorZpX:
            case Opcode.EorAbs:
            case Opcode.EorAbsX:
            case Opcode.EorAbsY:
            case Opcode.EorIndX:
            case Opcode.EorIndY:
                break;
            case Opcode.IncZp:
            case Opcode.IncZpX:
            case Opcode.IncAbs:
            case Opcode.IncAbsX:
                break;
            case Opcode.IndX:
            case Opcode.IndY:
                break;
            case Opcode.JmpAbs:
            case Opcode.JmpInd:
                break;
            case Opcode.Jsr:
                break;
            case Opcode.LdxImm:
            case Opcode.LdxZp:
            case Opcode.LdxZpY:
            case Opcode.LdxAbs:
            case Opcode.LdxAbsY:
                break;
            case Opcode.LdyImm:
            case Opcode.LdyZp:
            case Opcode.LdyZpX:
            case Opcode.LdyAbs:
            case Opcode.LdyAbsX:
                break;
            case Opcode.LsrA:
            case Opcode.LsrZp:
            case Opcode.LsrZpX:
            case Opcode.LsrAbs:
            case Opcode.LsrAbsX:
                break;
            case Opcode.Nop:
                break;
            case Opcode.OraImm:
            case Opcode.OraZp:
            case Opcode.OraZpX:
            case Opcode.OraAbs:
            case Opcode.OraAbsX:
            case Opcode.OraAbsY:
            case Opcode.OraIndX:
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
            case Opcode.RolZp:
            case Opcode.RolZpX:
            case Opcode.RolAbs:
            case Opcode.RolAbsX:
                break;
            case Opcode.RorA:
            case Opcode.RorZp:
            case Opcode.RorZpX:
            case Opcode.RorAbs:
            case Opcode.RorAbsX:
                break;
            case Opcode.Rti:
                break;
            case Opcode.Rts:
                break;
            case Opcode.SbcImm:
            case Opcode.SbcZp:
            case Opcode.SbcZpX:
            case Opcode.SbcAbs:
            case Opcode.SbcAbsX:
            case Opcode.SbcAbsY:
            case Opcode.SbcIndX:
            case Opcode.SbcIndY:
                break;
            case Opcode.Sec:
                break;
            case Opcode.Sed:
                break;
            case Opcode.Sei:
                break;
            case Opcode.StaZp:
            case Opcode.StaZpX:
            case Opcode.StaAbs:
            case Opcode.StaAbsX:
            case Opcode.StaAbsY:
            case Opcode.StaIndX:
            case Opcode.StaIndY:
                break;
            case Opcode.StxZp:
            case Opcode.StxZpY:
            case Opcode.StxAbs:
                break;
            case Opcode.StyZp:
            case Opcode.StyZpY:
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
                throw new ArgumentOutOfRangeException();
        }

        throw new Exception($"Unknown opcode {instruction.Opcode}");
    }

    private Operation ExecuteLda(Instruction instruction)
    {
        A = FetchOperand(instruction.AddressingMode);

        SetNegativeAndZeroFlags(A);

        return new Operation(instruction, A);
    }

    /// <summary>
    /// Fetches a byte from memory at the address pointed to by the program counter.
    /// This operation takes 1 cycle.
    /// </summary>
    protected byte FetchByte() => ReadByte(ProgramCounter++);

    /// <summary>
    /// Fetches the address of the next instruction.
    /// After reading, the program counter is incremented.
    /// This operation takes 2 cycles.
    /// </summary>
    private ushort FetchAddress()
    {
        var address = ReadAddress(ProgramCounter);
        ProgramCounter += 2;
        return address;
    }

    /// <summary>
    /// Reads a 16-bit address from memory starting at the specified address.
    /// This operation takes 2 cycles.
    /// </summary>
    private ushort ReadAddress(ushort address)
    {
        var lo = ReadByte(address);
        var hi = ReadByte((ushort)(address + 1));

        Cycle(2);

        return (ushort)(lo | (hi << 8));
    }

    // private ushort ReadAddress(AddressMode mode) => ReadAddress(ResolveAddress(mode));

    /// <summary>
    /// Reads a byte from memory at the specified address.
    /// This operation takes 1 cycle.
    /// </summary>
    protected byte ReadByte(ushort address)
    {
        var data = Memory[address];
        Cycle();
        return data;
    }

    /// <summary>
    /// Reads a byte from memory at the address resolved using the specified addressing mode.
    /// This number of cycles taken depends on the mode.
    /// </summary>
    // protected byte ReadByte(AddressingMode mode) => ReadByte(ResolveAddress(mode));

    private byte FetchOperand(AddressingMode mode)
    {
        // Non-memory.

        if (mode == AddressingMode.Implied)
        {
            throw new InvalidOperationException("Implied address mode does use operands.");
        }

        if (mode == AddressingMode.Accumulator)
        {
            return A;
        }

        if (mode == AddressingMode.Immediate)
        {
            return FetchByte();
        }

        // Memory.

        var address = mode switch
        {
            AddressingMode.Absolute => AbsoluteAddress(),
            AddressingMode.AbsoluteX => AbsoluteAddress(X),
            AddressingMode.AbsoluteY => AbsoluteAddress(Y),
            AddressingMode.Indirect => IndirectAddress(),
            AddressingMode.IndirectX => IndirectXAddress(),
            AddressingMode.IndirectY => IndirectYAddress(),
            // TODO: AddressingMode.Relative => RelativeAddress(),
            AddressingMode.ZeroPage => ZeroPageAddress(),
            AddressingMode.ZeroPageX => ZeroPageAddress(X),
            AddressingMode.ZeroPageY => ZeroPageAddress(Y),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, "Unknown address mode.")
        };

        return ReadByte(address);
    }

    /// <summary>
    /// Provides the abs address.
    /// This is the addre
    /// </summary>
    private ushort AbsoluteAddress(byte offset = 0)
    {
        var address = FetchAddress();

        if (offset == 0)
        {
            return address;
        }

        var offsetAddress = (ushort)(address + offset);
        CycleIfCrossedPageBoundary(address, offsetAddress);
        return offsetAddress;
    }

    private ushort IndirectAddress() => ReadAddress(AbsoluteAddress());

    /// <summary>
    /// Resolves an address using the indirect X addressing mode.
    /// </summary>
    private ushort IndirectXAddress()
    {
        var address = FetchByte();
        address += X;
        Cycle();
        return ReadAddress(address);
    }

    /// <summary>
    /// Resolves an address using the indirect Y addressing mode.
    /// </summary>
    private ushort IndirectYAddress()
    {
        var address = ReadAddress(FetchByte());
        var offsetAddress = (byte)(address + Y);
        CycleIfCrossedPageBoundary(address, offsetAddress);
        return offsetAddress;
    }

    /// <summary>
    /// Resolves an address using the zero-page addressing mode.
    /// </summary>
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
    private void CycleIfCrossedPageBoundary(ushort startAddress, ushort endAddress)
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

    private void SetNegativeFlag(byte value) => Status.Negative = IsNegative(value);

    private void SetZeroFlag(byte value) => Status.Zero = value == 0;

    private static bool IsNegative(byte value) => IsNthBitSet(value, 7);

    private static bool IsNthBitSet(byte value, int n)
    {
        if (n is < 0 or > 7)
        {
            throw new ArgumentOutOfRangeException(nameof(n), "Bit index must be between 0 and 7.");
        }

        return (value & (byte)(1 << n)) != 0;
    }

    private void OnBeforeExecute(Instruction instruction) =>
        BeforeInstruction?.Invoke(this, new BeforeExecuteEventArgs(instruction));

    private void OnAfterExecute(Operation operation) =>
        AfterInstruction?.Invoke(this, new AfterExecuteEventArgs(operation));
}