namespace AutoDis.Persistense.EntityFramework.Entities;

public sealed class CarBillNoteEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Cost { get; set; }
    public bool Available { get; set; }
    public int CarBillId { get; set; }

    public CarBillEntity CarBill { get; set; } = null!;
}
