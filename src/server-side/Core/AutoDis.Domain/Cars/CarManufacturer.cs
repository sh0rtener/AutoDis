namespace AutoDis.Domain.Cars;

public sealed class CarManufacturer : Entity<int>
{
    public string Name { get; private set; }
    public CarManufacturer Manufacturer { get; private set; }

    public CarManufacturer(string name, CarManufacturer manufacturer)
    {
        Name = name;
        Manufacturer = manufacturer;
    }
}
