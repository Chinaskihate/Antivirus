using Antivirus.Domain.Models;

namespace Antivirus.Application.Models;

public class ScanStatus
{
    public bool IsCompleted { get; set; }

    public ScanResult Result { get; set; }
}