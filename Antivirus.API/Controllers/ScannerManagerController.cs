using Antivirus.API.Models;
using Antivirus.Application.Interfaces.ScanManagers;
using Antivirus.Application.Models;
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
    public async Task<int> CreateScan([FromBody] CreateScanRequest request)
    {
        var result = _scanManager.CreateScan(request.Path);
        return result;
    }

    [HttpGet]
    public async Task<ScanStatusDto> GetScanStatus(int id)
    {
        var result = _scanManager.GetStatus(id);
        return result;
    }
}