namespace DisasterAlertSystemAPI.Models
{
    public class Regions
    {
        public string RegionId { get; set; }
        public LocationCoordinates LocationCoordinates { get; set; }
        // เก็บเป็น LIST เพื่อว่ามีมากกว่า 1 เหตุการณ์ Ex earthquake and flood and fire
        public List<string> DisasterTypes { get; set; }
    }

    public class LocationCoordinates
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
