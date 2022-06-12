using Antivirus.Application.Models;
using Antivirus.Domain.Models;

namespace Antivirus.Application.Interfaces.ScanManagers;

public interface IScanManager
{
    int CreateScan(string path);

    ScanStatusDto GetStatus(int id);
}