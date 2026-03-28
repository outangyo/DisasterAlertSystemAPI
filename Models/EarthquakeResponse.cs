namespace DisasterAlertSystemAPI.Models
{
    public class EarthquakeResponse
    {
        public List<Feature> Features { get; set; }
    }
    public class Feature
    {
        public Properties Properties { get; set; }
    }

    public class Properties
    {
        // Magnitude
        public double? Mag { get; set; }
    }
}
