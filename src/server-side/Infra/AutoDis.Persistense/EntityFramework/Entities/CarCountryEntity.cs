namespace AutoDis.Persistense.EntityFramework.Entities;

public class CarCountryEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string IsoCode { get; set; } = null!;

    public ICollection<CarManufacturerEntity> Manufacturers { get; set; } =
        new List<CarManufacturerEntity>();
}
