using System.Text.Json.Serialization;

namespace DisasterAlertSystemAPI.Models
{
    public class WeatherResponse
    {
        public MainData Main { get; set; }
        public RainData Rain { get; set; }
    }

    public class MainData
    {
        public double Temp { get; set; }
        public double Humidity { get; set; }
    }

    public class RainData 
    {
        [JsonPropertyName("1h")]
        public double Rain1h { get; set; }
    }
}
