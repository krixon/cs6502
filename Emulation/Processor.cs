namespace CS6502.Emulation;

public class Processor
{
    public event EventHandler<ExecutionEventArgs>? InstructionExecuted;

    public Clock Clock { get; }
    public ushort ProgramCounter { get; private set; } = 0xFFFC;
    public byte A { get; private set; }
    public byte X { get; private set; }
    public byte Y { get; private set; }
    public byte StackPointer { get; private set; } = 0xFF;
    public int InstructionsExecuted { get; private set; }
    public bool Carry => Status.HasFlag(Flags.Carry);
    public bool Zero => Status.HasFlag(Flags.Zero);
    public bool Interrupt => Status.HasFlag(Flags.Interrupt);
    public bool Decimal => Status.HasFlag(Flags.Decimal);
    public bool Break => Status.HasFlag(Flags.Break);
    public bool Overflow => Status.HasFlag(Flags.Overflow);
    public bool Negative => Status.HasFlag(Flags.Negative);

    private Flags Status { get; set; }

    private bool _executing;

    public Processor() : this(new Clock())
    {
    }

    public Processor(Clock clock)
    {
        // Fake the 8 startup cycles.
        clock.StartupCycles = 8;
        Clock = clock;
    }

    public void Execute(Memory memory)
    {
        if (_executing)
        {
            throw new InvalidOperationException("Cannot begin execution as already executing.");
        }

        // An active-low reset line allows to hold the processor in a known disabled
        // state, while the system is initialized. As the reset line goes high, the
        // processor performs a start sequence of 7 cycles, at the end of which the
        // program counter (PC) is read from the address provided in the 16-bit reset
        // vector at $FFFC (LB-HB). Then, at the eighth cycle, the processor transfers
        // control by performing a JMP to the provided address.
        // Any other initialisations are left to the thus executed program. (Notably,
        // instructions exist for the initialisation and loading of all registers, except
        // for the program counter, which is provided by the reset vector at $FFFC).

        Reset();
        _executing = true;

        // Set the program counter to the start of the program.
        ProgramCounter = memory.ReadWord(ProgramCounter);

        // Execute until the program terminates.
        // In the 6502 assembly language, the CPU doesn't inherently "know" when a program terminates.
        // The termination of a program is typically managed by the programmer through various means:
        //
        // Halt Instruction: You can use a specific assembly instruction that effectively halts the CPU,
        // signaling the end of the program's execution. For example, the instruction BRK (break) can be
        // used to stop the program's execution and initiate an interrupt routine.
        //
        // Loop with an Infinite Loop: You can create an infinite loop using a jump instruction (e.g., JMP or BRA)
        // that redirects the program's execution to a specific location in memory. This loop would effectively
        // result in the program running indefinitely until manually interrupted.
        //
        // Return from Main Routine: If your program is organized into subroutines, you might have a "main"
        // routine that starts the execution. You can use a return instruction (e.g., RTS for return from subroutine)
        // at the end of the main routine to exit the program.
        //
        // Software Interrupt: You can use a software interrupt (e.g., BRK or SWI) to signal the end of the program.
        // When the interrupt routine is executed, it can perform cleanup tasks and halt the program.

        while (_executing)
        {
            var previousCycles = Clock.Cycles;
            var opcode = FetchOpcode(memory);

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
                    _executing = false;
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
                    Lda(memory, AddressMode.Absolute);
                    break;
                case Opcode.LdaAbsX:
                    Lda(memory, AddressMode.AbsoluteX);
                    break;
                case Opcode.LdaAbsY:
                    Lda(memory, AddressMode.AbsoluteY);
                    break;
                case Opcode.LdaImm:
                    Lda(memory, AddressMode.Immediate);
                    break;
                case Opcode.LdaIndX:
                    Lda(memory, AddressMode.IndirectX);
                    break;
                case Opcode.LdaIndY:
                    Lda(memory, AddressMode.IndirectY);
                    break;
                case Opcode.LdaZp:
                    break;
                case Opcode.LdaZpX:
                    break;
                case Opcode.LdxAbs:
                    break;
                case Opcode.LdxImm:
                    X = FetchByte(memory);
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
            var instructionCycles = Clock.Cycles - previousCycles;
            var execution = new Execution(opcode, instructionCycles);

            OnInstructionExecuted(new ExecutionEventArgs(this, memory, execution));
        }
    }

    private void Lda(Memory memory, AddressMode addressMode) => LoadAccumulator(memory, addressMode);

    private void Reset()
    {
        Clock.Reset();
        ProgramCounter = 0xFFFC;
        StackPointer = 0xFF;
        Status = Flags.None;
        A = X = Y = 0;
        _executing = false;
    }

