namespace CS6502.Emulation;

public static class InstructionSetFactory
{
    public static InstructionSet Mos6502 =>
        new InstructionSet()
            // LDA
            .Add(Opcode.LdaImm, AddressingMode.Immediate)
        ;

    public static InstructionSet Mos65C02 =>
        Mos6502
            // TODO: 65C02-specific instructions.
            // .Add(0x00, AddressingMode.Immediate)
    ;
}