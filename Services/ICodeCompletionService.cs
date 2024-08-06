namespace AICodeAssistant.Services
{
    public interface ICodeCompletionService
    {
        Task<string> GetCodeSuggestionAsync(string code);
    }
}
