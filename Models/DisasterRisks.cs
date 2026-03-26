namespace DisasterAlertSystemAPI.Models
{
    public class DisasterRisks
    {
        public string RegionId { get; set; }
        public string DisasterType { get; set; }
        public int RiskScore { get; set; }
        public string RiskLevel { get; set; }
        public bool AlertTriggered { get; set; }
    }
}
