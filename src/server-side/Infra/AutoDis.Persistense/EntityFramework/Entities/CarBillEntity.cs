namespace AutoDis.Persistense.EntityFramework.Entities;

public sealed class CarBillEntity
{
    public int Id { get; set; }
    public decimal Cost { get; set; }
    public bool IsArchieve { get; set; }
    public int CarId { get; set; }

    public CarEntity Car { get; set; } = null!;
    public ICollection<CarBillNoteEntity> Notes { get; set; } = new List<CarBillNoteEntity>();
    public ICollection<CarBillImageEntity> Images { get; set; } = new List<CarBillImageEntity>();
}
