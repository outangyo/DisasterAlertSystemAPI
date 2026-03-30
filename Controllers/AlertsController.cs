using DisasterAlertSystemAPI.Data;
using DisasterAlertSystemAPI.Models;
using DisasterAlertSystemAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlertSystemAPI.Controllers
{
    [Route("api/alerts")]
    [ApiController]
    public class AlertsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly AlertService _alertService;
        private readonly ILogger<AlertsController> _logger;

        public AlertsController(AppDbContext context, AlertService alertService, ILogger<AlertsController> logger)
        {
            _appDbContext = context;
            _alertService = alertService;
            _logger = logger;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendAlert([FromBody] AlertData request)
        {
            // เบอร์มือถือจำลอง
            string phoneNumber = "+66123456789";
            try
            {
                // เรียกใช้งานตรงๆ
                bool isSent = await _alertService.SendAlertAsync(phoneNumber, request.Message);

                if (isSent)
                {
                    request.Timestamp = DateTime.UtcNow;
                    _appDbContext.alertDatas.Add(request);
                    await _appDbContext.SaveChangesAsync();

                    return Ok(new { status = "Success", message = "Alert sent and recorded.", data = request });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Send Alert: {ex.Message}");
                return StatusCode(500, "Failed to send alert");
            }
            return StatusCode(500, "Failed to send alert");
        }

        [HttpGet]
        public async Task<IActionResult> GetAlerts()
        {
            var alerts = await _appDbContext.alertDatas.OrderByDescending(a => a.Timestamp).ToListAsync();
            return Ok(alerts);
        }
    }
}
