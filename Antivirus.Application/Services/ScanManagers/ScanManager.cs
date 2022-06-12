using System.Collections.Concurrent;
using Antivirus.Application.Common.Exceptions;
using Antivirus.Application.Interfaces.ScanManagers;
using Antivirus.Application.Interfaces.ScanServices;
using Antivirus.Application.Services.ScanServices;
using Antivirus.Domain.Models;

namespace Antivirus.Application.Services.ScanManagers;

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

    public int CreateScan(string path)
    {
        int id = _tasks.Count;
        IScanService service = new ScanService();
        if (_tasks.TryAdd(id, service.Scan(path)))
        {
            return id;
        }

        return -1;
    }

    public ScanStatus GetStatus(int id)
    {
        if (!_tasks.ContainsKey(id))
        {
            throw new ScanNotFoundException(id, $"Scan {id} doesn't exist.");
        }

        return _tasks[id];
    }
}