using AutoDis.Domain.Cars;

namespace AutoDis.Tests.UnitTests.Cars;

public class CarTests
{
    private static Car Create(
        string model = "Camry",
        CarManufacturer? manufacturer = null,
        int year = 2020,
        Power? power = null,
        int doors = 4,
        int seats = 5,
        Mileage? mileage = null)
    {
        return new Car(
            model,
            manufacturer ?? CarFactory.CreateManufacturer(),
            year,
            CarColor.Red,
            CarBodyType.Suv,
            CarFuelType.Diesel,
            CarTransmissionType.Manual,
            CarDriveTrain.Awd,
            power ?? new Power(200),
            doors,
            seats,
            mileage ?? new Mileage(1234.5f)
        );
    }

    [Fact]
    public void Ctor_ValidArguments_SetsAllProperties()
    {
        var manufacturer = CarFactory.CreateManufacturer("Honda");
        var power = new Power(200);
        var mileage = new Mileage(1234.5f);

        var car = Create("Civic", manufacturer, 2021, power, 4, 5, mileage);

        Assert.Equal("Civic", car.Model);
        Assert.Same(manufacturer, car.Manufacturer);
        Assert.Equal(2021, car.Year);
        Assert.Equal(CarColor.Red, car.Color);
        Assert.Equal(CarBodyType.Suv, car.BodyType);
        Assert.Equal(CarFuelType.Diesel, car.FuelType);
        Assert.Equal(CarTransmissionType.Manual, car.TransmissionType);
        Assert.Equal(CarDriveTrain.Awd, car.DriveTrain);
        Assert.Same(power, car.Power);
        Assert.Equal(4, car.Doors);
        Assert.Equal(5, car.Seats);
        Assert.Same(mileage, car.Mileage);
    }

    [Fact]
    public void Ctor_TrimsModel()
    {
        var car = Create(model: "  Civic  ");

        Assert.Equal("Civic", car.Model);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Ctor_EmptyModel_Throws(string? model)
    {
        Assert.Throws<ArgumentException>(() => Create(model: model!));
    }

    [Theory]
    [InlineData(1885)]
    [InlineData(0)]
    [InlineData(-1)]
    public void Ctor_YearTooEarly_Throws(int year)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Create(year: year));
    }

    [Fact]
    public void Ctor_YearTooFarInFuture_Throws()
    {
        var year = DateTime.UtcNow.Year + 2;

        Assert.Throws<ArgumentOutOfRangeException>(() => Create(year: year));
    }

    [Fact]
    public void Ctor_YearBoundaries_AreAccepted()
    {
        var oldest = Create(year: 1886);
        var newest = Create(year: DateTime.UtcNow.Year + 1);

        Assert.Equal(1886, oldest.Year);
        Assert.Equal(DateTime.UtcNow.Year + 1, newest.Year);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(7)]
    public void Ctor_DoorsOutOfRange_Throws(int doors)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Create(doors: doors));
    }

    [Theory]
    [InlineData(2)]
    [InlineData(6)]
    public void Ctor_DoorsBoundaries_AreAccepted(int doors)
    {
        Assert.Equal(doors, Create(doors: doors).Doors);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(10)]
    public void Ctor_SeatsOutOfRange_Throws(int seats)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Create(seats: seats));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(9)]
    public void Ctor_SeatsBoundaries_AreAccepted(int seats)
    {
        Assert.Equal(seats, Create(seats: seats).Seats);
    }

    [Fact]
    public void Ctor_NullManufacturer_Throws()
    {
        var ex = Assert.Throws<ArgumentNullException>(() =>
            new Car(
                "Camry", null!, 2020, CarColor.Black,
                CarBodyType.Sedan, CarFuelType.Petrol, CarTransmissionType.Automatic,
                CarDriveTrain.Fwd, new Power(100), 4, 5, new Mileage(0f)));

        Assert.Equal("manufacturer", ex.ParamName);
    }

    [Fact]
    public void Ctor_NullPower_Throws()
    {
        var ex = Assert.Throws<ArgumentNullException>(() =>
            new Car(
                "Camry", CarFactory.CreateManufacturer(), 2020, CarColor.Black,
                CarBodyType.Sedan, CarFuelType.Petrol, CarTransmissionType.Automatic,
                CarDriveTrain.Fwd, null!, 4, 5, new Mileage(0f)));

        Assert.Equal("power", ex.ParamName);
    }

    [Fact]
    public void Ctor_NullMileage_Throws()
    {
        var ex = Assert.Throws<ArgumentNullException>(() =>
            new Car(
                "Camry", CarFactory.CreateManufacturer(), 2020, CarColor.Black,
                CarBodyType.Sedan, CarFuelType.Petrol, CarTransmissionType.Automatic,
                CarDriveTrain.Fwd, new Power(100), 4, 5, null!));

        Assert.Equal("mileage", ex.ParamName);
    }
}
