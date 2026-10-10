using AutoDis.Domain.Details;
using AutoDis.Tests.UnitTests.Cars;

namespace AutoDis.Tests.UnitTests.Details;

public class DetailTests
{
    private static Detail CreateDetail() => new("Brake pad", "Front brake pad", false, 50m);

    [Fact]
    public void Ctor_ValidArguments_SetsProperties()
    {
        var detail = new Detail("Brake pad", "Front brake pad", true, 49.99m);

        Assert.Equal("Brake pad", detail.Name);
        Assert.Equal("Front brake pad", detail.Description);
        Assert.True(detail.IsWorked);
        Assert.Equal(49.99m, detail.Cost);
        Assert.Empty(detail.AvailableCars);
        Assert.Empty(detail.ImagePaths);
    }

    [Fact]
    public void Ctor_TrimsNameAndDescription()
    {
        var detail = new Detail("  Brake pad  ", "  Front brake pad  ", false, 1m);

        Assert.Equal("Brake pad", detail.Name);
        Assert.Equal("Front brake pad", detail.Description);
    }

    [Fact]
    public void Ctor_ZeroCost_IsAllowed()
    {
        var detail = new Detail("Brake pad", "Front brake pad", false, 0m);

        Assert.Equal(0m, detail.Cost);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Ctor_EmptyName_Throws(string? name)
    {
        Assert.Throws<ArgumentException>(() => new Detail(name!, "Description", false, 1m));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Ctor_EmptyDescription_Throws(string? description)
    {
        Assert.Throws<ArgumentException>(() => new Detail("Name", description!, false, 1m));
    }

    [Fact]
    public void Ctor_NegativeCost_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Detail("Name", "Description", false, -1m)
        );
    }

