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

    public async Task<ScanResult> ScanAsync(string path)
    {
        using (var client = _factory.CreateHttpClient())
        {
            string query = $"test?path={System.Web.HttpUtility.UrlEncode(path)}";
            var status = await client.GetAsync<ScanResult>(query);
            return status;
        }
    }
}