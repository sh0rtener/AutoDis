namespace AutoDis.Persistense.EntityFramework.Entities;

public class CarBillImageEntity
{
    public int Id { get; set; }
    public int CarBillId { get; set; }
    public string ImagePath { get; set; } = null!;

    public CarBillEntity CarBill { get; set; } = null!;
}
