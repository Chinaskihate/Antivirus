using System.Collections.Concurrent;
using Antivirus.Application.Common.Exceptions;
using Antivirus.Application.Interfaces.ScanManagers;
using Antivirus.Application.Interfaces.ScanServices;
using Antivirus.Application.Models;
using Antivirus.Application.Services.ScanServices;
using Antivirus.Domain.Models;

namespace Antivirus.Application.Services.ScanManagers;

/// <summary>
///     Manages multiple scans.
/// </summary>
public class ScanManager : IScanManager
{
    private readonly ConcurrentDictionary<int, ScanStatus> _tasks;

    /// <summary>
    ///     Constructor.
    /// </summary>
    public ScanManager()
    {
        _tasks = new ConcurrentDictionary<int, ScanStatus>();
    }

    /// <summary>
    ///     Creates new scan.
    /// </summary>
    /// <param name="path"> Directory to scan. </param>
    /// <returns> Id of scan. </returns>
    public int CreateScan(string path)
    {
        var id = _tasks.Count;
        IScanService service = new ScanService();
        if (_tasks.TryAdd(id, service.Scan(path)))
        {
            return id;
        }

        return -1;
    }

    /// <summary>
    ///     Get status of scan.
    /// </summary>
    /// <param name="id"> Id of scan. </param>
    /// <returns> Dto of scan status. </returns>
    /// <exception cref="ScanNotFoundException"> If scan was not found. </exception>
    public ScanStatusDto GetStatus(int id)
    {
        if (!_tasks.ContainsKey(id))
        {
            throw new ScanNotFoundException(id, $"Scan {id} doesn't exist.");
        }

        var status = _tasks[id];

        return new ScanStatusDto
        {
            TotalProcessedFiles = status.TotalProcessedFiles,
            TotalEvilJsDetects = status.TotalEvilJsDetects,
            TotalRemoveDetects = status.TotalRemoveDetects,
            TotalRunDllDetects = status.TotalRunDllDetects,
            ExecutionTime = status.IsFinished
                ? (DateTime)status.FinishTime - status.StartTime
                : DateTime.Now - status.StartTime,
            IsFinished = status.IsFinished,
            ErrorMessages = status.ErrorMessages
        };
    }
}