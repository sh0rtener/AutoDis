namespace AutoDis.Persistense.EntityFramework.Entities;

public class CarManufacturerEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CountryId { get; set; }

    public CarCountryEntity Country { get; set; } = null!;
    public ICollection<CarEntity> Cars { get; set; } = new List<CarEntity>();
}
