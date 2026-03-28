namespace DisasterAlertSystemAPI.Models
{
    public class AlertData
    {
        public int Id { get; set; }
        public string RegionId { get; set; }
        public string DisasterType { get; set; }
        public string RiskLevel { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
