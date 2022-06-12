using Antivirus.Application.Common.Exceptions;
using Antivirus.Application.Models;

namespace Antivirus.Application.Interfaces.ScanManagers;

/// <summary>
///     Manages multiple scans.
/// </summary>
public interface IScanManager
{
    /// <summary>
    ///     Creates new scan.
    /// </summary>
    /// <param name="path"> Directory to scan. </param>
    /// <returns> Id of scan. </returns>
    int CreateScan(string path);

    /// <summary>
    ///     Get status of scan.
    /// </summary>
    /// <param name="id"> Id of scan. </param>
    /// <returns> Dto of scan status. </returns>
    /// <exception cref="ScanNotFoundException"> If scan was not found. </exception>
    ScanStatusDto GetStatus(int id);
}