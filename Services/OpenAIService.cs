using AICodeAssistant.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AICodeAssistant.Services
{
    public class OpenAIService : ICodeCompletionService
    {
        private readonly OpenAISettings _settings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<OpenAIService> _logger;

        public OpenAIService(IOptions<OpenAISettings> settings, ILogger<OpenAIService> logger)
        {
            _settings = settings.Value;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_settings.BaseUri);

            _logger = logger;
        }

        public async Task<string> GetCodeSuggestionAsync(string code)
        {
            try
            {
                _logger.LogInformation("Requesting code suggestion from OpenAI");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.ApiKey);

                var requestContent = new
                {
                    prompt = code,
                    max_tokens = 100,
                    temperature = 0.5
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/completions", content);

                response.EnsureSuccessStatusCode();

                _logger.LogInformation("Received code suggestion from OpenAI");
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting code suggestion from OpenAI");
                return "Error fetching suggestion";
            }
        }
    }
}
