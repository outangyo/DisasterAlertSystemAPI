using Microsoft.EntityFrameworkCore;

namespace DisasterAlertSystemAPI.Models
{
    public class Region
    {
        public string RegionId { get; set; }
        public LocationCoordinates LocationCoordinates { get; set; }
        // เก็บเป็น LIST เพื่อว่ามีมากกว่า 1 เหตุการณ์ Ex earthquake and flood and fire
        public List<string> DisasterTypes { get; set; }
    }

    [Owned]
    public class LocationCoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
