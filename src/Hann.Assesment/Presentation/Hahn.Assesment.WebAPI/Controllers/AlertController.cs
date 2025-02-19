using Hahn.Assesment.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.Assesment.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlertController : ControllerBase
    {
        private readonly IAlertAppService _alertAppService;
        private readonly ILogger<AlertController> _logger;

        public AlertController(IAlertAppService alertAppService, ILogger<AlertController> logger)
        {
            _alertAppService = alertAppService;
            _logger = logger;
        }

        [HttpGet("alerts")]
        public async Task<IActionResult> GetAlertsAsync()
        {
            var alerts = await _alertAppService.GetAlertsAsync();
            return Ok(alerts);
        }

        [HttpGet("current-alert")]
        public async Task<IActionResult> GetCurrentAlertAsync()
        {
            var alert = await _alertAppService.GetCurrentAlertAsync();
            if (alert == null)
            {
                _logger.LogInformation("No alert found in database.");
                return new NotFoundObjectResult("No Alert found.");
            }

            return Ok(alert);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlertsAsync(Guid id)
        {
            var alert = await _alertAppService.GetAlertByIdAsync(id);
            if (alert == null)
            {
                _logger.LogInformation($"Alert id '{id}' found in database.");
                return new NotFoundObjectResult("No Alert found with the id provided.");
            }
            return Ok(alert);
        }

        [HttpGet("{alertId}/categories")]
        public async Task<IActionResult> GetCategoriesAsync(Guid alertId)
        {
            var categories = await _alertAppService.GetCategoriesAsync(alertId);
            return Ok(categories);
        }

        [HttpGet("{alertId}/reports")]
        public async Task<IActionResult> GetReportsAsync(Guid alertId)
        {
            var reports = await _alertAppService.GetReportsAsync(alertId);
            return Ok(reports);
        }
    }
}
