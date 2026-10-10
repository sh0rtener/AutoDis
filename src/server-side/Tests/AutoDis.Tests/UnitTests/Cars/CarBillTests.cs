using AutoDis.Domain.Cars;

namespace AutoDis.Tests.UnitTests.Cars;

public class CarBillTests
{
    private static CarBill CreateBill() => new(CarFactory.CreateCar());

    [Fact]
    public void Ctor_ValidCar_InitializesDefaults()
    {
        var car = CarFactory.CreateCar();

        var bill = new CarBill(car);

        Assert.Same(car, bill.Car);
        Assert.Equal(0m, bill.Cost);
        Assert.False(bill.IsArchieve);
        Assert.Empty(bill.Notes);
        Assert.Empty(bill.ImagePaths);
    }

    [Fact]
    public void Ctor_NullCar_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => new CarBill(null!));
    }

    [Fact]
    public void ChangeCost_ValidCost_Updates()
    {
        var bill = CreateBill();

        bill.ChangeCost(1500.75m);

        Assert.Equal(1500.75m, bill.Cost);
    }

    [Fact]
    public void ChangeCost_Zero_IsAllowed()
    {
        var bill = CreateBill();
        bill.ChangeCost(10m);

        bill.ChangeCost(0m);

        Assert.Equal(0m, bill.Cost);
    }

    [Fact]
    public void ChangeCost_Negative_ThrowsAndKeepsOldValue()
    {
        var bill = CreateBill();
        bill.ChangeCost(10m);

        Assert.Throws<ArgumentOutOfRangeException>(() => bill.ChangeCost(-1m));
        Assert.Equal(10m, bill.Cost);
    }

    [Fact]
    public void ChangeCost_WhenArchived_Throws()
    {
        var bill = CreateBill();
        bill.Archive();

        Assert.Throws<InvalidOperationException>(() => bill.ChangeCost(10m));
        Assert.Equal(0m, bill.Cost);
    }

    [Fact]
    public void Cost_IsIndependentFromNotes()
    {
        var bill = CreateBill();
        bill.ChangeCost(500m);

        bill.AddNote(CarFactory.CreateNote(cost: 100m));
        bill.AddNote(CarFactory.CreateNote(cost: 200m));

        Assert.Equal(500m, bill.Cost);
    }

    [Fact]
    public void AddNote_NewNote_AddsToList()
    {
        var bill = CreateBill();
        var note = CarFactory.CreateNote();

        bill.AddNote(note);

        Assert.Single(bill.Notes);
        Assert.Contains(note, bill.Notes);
    }

    [Fact]
    public void AddNote_SeveralDifferentNotes_AddsAll()
    {
        var bill = CreateBill();

        bill.AddNote(CarFactory.CreateNote());
        bill.AddNote(CarFactory.CreateNote());
        bill.AddNote(CarFactory.CreateNote());

        Assert.Equal(3, bill.Notes.Count);
    }

    [Fact]
    public void AddNote_Null_Throws()
    {
        var bill = CreateBill();

        Assert.Throws<ArgumentNullException>(() => bill.AddNote(null!));
    }

    [Fact]
    public void AddNote_SameNoteTwice_Throws()
    {
        var bill = CreateBill();
        var note = CarFactory.CreateNote();
        bill.AddNote(note);

        Assert.Throws<InvalidOperationException>(() => bill.AddNote(note));
        Assert.Single(bill.Notes);
    }

    [Fact]
    public void AddNote_DifferentInstanceWithSameId_Throws()
    {
        var bill = CreateBill();
        var id = Guid.NewGuid();
        bill.AddNote(CarFactory.CreateNote(id));

        Assert.Throws<InvalidOperationException>(() => bill.AddNote(CarFactory.CreateNote(id)));
    }

    [Fact]
    public void AddNote_WhenArchived_Throws()
    {
        var bill = CreateBill();
        bill.Archive();

        Assert.Throws<InvalidOperationException>(() => bill.AddNote(CarFactory.CreateNote()));
        Assert.Empty(bill.Notes);
    }

    [Fact]
    public void RemoveNote_ExistingNote_RemovesIt()
    {
        var bill = CreateBill();
        var keep = CarFactory.CreateNote();
        var remove = CarFactory.CreateNote();
        bill.AddNote(keep);
        bill.AddNote(remove);

        bill.RemoveNote(remove);

        Assert.Single(bill.Notes);
        Assert.Contains(keep, bill.Notes);
        Assert.DoesNotContain(remove, bill.Notes);
    }

    [Fact]
    public void RemoveNote_InstanceWithSameId_RemovesStoredOne()
    {
        var bill = CreateBill();
        var id = Guid.NewGuid();
        bill.AddNote(CarFactory.CreateNote(id));

        bill.RemoveNote(CarFactory.CreateNote(id));

        Assert.Empty(bill.Notes);
    }

    [Fact]
    public void RemoveNote_Null_Throws()
    {
        var bill = CreateBill();

        Assert.Throws<ArgumentNullException>(() => bill.RemoveNote(null!));
    }

    [Fact]
    public void RemoveNote_NoteNotInBill_Throws()
    {
        var bill = CreateBill();
        bill.AddNote(CarFactory.CreateNote());

        Assert.Throws<InvalidOperationException>(() => bill.RemoveNote(CarFactory.CreateNote()));
        Assert.Single(bill.Notes);
    }

    [Fact]
    public void RemoveNote_WhenArchived_Throws()
    {
        var bill = CreateBill();
        var note = CarFactory.CreateNote();
        bill.AddNote(note);
        bill.Archive();

        Assert.Throws<InvalidOperationException>(() => bill.RemoveNote(note));
        Assert.Single(bill.Notes);
    }

    [Fact]
    public void AddImage_ValidPath_AddsTrimmed()
    {
        var bill = CreateBill();

        bill.AddImage("  /images/bill-1.png  ");

        Assert.Single(bill.ImagePaths);
        Assert.Contains("/images/bill-1.png", bill.ImagePaths);
    }

    [Fact]
    public void AddImage_SeveralDifferentPaths_AddsAll()
    {
        var bill = CreateBill();

        bill.AddImage("/images/1.png");
        bill.AddImage("/images/2.png");

        Assert.Equal(2, bill.ImagePaths.Count);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void AddImage_EmptyPath_Throws(string? path)
    {
        var bill = CreateBill();

        Assert.Throws<ArgumentException>(() => bill.AddImage(path!));
        Assert.Empty(bill.ImagePaths);
    }

    [Fact]
    public void AddImage_DuplicateIgnoringCaseAndSpaces_Throws()
    {
        var bill = CreateBill();
        bill.AddImage("/images/bill-1.png");

        Assert.Throws<InvalidOperationException>(() => bill.AddImage("  /IMAGES/Bill-1.PNG "));
        Assert.Single(bill.ImagePaths);
    }

    [Fact]
    public void AddImage_WhenArchived_Throws()
    {
        var bill = CreateBill();
        bill.Archive();

        Assert.Throws<InvalidOperationException>(() => bill.AddImage("/images/1.png"));
        Assert.Empty(bill.ImagePaths);
    }

    [Fact]
    public void RemoveImage_ExistingPath_RemovesIt()
    {
        var bill = CreateBill();
        bill.AddImage("/images/1.png");
        bill.AddImage("/images/2.png");

        bill.RemoveImage("/images/1.png");

        Assert.Single(bill.ImagePaths);
        Assert.Contains("/images/2.png", bill.ImagePaths);
    }

    [Fact]
    public void RemoveImage_DifferentCaseAndSpaces_RemovesStoredOne()
    {
        var bill = CreateBill();
        bill.AddImage("/images/1.png");

        bill.RemoveImage("  /IMAGES/1.PNG ");

        Assert.Empty(bill.ImagePaths);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void RemoveImage_EmptyPath_Throws(string? path)
    {
        var bill = CreateBill();

        Assert.Throws<ArgumentException>(() => bill.RemoveImage(path!));
    }

    [Fact]
    public void RemoveImage_PathNotInBill_Throws()
    {
        var bill = CreateBill();
        bill.AddImage("/images/1.png");

        Assert.Throws<InvalidOperationException>(() => bill.RemoveImage("/images/2.png"));
        Assert.Single(bill.ImagePaths);
    }

    [Fact]
    public void RemoveImage_WhenArchived_Throws()
    {
        var bill = CreateBill();
        bill.AddImage("/images/1.png");
        bill.Archive();

        Assert.Throws<InvalidOperationException>(() => bill.RemoveImage("/images/1.png"));
        Assert.Single(bill.ImagePaths);
    }

    [Fact]
    public void Archive_SetsFlag()
    {
        var bill = CreateBill();

        bill.Archive();

        Assert.True(bill.IsArchieve);
    }

    [Fact]
    public void Unarchive_ClearsFlagAndAllowsChangesAgain()
    {
        var bill = CreateBill();
        bill.Archive();

        bill.Unarchive();
        bill.AddNote(CarFactory.CreateNote());
        bill.ChangeCost(5m);

        Assert.False(bill.IsArchieve);
        Assert.Single(bill.Notes);
        Assert.Equal(5m, bill.Cost);
    }
}
