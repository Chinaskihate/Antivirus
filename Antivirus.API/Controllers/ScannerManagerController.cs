using Microsoft.AspNetCore.Mvc;

namespace Antivirus.API.Controllers
{
    [ApiController]
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    public class ScannerManagerController : Controller
    {
        [HttpGet]
        public async Task<int> Test()
        {
            return 1;
        }
    }
}