    private void LoadAccumulator(Memory memory, AddressMode addressMode)
    {
        A = ReadByte(memory, addressMode);
        SetNegativeAndZeroFlags(A);
    }

    private void LoadX(Memory memory, AddressMode addressMode)
    {
        X = ReadByte(memory, addressMode);
        SetNegativeAndZeroFlags(X);
    }

    private void LoadY(Memory memory, AddressMode addressMode)
    {
        Y = ReadByte(memory, addressMode);
        SetNegativeAndZeroFlags(Y);
    }

    private Opcode FetchOpcode(Memory memory)
    {
        var opcodeAddress = ProgramCounter;
        var rawOpcode = FetchByte(memory);

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
    private byte FetchByte(Memory memory)
    {
        return ReadByte(memory, ProgramCounter++);
    }

    /// <summary>
    /// Fetches a word from memory using the program counter.
    /// This operation takes 2 cycles.
    /// </summary>
    private ushort FetchWord(Memory memory)
    {
        var data = ReadWord(memory, ProgramCounter);
        ProgramCounter += 2;
        return data;
    }

    /// <summary>
    /// Reads a byte from memory at the specified address.
    /// This operation takes 1 cycle.
    /// </summary>
    private byte ReadByte(Memory memory, ushort address)
    {
        var data = memory[address];
        Clock.Tick();
        return data;
    }

    /// <summary>
    /// Reads a byte from memory at the address resolved using the specified address mode.
    /// This number of cycles taken depends on the address mode.
    /// </summary>
    private byte ReadByte(Memory memory, AddressMode mode)
        => mode == AddressMode.Immediate ? FetchByte(memory) : ReadByte(memory, Address(mode, memory));

    /// <summary>
    /// Reads a word from memory at the specified address.
    /// This operation takes 2 cycles.
    /// </summary>
    private ushort ReadWord(Memory memory, ushort address)
    {
        var value = memory.ReadWord(address);
        Clock.Tick(2);
        return value;
    }

    private ushort Address(AddressMode mode, Memory memory)
    {
        return mode switch
        {
            AddressMode.Absolute => AbsoluteAddress(memory),
            AddressMode.AbsoluteX => AbsoluteAddress(memory, X),
            AddressMode.AbsoluteY => AbsoluteAddress(memory, Y),
            AddressMode.Immediate
                => throw new InvalidOperationException("Immediate addressing does not use a memory address."),
            AddressMode.Implied
                => throw new InvalidOperationException("Implied addressing does not use a memory address."),
            AddressMode.Indirect => IndirectAddress(memory),
            AddressMode.IndirectX => IndirectXAddress(memory),
            AddressMode.IndirectY => IndirectYAddress(memory),
            AddressMode.ZeroPage => ZeroPageAddress(memory),
            AddressMode.ZeroPageX => ZeroPageAddress(memory, X),
            AddressMode.ZeroPageY => ZeroPageAddress(memory, Y),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, "Unknown address mode.")
        };
    }

    /// <summary>
    /// Provides the abs address of the program counter.
    /// </summary>
    private ushort AbsoluteAddress(Memory memory, byte offset = 0)
    {
        var address = FetchWord(memory);

        if (offset == 0)
        {
            return address;
        }

        var offsetAddress = (ushort)(address + offset);
        HandleCrossedPageBoundary(address, offsetAddress);
        return offsetAddress;
    }

    private ushort IndirectAddress(Memory memory) => ReadWord(memory, AbsoluteAddress(memory));

    private ushort IndirectXAddress(Memory memory)
    {
        var address = FetchByte(memory);
        address += X;
        Clock.Tick();
        return ReadWord(memory, address);
    }

    private ushort IndirectYAddress(Memory memory)
    {
        var address = ReadWord(memory, FetchByte(memory));
        var offsetAddress = (byte)(address + Y);
        HandleCrossedPageBoundary(address, offsetAddress);
        return offsetAddress;
    }

    private ushort ZeroPageAddress(Memory memory, byte offset = 0)
    {
        var address = FetchByte(memory);

        if (offset > 0)
        {
            address += offset;
            Clock.Tick();
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
            Clock.Tick();
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

    private void OnInstructionExecuted(ExecutionEventArgs args) => InstructionExecuted?.Invoke(this, args);

    // TODO: AddressMode, Operands.
    public record Execution(Opcode Opcode, int Cycles);

    public class ExecutionEventArgs : EventArgs
    {
        public Processor Processor { get; }
        public Memory Memory { get; }
        public Execution Execution { get; }

        public ExecutionEventArgs(Processor processor, Memory memory, Execution execution)
        {
            Processor = processor;
            Memory = memory;
            Execution = execution;
        }
    };
}