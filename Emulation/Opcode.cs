namespace CS6502.Emulation;

public enum Opcode : byte
{
    // Flags legend:
    // +  ... modified
    // -  ... not modified
    // 1  ... set
    // 0  ... cleared
    // M6 ... memory bit 6
    // M7 ... memory bit 7

    // ADC - Add memory to accumulator with carry.
    // A + M + C -> A, C
    // N Z C I D V
    // + + + - - +
    AdcImm = 0x69,
    AdcZp = 0x65,
    AdcZpX = 0x75,
    AdcAbs = 0x6D,
    AdcAbsX = 0x7D,
    AdcAbsY = 0x79,
    AdcIndX = 0x61,
    AdcIndY = 0x71,

    // AND - AND memory with accumulator.
    // A AND M -> A
    // N Z C I D V
    // + + - - - -
    AndImm = 0x29,
    AndZp = 0x25,
    AndZpX = 0x35,
    AndAbs = 0x2D,
    AndAbsX = 0x3D,
    AndAbsY = 0x39,
    AndIndX = 0x21,
    AndIndY = 0x31,

    // ASL - Shift left one bit (memory or accumulator).
    // C <- [76543210] <- 0
    // N Z C I D V
    // + + + - - -
    AslA = 0x0A,
    AslZp = 0x06,
    AslZpX = 0x16,
    AslAbs = 0x0E,
    AslAbsX = 0x1E,

    // BCC - Branch on carry clear.
    // branch on C = 0
    // N Z C I D V
    // - - - - - -
    Bcc = 0x90,

    // BCS - Branch on carry set.
    // branch on C = 1
    // N Z C I D V
    // - - - - - -
    Bcs = 0xB0,

    // BCS - Branch on result zero.
    // branch on Z = 1
    // N Z C I D V
    // - - - - - -
    Beq = 0xF0,

    // BIT - Test bits in memory with accumulator.
    // Bits 7 and 6 of operand are transferred to bit 7 and 6 of SR (N,V);
    // the zero-flag is set according to the result of the operand AND
    // the accumulator (set if the result is zero, unset otherwise).
    // This allows a quick check of a few bits at once without affecting
    // any of the registers, other than the status register (SR).
    // A AND M, M7 -> N, M6 -> V
    //  N Z C I D  V
    // M7 - - - - M6
    BitZp = 0x24,
    BitAbs = 0x2C,

    // BMI - Branch on result minus.
    // branch on N = 1
    // N Z C I D V
    // - - - - - -
    Bmi = 0x30,

    // BNE - Branch on result not zero.
    // branch on Z = 0
    // N Z C I D V
    // - - - - - -
    Bne = 0xD0,

    // BPL - Branch on result plus.
    // branch on N = 0
    // N Z C I D V
    // - - - - - -
    Bpl = 0x10,

    // BRL - Force break.
    // BRK initiates a software interrupt similar to a hardware
    // interrupt (IRQ). The return address pushed to the stack is
    // PC+2, providing an extra byte of spacing for a break mark
    // (identifying a reason for the break).
    // The status register will be pushed to the stack with the break
    // flag set to 1. However, when retrieved during RTI or by a PLP
    // instruction, the break flag will be ignored.
    // The interrupt disable flag is not set automatically.
    // interrupt, push PC+2, push SR
    // N Z C I D V
    // - - - 1 - -
    Brk = 0x0,

    // BVS - Branch on overflow set.
    // branch on V = 1
    // N Z C I D V
    // - - - - - -
    Bvs = 0x70,

    // BVC - Branch on overflow clear.
    // branch on V = 0
    // N Z C I D V
    // - - - - - -
    Bvc = 0x50,

    // CLC - Clear carry flag.
    // 0 -> C
    // N Z C I D V
    // - - 0 - - -
    Clc = 0x18,

    // CLD - Clear decimal mode.
    // 0 -> D
    // N Z C I D V
    // - - - - 0 -
    Cld = 0xD8,

    // CLI - Clear interrupt disable flag.
    // 0 -> I
    // N Z C I D V
    // - - - 0 - -
    Cli = 0x58,

    // CLV - Clear overflow flag.
    // 0 -> V
    // N Z C I D V
    // - - - - - 0
    Clv = 0xB8,

