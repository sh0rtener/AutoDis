namespace AutoDis.Domain.Cars;

public sealed class CarManufacturer : Entity<int>
{
    public string Name { get; private set; }
    public CarCountry Country { get; private set; }

    public CarManufacturer(string name, CarCountry country)
    {
        Name = name;
        Country = country;
    }
}
