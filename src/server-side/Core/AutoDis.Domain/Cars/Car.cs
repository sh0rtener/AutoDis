namespace AutoDis.Domain.Cars;

public sealed class Car : Entity<int>
{
    public string Model { get; private set; }
    public CarManufacturer Manufacturer { get; private set; }
    public int Year { get; private set; }
    public VinCode Vin { get; private set; }
    public CarColor Color { get; private set; }

    public CarBodyType BodyType { get; private set; }
    public CarFuelType FuelType { get; private set; }
    public CarTransmissionType TransmissionType { get; private set; }
    public CarDriveTrain DriveTrain { get; private set; }
    public Power Power { get; private set; }

    public int Doors { get; private set; }
    public int Seats { get; private set; }
    public Mileage Mileage { get; private set; }

    public Car(
        string model,
        CarManufacturer manufacturer,
        int year,
        VinCode vin,
        CarColor color,
        CarBodyType bodyType,
        CarFuelType fuelType,
        CarTransmissionType transmissionType,
        CarDriveTrain driveTrain,
        Power power,
        int doors,
        int seats,
        Mileage mileage
    )
    {
        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model is required.", nameof(model));

        if (year < 1886 || year > DateTime.UtcNow.Year + 1)
            throw new ArgumentOutOfRangeException(nameof(year));

        if (doors is < 2 or > 6)
            throw new ArgumentOutOfRangeException(nameof(doors));

        if (seats is < 1 or > 9)
            throw new ArgumentOutOfRangeException(nameof(seats));

        Model = model.Trim();
        Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));
        Year = year;
        Vin = vin ?? throw new ArgumentNullException(nameof(vin));
        Color = color;
        BodyType = bodyType;
        FuelType = fuelType;
        TransmissionType = transmissionType;
        DriveTrain = driveTrain;
        Power = power ?? throw new ArgumentNullException(nameof(power));
        Doors = doors;
        Seats = seats;
        Mileage = mileage ?? throw new ArgumentNullException(nameof(mileage));
    }
}