    // CMP - Compare memory with accumulator.
    // A - M
    // N Z C I D V
    // + + + - - -
    CmpImm = 0xC9,
    CmpZp = 0xC5,
    CmpZpX = 0xD5,
    CmpAbs = 0xCD,
    CmpAbsX = 0xDD,
    CmpAbsY = 0xD9,
    CmpIndX = 0xC1,
    CmpIndY = 0xD1,

    // CPX
    // Compare memory with X.
    // X - M
    // N Z C I D V
    // + + + - - -
    CpxImm = 0xE0,
    CpxZp = 0xE4,
    CpxAbs = 0xEC,

    // CPY
    // Compare memory with Y.
    // Y - M
    // N Z C I D V
    // + + + - - -
    CpyImm = 0xC0,
    CpyZp = 0xC4,
    CpyAbs = 0xCC,

    // DEC
    // Decrement memory by one.
    // M - 1 -> M
    // N Z C I D V
    // + + - - - -
    DecZp = 0xC6,
    DecZpX = 0xD6,
    DecAbs = 0xCE,
    DecAbsX = 0xDE,
    
    // DEX
    // Decrement X by one.
    // X - 1 -> X
    // N Z C I D V
    // + + - - - -
    Dex = 0xCA,
    
    // DEY
    // Decrement Y by one.
    // Y - 1 -> Y
    // N Z C I D V
    // + + - - - -
    Dey = 0x88,
    
    // EOR
    // EOR memory with accumulator.
    // A EOR M -> A
    // N Z C I D V
    // + + - - - -
    EorImm = 0x49,
    EorZp = 0x45,
    EorZpX = 0x55,
    EorAbs = 0x4D,
    EorAbsX = 0x5D,
    EorAbsY = 0x59,
    EorIndX = 0x41,
    EorIndY = 0x51,
    
    // INC
    // Increment memory by one.
    // M + 1 -> M
    // N Z C I D V
    // + + - - - -
    IncZp = 0xE6,
    IncZpX = 0xF6,
    IncAbs = 0xEE,
    IncAbsX = 0xFE,
    
    // INX
    // Increment X by one.
    // X + 1 -> X
    // N Z C I D V
    // + + - - - -
    IndX = 0xE8,
    
    // INY
    // Increment Y by one.
    // Y + 1 -> Y
    // N Z C I D V
    // + + - - - -
    IndY = 0xC8,

    // JMP
    // Jump to new location.
    // (PC+1) -> PCL, (PC+2) -> PCH
    // N Z C I D V
    // - - - - - -
    JmpAbs = 0x4C,
    JmpInd = 0x6C,

    // JSR
    // Jump to new location saving return address.
    // push (PC+2), (PC+1) -> PCL, (PC+2) -> PCH
    // N Z C I D V
    // - - - - - -
    Jsr = 0x20,

    // LDA
    // Load accumulator with memory.
    // M -> A
    // N Z C I D V
    // + + - - - -
    LdaAbs = 0xAD,
    LdaAbsX = 0xBD,
    LdaAbsY = 0xB9,
    LdaImm = 0xA9,
    LdaIndX = 0xA1,
    LdaIndY = 0xB1,
    LdaZp = 0xA5,
    LdaZpX = 0xB5,

    // LDX
    // Load X with memory.
    // M -> X
    // N Z C I D V
    // + + - - - -
    LdxImm = 0xA2,
    LdxZp = 0xA6,
    LdxZpY = 0xB6,
    LdxAbs = 0xAE,
    LdxAbsY = 0xBE,
    
    // LDY
    // Load Y with memory.
    // M -> Y
    // N Z C I D V
    // + + - - - -
    LdyImm = 0xA0,
    LdyZp = 0xA4,
    LdyZpX = 0xB4,
    LdyAbs = 0xAC,
    LdyAbsX = 0xBC,

    // LSR
    // Shift one bit right (memory or accumulator).
    // 0 -> [76543210] -> C
    // N Z C I D V
    // 0 + + - - -
    LsrA = 0x4A,
    LsrZp = 0x46,
    LsrZpX = 0x56,
    LsrAbs = 0x4E,
    LsrAbsX = 0x5E,
    
    // NOP
    // No operation.
    // ---
    // N Z C I D V
    // - - - - - -
    Nop = 0xEA,
    
    // ORA
    // OR memory with accumulator.
    // A OR M -> A
    // N Z C I D V
    // + + - - - -
    OraImm = 0x09,
    OraZp = 0x05,
    OraZpX = 0x15,
    OraAbs = 0x0D,
    OraAbsX = 0x1D,
    OraAbsY = 0x19,
    OraIndX = 0x01,
    OraIndY = 0x11,

