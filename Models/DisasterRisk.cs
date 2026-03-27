using Microsoft.EntityFrameworkCore;

namespace DisasterAlertSystemAPI.Models
{
    // ใช้สอง column นี้เป็น KEY
    [PrimaryKey(nameof(RegionId), nameof(DisasterType))]
    public class DisasterRisk
    {
        public string RegionId { get; set; }
        public string DisasterType { get; set; }
        public int RiskScore { get; set; }
        public string RiskLevel { get; set; }
        public bool AlertTriggered { get; set; }
    }
}
