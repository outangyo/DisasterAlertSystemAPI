using DisasterAlertSystemAPI.Data;
using DisasterAlertSystemAPI.Models;
using DisasterAlertSystemAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DisasterAlertSystemAPI.Controllers
{
    [Route("api/disaster-risks")]
    [ApiController]
    public class DisasterRisksController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<DisasterRisksController> _logger;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiKeyFromOpenWeather = "a54601777fb0a91a23ddd75b05270386";

        public DisasterRisksController(ILogger<DisasterRisksController> logger, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDisasterRisks()
        {
            try
            {
                var regions = await _appDbContext.regions.ToListAsync();
                var result = new List<DisasterRisk>();

                // Error Handling Regions with no available data
                if (regions == null || !regions.Any())
                {
                    _logger.LogWarning("No regions found in the database.");
                    return NotFound("No regions found.");
                }

                foreach (var region in regions)
                {
                    WeatherResponse weather = null;
                    EarthquakeResponse earthquakeData = null;

                    try
                    {
                        string weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={region.LocationCoordinates.Latitude}" +
                       $"&lon={region.LocationCoordinates.Longitude}" +
                       $"&appid={_apiKeyFromOpenWeather}&units=metric";

                        var weatherResponse = await _httpClient.GetAsync(weatherUrl);

                        // Error Handling for external api OpenWeather
                        if (!weatherResponse.IsSuccessStatusCode)
                        {
                            _logger.LogError($"Failed to get weather data from OpenWeather: {region.RegionId}");
                            continue; // เพื่อให้โปรเเกรมทำงานต่อ
                        }

                        var weatherJson = await weatherResponse.Content.ReadAsStringAsync();
                        weather = JsonSerializer.Deserialize<WeatherResponse>(weatherJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Network Error getting OpenWeather for {region.RegionId}: {ex.Message}");
                        continue;
                    }

                    if (region.DisasterTypes.Any(t => t.ToLower() == "earthquake"))
                    {
                        try
                        {
                            string earthquakeUrl = $"https://earthquake.usgs.gov/fdsnws/event/1/query?format=geojson&latitude={region.LocationCoordinates.Latitude}&longitude={region.LocationCoordinates.Longitude}&maxradiuskm=100&limit=1";
                            var earthquakeResponse = await _httpClient.GetAsync(earthquakeUrl);

                            // Error Handling for external api USGS
                            if (!earthquakeResponse.IsSuccessStatusCode)
                            {
                                _logger.LogWarning($"Failed to get earthquake data from USGS: {region.RegionId}.");
                            }
                            else
                            {
                                var earthquakeJson = await earthquakeResponse.Content.ReadAsStringAsync();
                                earthquakeData = JsonSerializer.Deserialize<EarthquakeResponse>(earthquakeJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning($"Network Error getting USGS for {region.RegionId}: {ex.Message}");
                        }
                    }

                    foreach (var type in region.DisasterTypes)
                    {
                        // Error Handling Missing data from external sources
                        int score = DisasterRiskService.CalculateScore(type, weather, earthquakeData);
                        var setting = _appDbContext.alertSettings.FirstOrDefault(s => s.RegionId == region.RegionId && s.DisasterTypes == type);

                        // ใช้ค่า default หรือปรับค่าได้
                        int threshold = setting?.ThresholdScore ?? 80;

                        var risk = new DisasterRisk
                        {
                            RegionId = region.RegionId,
                            DisasterType = type,
                            RiskScore = score,
                            RiskLevel = DisasterRiskService.ScoreLevel(score),
                            AlertTriggered = score >= threshold
                        };

                        result.Add(risk);
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting disaster risks: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving disaster risks.");
            }
        }
    }
}
