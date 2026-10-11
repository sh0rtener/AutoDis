namespace AutoDis.UseCases.Cars;

public sealed class ShortCarDto
{
    public int Id { get; set; }
    public string? Model { get; set; }
    public string? Manufacturer { get; set; }
    public string? Country { get; set; }
    public int Year { get; set; }
}
