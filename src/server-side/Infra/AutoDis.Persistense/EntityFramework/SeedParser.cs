namespace AutoDis.Persistense.EntityFramework;

public static class SeedParser
{
    public static IEnumerable<ManufacturerSeed> ParseManufacturers(
        string csvData,
        char separator = ';'
    )
    {
        csvData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Seeds", csvData); 
        if (!File.Exists(csvData))
            throw new ArgumentNullException($"Не найден файл с сидами ManufacturersSeed (${csvData})");
        var lines = File.ReadAllLines(csvData);

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            var parts = lines[i].Split(separator);
            if (parts.Length == 3)
            {
                if (
                    int.TryParse(parts[0].Trim(), out int id)
                    && int.TryParse(parts[2].Trim(), out int countryId)
                )
                {
                    yield return new ManufacturerSeed(id, parts[1].Trim(), countryId);
                }
            }
        }
    }

    public static IEnumerable<CountrySeed> ParseCountries(string csvData, char separator = ';')
    {
        csvData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Seeds", csvData); 
        if (!File.Exists(csvData))
            throw new ArgumentNullException($"Не найден файл с сидами CountrySeed (${csvData})");
        var lines = File.ReadAllLines(csvData);

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            var parts = lines[i].Split(separator);
            if (parts.Length == 3)
            {
                if (int.TryParse(parts[2].Trim(), out int id))
                {
                    yield return new CountrySeed(id, parts[0].Trim(), parts[1].Trim());
                }
            }
        }
    }
}
