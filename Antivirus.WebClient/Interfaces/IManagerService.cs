using Antivirus.WebClient.Results;

namespace Antivirus.WebClient.Interfaces;

/// <summary>
///     Manages calls to WebAPI.
/// </summary>
public interface IManagerService
{
    /// <summary>
    ///     Requests to create new scan to WebAPI.
    /// </summary>
    /// <param name="path"> Directory to scan. </param>
    /// <returns> Id. </returns>
    Task<int> CreateScanAsync(string path);

    /// <summary>
    ///     Get status of scan from WebAPI.
    /// </summary>
    /// <param name="id"> Id of scan. </param>
    /// <returns> Current status of scan. </returns>
    Task<ScanStatus> GetStatusAsync(int id);
}