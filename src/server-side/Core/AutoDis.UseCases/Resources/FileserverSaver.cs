using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AutoDis.UseCases.Resources;

public sealed record FileData(string FileName, string ContentType, long Length, Stream Content);

public sealed class FileserverSaver : IResourceSaver
{
    private record FileserverCredentials(string Username, string Password);

    private readonly string _path;
    private readonly string _host;
    private readonly FileserverCredentials _credentials;
    private readonly ILogger<FileserverSaver> _logger;

    public FileserverSaver(IConfiguration configuration, ILogger<FileserverSaver> logger)
    {
        _path =
            configuration["Fileserver:Path"] ?? throw new ArgumentNullException("Fileserver:Path");
        _path = _path.Trim('/');
        _host =
            configuration["Fileserver:Host"] ?? throw new ArgumentNullException("Fileserver:Host");
        _host.Trim('/');

        _credentials = new(
            (
                configuration["Fileserver:Username"]
                ?? throw new ArgumentNullException("Fileserver:Username")
            ).Trim(),
            (
                configuration["Fileserver:Password"]
                ?? throw new ArgumentNullException("Fileserver:Password")
            ).Trim()
        );

        _logger = logger;
    }

    public async Task<string> SaveResource(
        byte[] resource,
        string type,
        CancellationToken cancellationToken = default
    )
    {
        var path = $"{_path}/{Guid.NewGuid()}.{type}";
        var url = $"{_host}/{_path}";

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic",
            GetBasicAuthHeader()
        );

        using var content = new ByteArrayContent(resource);

        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        try
        {
            _logger.LogInformation($"Trying to send {resource.Length} bytes to {url}");

            var response = await httpClient.PutAsync(url, content, cancellationToken);

            if (response.IsSuccessStatusCode)
                _logger.LogInformation("Bytes sended succesfully");
            else
            {
                string errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("Bytes doesn't send succesfully");
                throw new FileLoadException(errorBody);
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"NetworkError {ex.Message}");
            throw;
        }

        return path;
    }

    private string GetBasicAuthHeader()
    {
        var value = $"{_credentials.Username}:{_credentials.Password}";
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    }
}
