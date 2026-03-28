namespace DisasterAlertSystemAPI.Services
{
    public class AlertService
    {
        private readonly ILogger<AlertService> _logger;

        public AlertService(ILogger<AlertService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SendAlertAsync(string to, string message)
        {
            // จำลองการพ่น Log ออก Console
            _logger.LogWarning($"[MOCK SMS/EMAIL] Sending alert to {to}... Message: {message}");

            return true;
        }
    }
}
