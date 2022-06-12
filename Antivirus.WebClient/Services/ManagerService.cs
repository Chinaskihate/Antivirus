using Antivirus.WebClient.Exceptions;
using Antivirus.WebClient.Interfaces;
using Antivirus.WebClient.Results;

namespace Antivirus.WebClient.Services;

/// <summary>
///     Manages calls to WebAPI.
/// </summary>
public class ManagerService : IManagerService
{
    private readonly ManagerHttpClientFactory _factory;

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="factory"> HttpClientFactory. </param>
    public ManagerService(ManagerHttpClientFactory factory)
    {
        _factory = factory;
    }

    /// <summary>
    ///     Requests to create new scan to WebAPI.
    /// </summary>
    /// <param name="path"> Directory to scan. </param>
    /// <returns> Id. </returns>
    /// <exception cref="ArgumentException"> If deserializing went wrong. </exception>
    public async Task<int> CreateScanAsync(string path)
    {
        using (var client = _factory.CreateHttpClient())
        {
            var id = await client.PostAsync<int>("", new Dictionary<string, string>()
            {
                {"Path", path }
            });
            return id;
        }
    }

    /// <summary>
    ///     Get status of scan from WebAPI.
    /// </summary>
    /// <param name="id"> Id of scan. </param>
    /// <returns> Status of scan. </returns>
    /// <exception cref="ScanNotFoundException"> If scan with such id is not found. </exception>
    public async Task<ScanStatus> GetStatusAsync(int id)
    {
        using (var client = _factory.CreateHttpClient())
        {
            var query = $"?id={id}";
            try
            {
                var status = await client.GetAsync<ScanStatus>(query);
                return status;
            }
            catch (NotFoundException)
            {
                throw new ScanNotFoundException(id, $"Scan with ID {id} not found.");
            }
        }
    }
}