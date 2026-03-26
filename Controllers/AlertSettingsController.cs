using DisasterAlertSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DisasterAlertSystemAPI.Controllers
{
    [Route("api/alert-settings")]
    [ApiController]
    public class AlertSettingsController : ControllerBase
    {
        private static readonly List<AlertSettings> _alertSettings = new List<AlertSettings>()
        {
            new AlertSettings
            {
                RegionId = "R1",
                DisasterTypes = "flood",
                ThresholdScore = 75
            },
            new AlertSettings
            {
                RegionId = "R2",
                DisasterTypes = "wildfire",
                ThresholdScore = 80
            }
        };

        // POST /api/alert-settings
        [HttpPost]
        public IActionResult AlertSettingsConfigure([FromBody] AlertSettings setting)
        {
            // oldSetting คือค่า setting เดิมที่มีอยู่เเล้วในระบบ
            var oldSetting = _alertSettings.FirstOrDefault(s => s.RegionId == setting.RegionId &&
            s.DisasterTypes == setting.DisasterTypes);

            // ทำการเช็คค่า ถ้าไม่มีอยู่ในทำการ setting ใหม่เข้าไป
            if (oldSetting == null)
            {
                _alertSettings.Add(setting);
            }
            else
            {
                // ถ้ามีอยู่เเล้วทำการอัพเดทค่าใหม่เข้าไป
                oldSetting.ThresholdScore = setting.ThresholdScore;
            }
            return Ok(setting);
        }

        // GET /api/alert-settings
        [HttpGet]
        public IActionResult GetAllSettings()
        {
            return Ok(_alertSettings);
        }
    }
}
