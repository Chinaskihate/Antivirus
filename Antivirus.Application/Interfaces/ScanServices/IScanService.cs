using Antivirus.Domain.Models;

namespace Antivirus.Application.Interfaces.ScanServices;

public interface IScanService
{
    Task<ScanResult> ScanAsync(string path);
}