    [Fact]
    public void ChangeDescription_ValidValue_UpdatesAndTrims()
    {
        var detail = CreateDetail();

        detail.ChangeDescription("  Rear brake pad  ");

        Assert.Equal("Rear brake pad", detail.Description);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ChangeDescription_Empty_ThrowsAndKeepsOldValue(string? description)
    {
        var detail = CreateDetail();

        Assert.Throws<ArgumentException>(() => detail.ChangeDescription(description!));
        Assert.Equal("Front brake pad", detail.Description);
    }

    [Fact]
    public void ChangeIsWorked_TogglesFlag()
    {
        var detail = CreateDetail();

        detail.ChangeIsWorked(true);
        Assert.True(detail.IsWorked);

        detail.ChangeIsWorked(false);
        Assert.False(detail.IsWorked);
    }

    [Fact]
    public void AddCar_NewCar_AddsToList()
    {
        var detail = CreateDetail();
        var car = CarFactory.CreateCar(id: 1);

        detail.AddCar(car);

        Assert.Single(detail.AvailableCars);
        Assert.Contains(car, detail.AvailableCars);
    }

    [Fact]
    public void AddCar_SeveralCarsWithDifferentIds_AddsAll()
    {
        var detail = CreateDetail();

        detail.AddCar(CarFactory.CreateCar(id: 1));
        detail.AddCar(CarFactory.CreateCar(id: 2));
        detail.AddCar(CarFactory.CreateCar(id: 3));

        Assert.Equal(3, detail.AvailableCars.Count);
    }

    [Fact]
    public void AddCar_Null_Throws()
    {
        var detail = CreateDetail();

        Assert.Throws<ArgumentNullException>(() => detail.AddCar(null!));
    }

    [Fact]
    public void AddCar_SameCarTwice_Throws()
    {
        var detail = CreateDetail();
        var car = CarFactory.CreateCar(id: 1);
        detail.AddCar(car);

        Assert.Throws<InvalidOperationException>(() => detail.AddCar(car));
        Assert.Single(detail.AvailableCars);
    }

    [Fact]
    public void AddCar_DifferentInstanceWithSameId_Throws()
    {
        var detail = CreateDetail();
        detail.AddCar(CarFactory.CreateCar(id: 7));

        Assert.Throws<InvalidOperationException>(() => detail.AddCar(CarFactory.CreateCar(id: 7)));
    }

    [Fact]
    public void RemoveCar_ExistingCar_RemovesIt()
    {
        var detail = CreateDetail();
        var keep = CarFactory.CreateCar(id: 1);
        var remove = CarFactory.CreateCar(id: 2);
        detail.AddCar(keep);
        detail.AddCar(remove);

        detail.RemoveCar(remove);

        Assert.Single(detail.AvailableCars);
        Assert.Contains(keep, detail.AvailableCars);
        Assert.DoesNotContain(remove, detail.AvailableCars);
    }

    [Fact]
    public void RemoveCar_InstanceWithSameId_RemovesStoredOne()
    {
        var detail = CreateDetail();
        detail.AddCar(CarFactory.CreateCar(id: 5));

        detail.RemoveCar(CarFactory.CreateCar(id: 5));

        Assert.Empty(detail.AvailableCars);
    }

    [Fact]
    public void RemoveCar_Null_Throws()
    {
        var detail = CreateDetail();

        Assert.Throws<ArgumentNullException>(() => detail.RemoveCar(null!));
    }

    [Fact]
    public void RemoveCar_CarNotInList_Throws()
    {
        var detail = CreateDetail();
        detail.AddCar(CarFactory.CreateCar(id: 1));

        Assert.Throws<InvalidOperationException>(
            () => detail.RemoveCar(CarFactory.CreateCar(id: 2))
        );
        Assert.Single(detail.AvailableCars);
    }

    [Fact]
    public void AddImage_ValidPath_AddsTrimmed()
    {
        var detail = CreateDetail();

        detail.AddImage("  /images/pad-1.png  ");

        Assert.Single(detail.ImagePaths);
        Assert.Contains("/images/pad-1.png", detail.ImagePaths);
    }

    [Fact]
    public void AddImage_SeveralDifferentPaths_AddsAll()
    {
        var detail = CreateDetail();

        detail.AddImage("/images/1.png");
        detail.AddImage("/images/2.png");

        Assert.Equal(2, detail.ImagePaths.Count);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void AddImage_EmptyPath_Throws(string? path)
    {
        var detail = CreateDetail();

        Assert.Throws<ArgumentException>(() => detail.AddImage(path!));
        Assert.Empty(detail.ImagePaths);
    }

    [Fact]
    public void AddImage_DuplicateIgnoringCaseAndSpaces_Throws()
    {
        var detail = CreateDetail();
        detail.AddImage("/images/pad-1.png");

        Assert.Throws<InvalidOperationException>(() => detail.AddImage("  /IMAGES/Pad-1.PNG "));
        Assert.Single(detail.ImagePaths);
    }

    [Fact]
    public void RemoveImage_ExistingPath_RemovesIt()
    {
        var detail = CreateDetail();
        detail.AddImage("/images/1.png");
        detail.AddImage("/images/2.png");

        detail.RemoveImage("/images/1.png");

        Assert.Single(detail.ImagePaths);
        Assert.Contains("/images/2.png", detail.ImagePaths);
    }

    [Fact]
    public void RemoveImage_DifferentCaseAndSpaces_RemovesStoredOne()
    {
        var detail = CreateDetail();
        detail.AddImage("/images/1.png");

        detail.RemoveImage("  /IMAGES/1.PNG ");

        Assert.Empty(detail.ImagePaths);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void RemoveImage_EmptyPath_Throws(string? path)
    {
        var detail = CreateDetail();

        Assert.Throws<ArgumentException>(() => detail.RemoveImage(path!));
    }

    [Fact]
    public void RemoveImage_PathNotInList_Throws()
    {
        var detail = CreateDetail();
        detail.AddImage("/images/1.png");

        Assert.Throws<InvalidOperationException>(() => detail.RemoveImage("/images/2.png"));
        Assert.Single(detail.ImagePaths);
    }
}
