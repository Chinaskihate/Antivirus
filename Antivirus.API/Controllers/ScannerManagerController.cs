using Antivirus.Application.Interfaces;
using Antivirus.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Antivirus.API.Controllers
{
    [ApiController]
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    public class ScannerManagerController : Controller
    {
        private readonly IScanService _scanService;

        public ScannerManagerController(IScanService scanService)
        {
            _scanService = scanService;
        }

        [HttpGet]
        public async Task<ScanResult> Test(string path)
        {
            var result = await _scanService.ScanAsync(path);
            return result;
        }
    }
}
