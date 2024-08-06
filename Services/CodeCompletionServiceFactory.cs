namespace AICodeAssistant.Services
{
    public class CodeCompletionServiceFactory : ICodeCompletionServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CodeCompletionServiceFactory> _logger;

        public CodeCompletionServiceFactory(IServiceProvider serviceProvider, ILogger<CodeCompletionServiceFactory> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public ICodeCompletionService CreateService(string provider)
        {
            try
            {
                _logger.LogInformation($"Creating code completion service for provider: {provider}", provider);
                return provider switch
                {
                    "OpenAI" => _serviceProvider.GetRequiredService<OpenAIService>(),
                    "Mock" => _serviceProvider.GetRequiredService<MockCodeCompletionService>(),
                    _ => throw new ArgumentException("Invalid code completion service provider")
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating code completion service for provider: {provider}", ex, provider);
                throw;
            }
        }
    }

}
