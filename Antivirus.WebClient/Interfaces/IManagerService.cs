using Antivirus.WebClient.Results;

namespace Antivirus.WebClient.Interfaces;

public interface IManagerService
{
    Task<int> CreateScanAsync(string path);

    Task<ScanStatus> GetStatusAsync(int id);
}