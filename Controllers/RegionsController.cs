using DisasterAlertSystemAPI.Data;
using DisasterAlertSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DisasterAlertSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(ILogger<RegionsController> logger, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        // POST /api/regions
        [HttpPost]
        public IActionResult AddRegion([FromBody] Region newRegion)
        {
            try
            {
                _appDbContext.regions.Add(newRegion);
                _appDbContext.SaveChanges();

                _logger.LogInformation($"Region {newRegion.RegionId} add success.");

                return Ok(newRegion);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding is: {ex.Message}");

                return StatusCode(500, "An error occurred while saving the region.");
            }

        }

        [HttpGet]
        public IActionResult GetAllRegions()
        {
            var regionsList = _appDbContext.regions.ToList();

            return Ok(regionsList);
        }
    }
}
