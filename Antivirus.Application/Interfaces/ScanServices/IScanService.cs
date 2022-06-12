using Antivirus.Domain.Models;

namespace Antivirus.Application.Interfaces.ScanServices;

public interface IScanService
{
    ScanStatus Scan(string path);
}