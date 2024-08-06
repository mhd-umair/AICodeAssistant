using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using AICodeAssistant.Services;
using AICodeAssistant.Models;

namespace AICodeAssistant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodeAssistantController : ControllerBase
    {
        private readonly ICodeCompletionServiceFactory _serviceFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CodeAssistantController> _logger;

        public CodeAssistantController(ICodeCompletionServiceFactory serviceFactory, IConfiguration configuration, ILogger<CodeAssistantController> logger)
        {
            _serviceFactory = serviceFactory;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> GetCodeSuggestion([FromBody] CodeInputModel codeInput)
        {
            try
            {
                _logger.LogInformation("Received code suggestion request");
                var provider = _configuration.GetValue<string>("CodeCompletionService:Provider");
                var service = _serviceFactory.CreateService(provider);
                var suggestion = await service.GetCodeSuggestionAsync(codeInput.Code);
                _logger.LogInformation("Successfully processed code suggestion request");
                return Ok(suggestion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing code suggestion request");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }

}
