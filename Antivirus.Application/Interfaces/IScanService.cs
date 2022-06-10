using Antivirus.Domain.Models;

namespace Antivirus.Application.Interfaces;

public interface IScanService
{
    Task<ScanResult> ScanAsync(string path);
}