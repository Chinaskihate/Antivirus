using Antivirus.Domain.Models;

namespace Antivirus.Application.Interfaces.ScanManagers;

public interface IScanManager
{
    int CreateScan(string path);

    ScanStatus GetStatus(int id);
}