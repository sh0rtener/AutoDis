namespace AutoDis.Domain.Cars;

public sealed class Power : ValueObject
{
    private const double KwPerHp = 0.7355;
    private const int MaxHorsepower = 3000;
    public int Horsepower { get; private set; }

    public Power(int horsepower)
    {
        if (horsepower is <= 0 or > MaxHorsepower)
            throw new ArgumentOutOfRangeException(
                nameof(horsepower),
                $"Power must be between 1 and {MaxHorsepower} hp."
            );

        Horsepower = horsepower;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Horsepower;
    }

    public override string ToString() => $"{Horsepower} hp";
}
