using Antivirus.Domain.Models;

namespace Antivirus.Application.Interfaces.ScanServices;

public interface IScanService
{
    ScanResult ScanAsync(string path);
}