namespace Antivirus.WebClient;

/// <summary>
///     Http Client Factory.
/// </summary>
public class ManagerHttpClientFactory
{
    private readonly string _baseUrl;

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="baseUrl"> Base url. </param>
    public ManagerHttpClientFactory(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    /// <summary>
    /// Creates HttpClient.
    /// </summary>
    /// <returns> ManagerHttpClient with base url. </returns>
    public ManagerHttpClient CreateHttpClient()
    {
        return new ManagerHttpClient(_baseUrl);
    }
}