using DisasterAlertSystemAPI.Models;

namespace DisasterAlertSystemAPI.Services
{
    public static class DisasterRiskService
    {
        public static int CalculateScore(string type, WeatherResponse weatherData, EarthquakeResponse earthquakeData)
        {
            type = type.ToLower();
            if (type.ToLower() == "flood")
            {
                // สูตร: (ปริมาณฝน / 50) * 100
                double rain = weatherData.Rain?.Rain1h ?? 0;

                return (int)Math.Min((rain / 50.0) * 100, 100);
            }
            else if (type == "wildfire")
            {
                // โจทย์: high temperatures with low humidity increase the risk
                // โจทย์ไม่ได้กำหนดสูตรชัดเจนเลยทำการประมานค่าเอง
                double temp = weatherData.Main?.Temp ?? 0;
                double humidity = weatherData.Main?.Humidity ?? 100;

                if (temp > 40 && humidity < 30)
                {
                    return 80; // High risk
                }
                else if (temp > 30 && humidity < 50)
                {
                    return 50; // Medium risk
                }
                return 20; // Low risk
            }
            else if (type == "earthquake")
            {
                // โจทย์: Magnitude มากกว่า 5.0 คือ high risk
                double Magnitude = earthquakeData?.Features?.FirstOrDefault()?.Properties?.Mag ?? 0;

                if (Magnitude >= 5.0)
                {
                    return 100; // High risk
                }
                if (Magnitude >= 3.0)
                {
                    return 50; // Medium risk
                }
                return 10;  // Low risk
            }
            return 20; // Default score
        }

        public static string ScoreLevel(int score)
        {
            if (score >= 75)
            {
                return "High";
            }
            if (score >= 40)
            {
                return "Medium";
            }
            return "Low";
        }
    }
}
