using Antivirus.WebClient.Interfaces;
using Antivirus.WebClient.Results;

namespace Antivirus.WebClient.Services;

public class ManagerService : IManagerService
{
    private readonly ManagerHttpClientFactory _factory;

    public ManagerService(ManagerHttpClientFactory factory)
    {
        _factory = factory;
    }

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

    public async Task<ScanStatus> GetStatusAsync(int id)
    {
        using (var client = _factory.CreateHttpClient())
        {
            var query = $"?id={id}";
            var status = await client.GetAsync<ScanStatus>(query);
            return status;
        }
    }
}