namespace AutoDis.Persistense.EntityFramework.Entities;

public sealed class CarEntity
{
    public int Id { get; set; }
    public string Model { get; set; } = null!;
    public int ManufacturerId { get; set; }
    public int Year { get; set; }
    public int ColorType { get; set; }
    public int BodyType { get; set; }
    public int FuelType { get; set; }
    public int TransmissionType { get; set; }
    public int DriveTrain { get; set; }
    public int Power { get; set; }
    public int Doors { get; set; }
    public int Seats { get; set; }
    public float Mileage { get; set; }

    public CarManufacturerEntity Manufacturer { get; set; } = null!;
}
