using System.Net;
using System.Text;
using System.Text.Json;
using Antivirus.WebClient.Exceptions;

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
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new ArgumentException();
        }
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new NotFoundException();
        }
        // TODO: check status codes.
        var jsonResponse = await response.Content.ReadAsStringAsync();

        try
        {
            return JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch
        {
            throw new ArgumentException(jsonResponse);
        }
    }

    public async Task<T> PostAsync<T>(string query, Dictionary<string, string> data)
    {
        var jsonString = JsonSerializer.Serialize(data);
        HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
        var response = await PostAsync("", content);
        
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