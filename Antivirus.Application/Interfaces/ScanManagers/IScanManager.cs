using Antivirus.Application.Models;
using Antivirus.Domain.Models;

namespace Antivirus.Application.Interfaces.ScanManagers;

public interface IScanManager
{
    Guid CreateScan(string apth);

    ScanStatus GetStatus(Guid id);
}