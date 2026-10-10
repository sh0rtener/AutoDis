using AutoDis.Domain.Cars;

namespace AutoDis.Tests.UnitTests.Cars;

public class VinCodeTests
{
    [Fact]
    public void Ctor_ValidVin_SetsCode()
    {
        var vin = new VinCode(CarFactory.ValidVin);

        Assert.Equal(CarFactory.ValidVin, vin.Code);
    }

    [Fact]
    public void Ctor_LowercaseWithSpaces_NormalizesToUpperTrimmed()
    {
        var vin = new VinCode("  1hgcm82633a004352  ");

        Assert.Equal("1HGCM82633A004352", vin.Code);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Ctor_EmptyVin_Throws(string? code)
    {
        Assert.Throws<ArgumentException>(() => new VinCode(code!));
    }

    [Theory]
    [InlineData("1HGCM82633A00435")] // 16
    [InlineData("1HGCM82633A0043521")] // 18
    public void Ctor_WrongLength_Throws(string code)
    {
        Assert.Throws<ArgumentException>(() => new VinCode(code));
    }

    [Theory]
    [InlineData("IHGCM82633A004352")] // I
    [InlineData("OHGCM82633A004352")] // O
    [InlineData("QHGCM82633A004352")] // Q
    [InlineData("ihgcm82633a004352")] // lowercase i
    [InlineData("1HGCM82633A00435-")] // symbol
    [InlineData("\u0410HGCM82633A004352")] // Cyrillic A
    public void Ctor_InvalidCharacters_Throws(string code)
    {
        Assert.Throws<ArgumentException>(() => new VinCode(code));
    }

    [Fact]
    public void Equals_SameCodeDifferentCase_AreEqual()
    {
        var a = new VinCode("1HGCM82633A004352");
        var b = new VinCode("1hgcm82633a004352");

        Assert.True(a.Equals(b));
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void Equals_DifferentCode_AreNotEqual()
    {
        var a = new VinCode("1HGCM82633A004352");
        var b = new VinCode("1HGCM82633A004353");

        Assert.False(a.Equals(b));
    }
}
