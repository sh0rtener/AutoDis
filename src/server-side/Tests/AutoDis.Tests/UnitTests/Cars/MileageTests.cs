using AutoDis.Domain.Cars;

namespace AutoDis.Tests.UnitTests.Cars;

public class MileageTests
{
    [Theory]
    [InlineData(0f)]
    [InlineData(150_000.5f)]
    [InlineData(10_000_000f)]
    public void Ctor_ValidKm_SetsValue(float km)
    {
        var mileage = new Mileage(km);

        Assert.Equal(km, mileage.Km);
    }

    [Theory]
    [InlineData(-0.1f)]
    [InlineData(-1f)]
    [InlineData(10_000_001f)]
    public void Ctor_KmOutOfRange_Throws(float km)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Mileage(km));
    }

    [Theory]
    [InlineData(float.NaN)]
    [InlineData(float.PositiveInfinity)]
    [InlineData(float.NegativeInfinity)]
    public void Ctor_NotAValidNumber_Throws(float km)
    {
        Assert.Throws<ArgumentException>(() => new Mileage(km));
    }

    [Fact]
    public void Equals_SameKm_AreEqual()
    {
        var a = new Mileage(1000f);
        var b = new Mileage(1000f);

        Assert.True(a.Equals(b));
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void Equals_DifferentKm_AreNotEqual()
    {
        Assert.False(new Mileage(1000f).Equals(new Mileage(1001f)));
    }
}
