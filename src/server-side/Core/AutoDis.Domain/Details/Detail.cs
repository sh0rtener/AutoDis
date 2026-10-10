using AutoDis.Domain.Cars;

namespace AutoDis.Domain.Details;

public sealed class Detail : Entity<Guid>
{
    private List<Car> _availableCars = new();
    private List<string> _imagePaths = new();
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsWorked { get; private set; }
    public decimal Cost { get; private set; }
    public IReadOnlyCollection<Car> AvailableCars => _availableCars;
    public IReadOnlyCollection<string> ImagePaths => _imagePaths;

    public Detail(string name, string description, bool isWorked, decimal cost)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.", nameof(description));

        if (cost < 0)
            throw new ArgumentOutOfRangeException(nameof(cost), "Cost cannot be negative.");

        Name = name.Trim();
        Description = description.Trim();
        IsWorked = isWorked;
        Cost = cost;
    }

    public void AddImage(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
            throw new ArgumentException("Image path is required.", nameof(imagePath));

        imagePath = imagePath.Trim();

        if (_imagePaths.Contains(imagePath, StringComparer.OrdinalIgnoreCase))
            throw new InvalidOperationException("This image is already added to the detail.");

        _imagePaths.Add(imagePath);
    }

    public void RemoveImage(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
            throw new ArgumentException("Image path is required.", nameof(imagePath));

        imagePath = imagePath.Trim();

        var existing =
            _imagePaths.FirstOrDefault(p =>
                string.Equals(p, imagePath, StringComparison.OrdinalIgnoreCase)
            ) ?? throw new InvalidOperationException("This image is not in the detail.");

        _imagePaths.Remove(existing);
    }

    public void ChangeDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.", nameof(description));

        Description = description.Trim();
    }

    public void ChangeIsWorked(bool isWorked)
    {
        IsWorked = isWorked;
    }

    public void AddCar(Car car)
    {
        ArgumentNullException.ThrowIfNull(car);

        if (_availableCars.Any(c => c.Id == car.Id))
            throw new InvalidOperationException("This car is already added to the detail.");

        _availableCars.Add(car);
    }

    public void RemoveCar(Car car)
    {
        ArgumentNullException.ThrowIfNull(car);

        var existing =
            _availableCars.FirstOrDefault(c => c.Id == car.Id)
            ?? throw new InvalidOperationException("This car is not in the detail's list.");

        _availableCars.Remove(existing);
    }
}
