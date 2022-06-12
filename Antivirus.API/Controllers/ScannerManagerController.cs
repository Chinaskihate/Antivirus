using Antivirus.API.Models;
using Antivirus.Application.Interfaces.ScanManagers;
using Antivirus.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Antivirus.API.Controllers;

/// <summary>
///     Controller for scan manager.
/// </summary>
[ApiController]
[ApiVersionNeutral]
[Route("api/[controller]")]
public class ScannerManagerController : Controller
{
    private readonly IScanManager _scanManager;

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="scanManager"> Scan manager. </param>
    public ScannerManagerController(IScanManager scanManager)
    {
        _scanManager = scanManager;
    }

    /// <summary>
    ///     Creates new scan.
    /// </summary>
    /// <param name="request"> Request with directory to scan. </param>
    /// <returns> Id. </returns>
    /// <response code="200"> Success. </response>
    [HttpPost]
    public async Task<ActionResult<int>> CreateScan([FromBody] CreateScanRequest request)
    {
        var result = _scanManager.CreateScan(request.Path);
        return Ok(result);
    }

    /// <summary>
    ///     Get scan status.
    /// </summary>
    /// <param name="id"> Id of scan. </param>
    /// <returns> Current scan status. </returns>
    /// <response code="200">Success.</response>
    /// <response code="404"> If scan was not found.</response>
    [HttpGet]
    public async Task<ActionResult<ScanStatusDto>> GetScanStatus(int id)
    {
        var result = _scanManager.GetStatus(id);
        return Ok(result);
    }
}