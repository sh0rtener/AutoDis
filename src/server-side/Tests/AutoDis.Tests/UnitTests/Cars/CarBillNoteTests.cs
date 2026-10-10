using AutoDis.Domain.Cars;

namespace AutoDis.Tests.UnitTests.Cars;

public class CarBillNoteTests
{
    [Fact]
    public void Ctor_ValidArguments_SetsProperties()
    {
        var note = new CarBillNote("Oil change", 99.9m, true);

        Assert.Equal("Oil change", note.Name);
        Assert.Equal(99.9m, note.Cost);
        Assert.True(note.IsAvailableForSale);
    }

    [Fact]
    public void Ctor_DefaultsToNotAvailableForSale()
    {
        var note = new CarBillNote("Oil change", 10m);

        Assert.False(note.IsAvailableForSale);
    }

    [Fact]
    public void Ctor_TrimsName()
    {
        var note = new CarBillNote("  Oil change  ", 10m);

        Assert.Equal("Oil change", note.Name);
    }

    [Fact]
    public void Ctor_ZeroCost_IsAllowed()
    {
        var note = new CarBillNote("Free check", 0m);

        Assert.Equal(0m, note.Cost);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Ctor_EmptyName_Throws(string? name)
    {
        Assert.Throws<ArgumentException>(() => new CarBillNote(name!, 10m));
    }

    [Fact]
    public void Ctor_NegativeCost_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new CarBillNote("Oil change", -0.01m));
    }

    [Fact]
    public void ChangeName_ValidName_UpdatesAndTrims()
    {
        var note = new CarBillNote("Oil change", 10m);

        note.ChangeName("  Brake pads  ");

        Assert.Equal("Brake pads", note.Name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ChangeName_EmptyName_ThrowsAndKeepsOldValue(string? name)
    {
        var note = new CarBillNote("Oil change", 10m);

        Assert.Throws<ArgumentException>(() => note.ChangeName(name!));
        Assert.Equal("Oil change", note.Name);
    }

    [Fact]
    public void ChangeCost_ValidCost_Updates()
    {
        var note = new CarBillNote("Oil change", 10m);

        note.ChangeCost(25.5m);

        Assert.Equal(25.5m, note.Cost);
    }

    [Fact]
    public void ChangeCost_NegativeCost_ThrowsAndKeepsOldValue()
    {
        var note = new CarBillNote("Oil change", 10m);

        Assert.Throws<ArgumentOutOfRangeException>(() => note.ChangeCost(-1m));
        Assert.Equal(10m, note.Cost);
    }

    [Fact]
    public void MakeAvailableForSale_SetsFlag()
    {
        var note = new CarBillNote("Oil change", 10m);

        note.MakeAvailableForSale();

        Assert.True(note.IsAvailableForSale);
    }

    [Fact]
    public void MakeUnavailableForSale_ClearsFlag()
    {
        var note = new CarBillNote("Oil change", 10m, true);

        note.MakeUnavailableForSale();

        Assert.False(note.IsAvailableForSale);
    }
}
