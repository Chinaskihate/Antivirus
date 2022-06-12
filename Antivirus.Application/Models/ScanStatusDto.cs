using Antivirus.Domain.Models;

namespace Antivirus.Application.Models;

public class ScanStatusDto
{
    public bool IsCompleted { get; set; }

    public ScanStatus Status { get; set; }
}