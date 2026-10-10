using AutoDis.Domain.Cars;

namespace AutoDis.Tests.UnitTests.Cars;

public class CarCountryTests
{
    [Fact]
    public void Ctor_ValidArguments_SetsProperties()
    {
        var country = new CarCountry("Germany", "DE");

        Assert.Equal("Germany", country.Name);
        Assert.Equal("DE", country.IsoCode);
    }

    [Fact]
    public void Ctor_TrimsNameAndNormalizesIsoCode()
    {
        var country = new CarCountry("  Germany  ", " de ");

        Assert.Equal("Germany", country.Name);
        Assert.Equal("DE", country.IsoCode);
    }

    [Theory]
    [InlineData("XX")]
    [InlineData("XK")]
    public void Ctor_ServiceCodes_AreAccepted(string iso)
    {
        var country = new CarCountry("Service", iso);

        Assert.Equal(iso, country.IsoCode);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Ctor_EmptyName_Throws(string? name)
    {
        Assert.Throws<ArgumentException>(() => new CarCountry(name!, "DE"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Ctor_EmptyIsoCode_Throws(string? iso)
    {
        Assert.Throws<ArgumentException>(() => new CarCountry("Germany", iso!));
    }

    [Theory]
    [InlineData("D")]
    [InlineData("DEU")]
    [InlineData("D1")]
    [InlineData("1D")]
    [InlineData("D-")]
    [InlineData("\u0414\u0415")] // Cyrillic ДЕ
    public void Ctor_InvalidIsoCode_Throws(string iso)
    {
        Assert.Throws<ArgumentException>(() => new CarCountry("Germany", iso));
    }
}
