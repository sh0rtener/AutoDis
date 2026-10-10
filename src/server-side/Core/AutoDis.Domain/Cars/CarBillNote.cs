namespace AutoDis.Domain.Cars;

public sealed class CarBillNote : Entity<Guid>
{
    public string Name { get; private set; }
    public decimal Cost { get; private set; }
    public bool IsAvailableForSale { get; private set; }

    public CarBillNote(string name, decimal cost, bool isAvailableForSale = false)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        if (cost < 0)
            throw new ArgumentOutOfRangeException(nameof(cost), "Cost cannot be negative.");

        Name = name.Trim();
        Cost = cost;
        IsAvailableForSale = isAvailableForSale;
    }

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        Name = name.Trim();
    }

    public void ChangeCost(decimal cost)
    {
        if (cost < 0)
            throw new ArgumentOutOfRangeException(nameof(cost), "Cost cannot be negative.");

        Cost = cost;
    }

    public void MakeAvailableForSale()
    {
        IsAvailableForSale = true;
    }

    public void MakeUnavailableForSale()
    {
        IsAvailableForSale = false;
    }
}
