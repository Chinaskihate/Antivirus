using System.Text.Json;

namespace Antivirus.WebClient;

public class ManagerHttpClient : HttpClient
{
    public ManagerHttpClient(string address)
    {
        BaseAddress = new Uri(address);
    }

    public async Task<T> GetAsync<T>(string query)
    {
        var response = await GetAsync(query);
        // TODO: check status codes.
        var jsonResponse = await response.Content.ReadAsStringAsync();

        try
        {
            return JsonSerializer.Deserialize<T>(jsonResponse);
        }
        catch
        {
            throw new ArgumentException(jsonResponse);
        }
    }
}