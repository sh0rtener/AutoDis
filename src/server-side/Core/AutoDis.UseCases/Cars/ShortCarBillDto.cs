namespace AutoDis.UseCases.Cars;

public sealed class ShortCarBillDto
{
    public int Id { get; set; }
    public ShortCarDto? Car { get; set; }
    public List<ShortCarBillNoteDto> Notes { get; set; } = new();
    public List<string> ImagePaths { get; set; } = new();
    public decimal Cost { get; set; }
    public bool IsArchieve { get; set; }
}

public sealed class ShortCarBillNoteDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public decimal Cost { get; set; }
    public bool IsAvailableForSale { get; set; }
}
