namespace Antivirus.WebClient;

public class ManagerHttpClientFactory
{
    private readonly string _baseUrl;

    public ManagerHttpClientFactory(string url)
    {
        _baseUrl = url;
    }

    public ManagerHttpClient CreateHttpClient()
    {
        return new ManagerHttpClient(_baseUrl);
    }
}