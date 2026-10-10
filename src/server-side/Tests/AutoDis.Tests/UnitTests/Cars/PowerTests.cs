using AutoDis.Domain.Cars;

namespace AutoDis.Tests.UnitTests.Cars;

public class PowerTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(150)]
    [InlineData(3000)]
    public void Ctor_ValidHorsepower_SetsValue(int hp)
    {
        var power = new Power(hp);

        Assert.Equal(hp, power.Horsepower);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(3001)]
    public void Ctor_HorsepowerOutOfRange_Throws(int hp)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Power(hp));
    }

    [Fact]
    public void Equals_SameHorsepower_AreEqual()
    {
        var a = new Power(150);
        var b = new Power(150);

        Assert.True(a.Equals(b));
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void Equals_DifferentHorsepower_AreNotEqual()
    {
        Assert.False(new Power(150).Equals(new Power(151)));
    }

    [Fact]
    public void Equals_NullOrOtherType_ReturnsFalse()
    {
        var power = new Power(1);

        Assert.False(power.Equals(null!));
        Assert.False(power.Equals(new Mileage(1f)));
    }

    [Fact]
    public void ToString_ReturnsHorsepowerWithUnit()
    {
        Assert.Equal("150 hp", new Power(150).ToString());
    }
}
