using System.Collections.Concurrent;
using Antivirus.Application.Interfaces.ScanManagers;
using Antivirus.Application.Interfaces.ScanServices;
using Antivirus.Application.Services.ScanServices;
using Antivirus.Domain.Models;

namespace Antivirus.Application.Services.ScanManagers;

public class ScanManager : IScanManager
{
    private readonly ConcurrentDictionary<Guid, ScanStatus> _tasks;

    /// <summary>
    ///     Constructor.
    /// </summary>
    public ScanManager()
    {
        _tasks = new ConcurrentDictionary<Guid, ScanStatus>();
    }

    public Guid CreateScan(string path)
    {
        var id = Guid.NewGuid();
        IScanService service = new ScanService();
        if (_tasks.TryAdd(id, service.Scan(path)))
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

        return _tasks[id];
    }
}