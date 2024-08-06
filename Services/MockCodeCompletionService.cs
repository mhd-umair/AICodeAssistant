namespace AICodeAssistant.Services
{
    public class MockCodeCompletionService : ICodeCompletionService
    {
        public Task<string> GetCodeSuggestionAsync(string code)
        {
            // Return a mock suggestion
            return Task.FromResult($"Mock suggestion for: {code}");
        }
    }
}
