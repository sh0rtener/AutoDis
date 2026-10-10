namespace AutoDis.Domain.Cars;

public sealed class CarBill : Entity<int>
{
    private List<CarBillNote> _notes = new();
    private List<string> _imagePaths = new();

    public Car Car { get; private set; }
    public IReadOnlyCollection<CarBillNote> Notes => _notes;
    public decimal Cost { get; private set; }
    public bool IsArchieve { get; private set; }
    public IReadOnlyCollection<string> ImagePaths => _imagePaths;

    public CarBill(Car car)
    {
        Car = car ?? throw new ArgumentNullException(nameof(car));
        Cost = 0;
        IsArchieve = false;
    }

    public void AddImage(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
            throw new ArgumentException("Image path is required.", nameof(imagePath));

        if (IsArchieve)
            throw new InvalidOperationException("Cannot change an archived bill.");

        imagePath = imagePath.Trim();

        if (_imagePaths.Contains(imagePath, StringComparer.OrdinalIgnoreCase))
            throw new InvalidOperationException("This image is already added to the bill.");

        _imagePaths.Add(imagePath);
    }

    public void RemoveImage(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
            throw new ArgumentException("Image path is required.", nameof(imagePath));

        if (IsArchieve)
            throw new InvalidOperationException("Cannot change an archived bill.");

        imagePath = imagePath.Trim();

        var existing =
            _imagePaths.FirstOrDefault(p =>
                string.Equals(p, imagePath, StringComparison.OrdinalIgnoreCase)
            ) ?? throw new InvalidOperationException("This image is not in the bill.");

        _imagePaths.Remove(existing);
    }

    public void ChangeCost(decimal cost)
    {
        if (IsArchieve)
            throw new InvalidOperationException("Cannot change an archived bill.");

        if (cost < 0)
            throw new ArgumentOutOfRangeException(nameof(cost), "Cost cannot be negative.");

        Cost = cost;
    }

    public void AddNote(CarBillNote note)
    {
        ArgumentNullException.ThrowIfNull(note);

        if (IsArchieve)
            throw new InvalidOperationException("Cannot change an archived bill.");

        if (_notes.Any(n => n.Id == note.Id))
            throw new InvalidOperationException("This note is already added to the bill.");

        _notes.Add(note);
    }

    public void RemoveNote(CarBillNote note)
    {
        ArgumentNullException.ThrowIfNull(note);

        if (IsArchieve)
            throw new InvalidOperationException("Cannot change an archived bill.");

        var existing =
            _notes.FirstOrDefault(n => n.Id == note.Id)
            ?? throw new InvalidOperationException("This note is not in the bill.");

        _notes.Remove(existing);
    }

    public void Archive()
    {
        IsArchieve = true;
    }

    public void Unarchive()
    {
        IsArchieve = false;
    }
}
