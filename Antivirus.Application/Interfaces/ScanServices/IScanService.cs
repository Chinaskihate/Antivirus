using Antivirus.Domain.Models;

namespace Antivirus.Application.Interfaces.ScanServices;

/// <summary>
///     Service for scanning directory.
/// </summary>
public interface IScanService
{
    /// <summary>
    ///     Scans directory.
    /// </summary>
    /// <param name="path"> Directory to scan. </param>
    /// <returns> Status of scan. </returns>
    ScanStatus Scan(string path);
}