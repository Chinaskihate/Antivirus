using System.Collections.Concurrent;
using Antivirus.Application.Interfaces.ScanManagers;
using Antivirus.Application.Interfaces.ScanServices;
using Antivirus.Application.Models;
using Antivirus.Application.Services.ScanServices;
using Antivirus.Domain.Models;

namespace Antivirus.Application.Services.ScanManagers;

public class ScanManager : IScanManager
{
    private readonly ConcurrentDictionary<Guid, ScanStatus> _statuses;

    /// <summary>
    ///     Constructor.
    /// </summary>
    public ScanManager()
    {
        _statuses = new ConcurrentDictionary<Guid, ScanStatus>();
    }

    public Guid CreateScan(string path)
    {
        var id = Guid.NewGuid();
        IScanService service = new ScanService();
        if (_statuses.TryAdd(id, service.Scan(path)))
        {
            return id;
        }

        ;

        return Guid.Empty;
    }

    public ScanStatusDto GetStatus(Guid id)
    {
        if (!_statuses.ContainsKey(id))
        {
            throw new ArgumentException($"Scan {id} doesn't exist.");
        }

        var currStatus = _statuses[id];

        return new ScanStatusDto
        {
            IsCompleted = currStatus.IsFinished,
            Status = currStatus
        };
    }
}