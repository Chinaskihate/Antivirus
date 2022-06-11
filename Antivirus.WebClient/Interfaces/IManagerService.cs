using Antivirus.WebClient.Results;

namespace Antivirus.WebClient.Interfaces;

public interface IManagerService
{
    Task<ScanResult> ScanAsync(string path);
}