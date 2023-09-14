using CS6502.Emulation;
using FluentAssertions;

namespace Emulation.Tests;

public class Mos6502Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ItSetsRegistersToInitialStateOnReset()
    {
        var mpu = new Mos6502();

        mpu.Reset();

        mpu.StackPointer.Should().Be(0xFF);
        mpu.A.Should().Be(0);
        mpu.X.Should().Be(0);
        mpu.Y.Should().Be(0);
    }
}