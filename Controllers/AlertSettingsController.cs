using DisasterAlertSystemAPI.Data;
using DisasterAlertSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlertSystemAPI.Controllers
{
    [Route("api/alert-settings")]
    [ApiController]
    public class AlertSettingsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<AlertSettingsController> _logger;

        public AlertSettingsController(ILogger<AlertSettingsController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        // POST /api/alert-settings
        [HttpPost]
        public IActionResult AlertSettingsConfigure([FromBody] AlertSetting setting)
        {
            try
            {
                // oldSetting คือค่า setting เดิมที่มีอยู่เเล้วในระบบ
                var oldSetting = _appDbContext.alertSettings.FirstOrDefault(s =>
                s.RegionId == setting.RegionId &&
                s.DisasterTypes == setting.DisasterTypes);

                // ทำการเช็คค่า ถ้าไม่มีอยู่ในทำการ setting ใหม่เข้าไป
                if (oldSetting != null)
                {
                    // ถ้ามีอยู่เเล้วทำการอัพเดทค่าใหม่เข้าไป
                    oldSetting.ThresholdScore = setting.ThresholdScore;
                    _logger.LogInformation($"Updated threshold for {setting.RegionId} - {setting.DisasterTypes}");
                }
                else
                {
                    _appDbContext.alertSettings.Add(setting);
                    _logger.LogInformation($"Added new alert setting for {setting.RegionId} - {setting.DisasterTypes}");
                }
                _appDbContext.SaveChanges();
                return Ok(setting);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error configuring alert setting: {ex.Message}");
                return StatusCode(500, "An error occurred while configuring alert settings.");
            }
        }

        // GET /api/alert-settings
        [HttpGet]
        public IActionResult GetAllSettings()
        {
            return Ok(_alertSettings);
        }
    }
}
