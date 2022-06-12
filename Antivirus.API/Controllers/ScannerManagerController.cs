using Antivirus.Application.Interfaces.ScanManagers;
using Antivirus.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Antivirus.API.Controllers;

[ApiController]
[ApiVersionNeutral]
[Route("api/[controller]")]
public class ScannerManagerController : Controller
{
    private readonly IScanManager _scanManager;

    public ScannerManagerController(IScanManager scanManager)
    {
        _scanManager = scanManager;
    }

    [HttpPost]
    public async Task<Guid> CreateScan(string path)
    {
        var result = _scanManager.CreateScan(path);
        return result;
    }

    [HttpGet]
    public async Task<ScanStatus> GetScanStatus(Guid id)
    {
        var result = _scanManager.GetStatus(id);
        return result;
    }
}