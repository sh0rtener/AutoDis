namespace AutoDis.Domain.Cars;

public sealed class VinCode : ValueObject
{
    private const int VinLength = 17;

    private string _code;
    public string Code => _code;

    public VinCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("VIN code is required.", nameof(code));

        code = code.Trim().ToUpperInvariant();

        if (code.Length != VinLength)
            throw new ArgumentException(
                $"VIN code must be exactly {VinLength} characters.",
                nameof(code)
            );

        if (code.Any(c => !char.IsAsciiLetterOrDigit(c) || c is 'I' or 'O' or 'Q'))
            throw new ArgumentException(
                "VIN code may contain only digits and Latin letters except I, O, Q.",
                nameof(code)
            );

        _code = code;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}
