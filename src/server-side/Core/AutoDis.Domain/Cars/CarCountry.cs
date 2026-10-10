namespace AutoDis.Domain.Cars;

public sealed class CarCountry : Entity<int>
{
    public string Name { get; private set; }
    public string IsoCode { get; private set; }

    public CarCountry(string name, string isoCode)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Country name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(isoCode))
            throw new ArgumentException("ISO code is required.", nameof(isoCode));

        isoCode = isoCode.Trim().ToUpperInvariant();

        if (isoCode.Length != 2 || isoCode.Any(c => !char.IsAsciiLetterUpper(c)))
            throw new ArgumentException("ISO code must be exactly 2 Latin letters.", nameof(isoCode));

        Name = name.Trim();
        IsoCode = isoCode;
    }
}