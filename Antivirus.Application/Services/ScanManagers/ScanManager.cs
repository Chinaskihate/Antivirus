using System.Collections.Concurrent;
using Antivirus.Application.Interfaces.ScanManagers;
using Antivirus.Application.Interfaces.ScanServices;
using Antivirus.Application.Models;
using Antivirus.Application.Services.ScanServices;
using Antivirus.Domain.Models;

namespace Antivirus.Application.Services.ScanManagers;

public class ScanManager : IScanManager
{
    private readonly ConcurrentDictionary<Guid, Task<ScanResult>> _tasks;

    /// <summary>
    ///     Constructor.
    /// </summary>
    public ScanManager()
    {
        _tasks = new ConcurrentDictionary<Guid, Task<ScanResult>>();
    }

    public Guid CreateScan(string path)
    {
        var id = Guid.NewGuid();
        IScanService service = new ScanService();
        if (_tasks.TryAdd(id, service.ScanAsync(path)))
        {
            return id;
        }

        ;

        return Guid.Empty;
    }

    public ScanStatus GetStatus(Guid id)
    {
        if (!_tasks.ContainsKey(id))
        {
            throw new ArgumentException($"Scan {id} doesn't exist.");
        }

        var currTask = _tasks[id];

        return new ScanStatus
        {
            IsCompleted = currTask.IsCompleted,
            Result = currTask.IsCompleted ? currTask.Result : new ScanResult()
        };
    }
}