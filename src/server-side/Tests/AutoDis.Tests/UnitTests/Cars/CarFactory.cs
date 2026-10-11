namespace AutoDis.Tests.UnitTests.Cars;

using AutoDis.Domain.Cars;

internal static class CarFactory
{
    public static CarManufacturer CreateManufacturer(string name = "Toyota") => new(name, null!);

    public static Car CreateCar(
        int id = 1,
        string model = "Camry",
        int year = 2020,
        int doors = 4,
        int seats = 5
    )
    {
        return new Car(
            model,
            CreateManufacturer(),
            year,
            CarColor.Black,
            CarBodyType.Sedan,
            CarFuelType.Petrol,
            CarTransmissionType.Automatic,
            CarDriveTrain.Fwd,
            new Power(150),
            doors,
            seats,
            new Mileage(10_000f)
        )
        {
            Id = id,
        };
    }

    public static CarBillNote CreateNote(
        Guid? id = null,
        string name = "Oil change",
        decimal cost = 100m
    ) => new(name, cost) { Id = id ?? Guid.NewGuid() };
}
