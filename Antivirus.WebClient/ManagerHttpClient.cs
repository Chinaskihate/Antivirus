using System.Net;
using System.Text;
using System.Text.Json;
using Antivirus.WebClient.Exceptions;

namespace Antivirus.WebClient;

/// <summary>
///     Http Client with generic methods.
/// </summary>
public class ManagerHttpClient : HttpClient
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="baseAddress"> Base address. </param>
    public ManagerHttpClient(string baseAddress)
    {
        BaseAddress = new Uri(baseAddress);
    }
    
    /// <summary>
    ///     Sends GET method to WebAPI.
    /// </summary>
    /// <typeparam name="T"> Type of response. </typeparam>
    /// <param name="query"> Query. </param>
    /// <returns> Response from WebAPI. </returns>
    /// <exception cref="ArgumentException"> If query is wrong. </exception>
    /// <exception cref="NotFoundException"> If object is not found. </exception>
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

    /// <summary>
    ///     Sends POST method to WebAPI.
    /// </summary>
    /// <typeparam name="T"> Type of response. </typeparam>
    /// <param name="query"> Query. </param>
    /// <param name="data"> Properties and values. </param>
    /// <returns> Response from WebAPI. </returns>
    /// <exception cref="ArgumentException"> If deserialization went wrong. </exception>
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