using DisasterAlertSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DisasterAlertSystemAPI.Controllers
{
    [Route("api/test-weather")]
    [ApiController]
    public class TestWeatherController : ControllerBase
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string apiKey = "a54601777fb0a91a23ddd75b05270386";

        [HttpGet]
        public async Task<IActionResult> GetWeather()
        {
            double latitude = 13.7563;
            double longitude = 100.5018;

            // จัดการต่อ String URL ให้ตรงตาม Format ของ OpenWeather (ใส่ units=metric เพื่อให้อุณหภูมิเป็นเซลเซียส)
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric";

            // ยิง GET Request ไปที่ OpenWeather
            var response = await _httpClient.GetAsync(url);

            // อ่านค่าที่เขาตอบกลับมาเป็น String (JSON)
            var jsonString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                // ถ้ามี Error ให้ response error กลับไป
                return BadRequest($"Error from OpenWeather: {jsonString}");
            }

            // แปลง JSON ให้กลายเป็น Model C#
            var weatherData = JsonSerializer.Deserialize<WeatherResponse>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Ok(weatherData);
        }

    }
}
