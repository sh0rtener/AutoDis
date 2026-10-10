namespace AutoDis.Domain.Cars;

public sealed class Mileage : ValueObject
{
    private const float MaxKm = 10_000_000f;

    private float _km;
    public float Km => _km;

    public Mileage(float km)
    {
        if (float.IsNaN(km) || float.IsInfinity(km))
            throw new ArgumentException("Mileage must be a valid number.", nameof(km));

        if (km < 0)
            throw new ArgumentOutOfRangeException(nameof(km), "Mileage cannot be negative.");

        if (km > MaxKm)
            throw new ArgumentOutOfRangeException(nameof(km), $"Mileage cannot exceed {MaxKm} km.");

        _km = km;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Km;
    }
}