    // PHA
    // Push accumulator onto stack.
    // push A
    // N Z C I D V
    // - - - - - -
    Pha = 0x48,

    // PHP
    // Push processor status onto stack.
    // The status register will be pushed with the break
    // flag and bit 5 set to 1.
    // push A
    // N Z C I D V
    // - - - - - -
    Php = 0x08,
    
    // PLA
    // Pull accumulator from stack.
    // push A
    // N Z C I D V
    // - - - - - -
    Pla = 0x68,

    // PLP
    // Pull processor status from stack.
    // The status register will be pushed with the break
    // flag and bit 5 set to 1.
    // push A
    // N Z C I D V
    // - - - - - -
    Plp = 0x28,

    // ROL
    // Roll one bit left (memory or accumulator).
    // C <- [76543210] <- C
    // N Z C I D V
    // + + + - - -
    RolA = 0x2A,
    RolZp = 0x26,
    RolZpX = 0x36,
    RolAbs = 0x2E,
    RolAbsX = 0x3E,

    // ROR
    // Roll one bit right (memory or accumulator).
    // 0 -> [76543210] -> C
    // N Z C I D V
    // 0 + + - - -
    RorA = 0x6A,
    RorZp = 0x66,
    RorZpX = 0x76,
    RorAbs = 0x6E,
    RorAbsX = 0x7E,

    // RTI
    // Return from interrupt.
    // pull SR, pull PC
    // N Z C I D V
    // from stack
    Rti = 0x40,

    // RTS
    // Return from subroutine.
    // pull PC, PC + 1 -> PC
    // N Z C I D V
    // - - - - - -
    Rts = 0x60,

    // SBC
    // Subtract memory from accumulator with borrow.
    // A - M - C̅ -> A
    // N Z C I D V
    // + + + - - +
    SbcImm = 0xE9,
    SbcZp = 0xE5,
    SbcZpX = 0xF5,
    SbcAbs = 0xED,
    SbcAbsX = 0xFD,
    SbcAbsY = 0xF9,
    SbcIndX = 0xE1,
    SbcIndY = 0xF1,
    
    // SEC
    // Set carry flag.
    // 1 -> C
    // N Z C I D V
    // - - 1 - - -
    Sec = 0x38,
    
    // SED
    // Set decimal flag.
    // 1 -> D
    // N Z C I D V
    // - - - - 1 -
    Sed = 0xF8,
    
    // SEI
    // Set interrupt disable flag.
    // 1 -> I
    // N Z C I D V
    // - - - 1 - -
    Sei = 0x78,
    
    // STA
    // Store accumulator in memory.
    // A -> M
    // N Z C I D V
    // - - - - - -
    StaZp = 0x85,
    StaZpX = 0x95,
    StaAbs = 0x8D,
    StaAbsX = 0x9D,
    StaAbsY = 0x99,
    StaIndX = 0x81,
    StaIndY = 0x91,
    
    // STX
    // Store X in memory.
    // X -> M
    // N Z C I D V
    // - - - - - -
    StxZp = 0x86,
    StxZpY = 0x96,
    StxAbs = 0x8E,
    
    // STY
    // Store Y in memory.
    // Y -> M
    // N Z C I D V
    // - - - - - -
    StyZp = 0x84,
    StyZpY = 0x94,
    StyAbs = 0x8C,
    
    // TAX
    // Transfer accumulator to X.
    // A -> X
    // N Z C I D V
    // + + - - - -
    Tax = 0xAA,
    
    // TAY
    // Transfer accumulator to Y.
    // A -> Y
    // N Z C I D V
    // + + - - - -
    Tay = 0xA8,
    
    // TSX
    // Transfer stack pointer to X.
    // SP -> X
    // N Z C I D V
    // + + - - - -
    Tsx = 0xBA,
    
    // TXA
    // Transfer X to accumulator.
    // X -> A
    // N Z C I D V
    // + + - - - -
    Txa = 0x8A,

    // TXS
    // Transfer X to stack register.
    // X -> SP
    // N Z C I D V
    // - - - - - -
    Txs = 0x9A,

    // TYA
    // Transfer Y to accumulator.
    // Y -> A
    // N Z C I D V
    // + + - - - -
    Tya = 0x98,
}