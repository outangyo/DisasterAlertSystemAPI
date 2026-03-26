using DisasterAlertSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DisasterAlertSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private static readonly List<Regions> _regions = new List<Regions>()
        {
            new Regions
            {
                RegionId = "R1",
                LocationCoordinates = new LocationCoordinates { latitude = 13.7563, longitude = 100.5018 },
                DisasterTypes = new List<string> { "Flood", "Earthquake" }
            },
            new Regions
            {
                RegionId = "R2",
                LocationCoordinates = new LocationCoordinates { latitude = 18.7883, longitude = 98.9853 },
                DisasterTypes = new List<string> { "Wildfire" }
            }
        };

        // POST /api/regions
        [HttpPost]
        public IActionResult AddRegion([FromBody] Regions newRegion)
        {
            _regions.Add(newRegion);

            return Ok(newRegion);
        }

        [HttpGet]
        public IActionResult GetAllRegions()
        {
            return Ok(_regions);
        }
    }
}
