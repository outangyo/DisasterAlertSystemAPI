using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace DisasterAlertSystemAPI.Models
{
    public class AlertSettings
    {
        public string RegionId { get; set; }
        public string DisasterTypes { get; set; }
        public int ThresholdScore { get; set; }
    }
}
