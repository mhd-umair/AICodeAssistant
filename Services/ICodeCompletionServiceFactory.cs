namespace AICodeAssistant.Services
{
    public interface ICodeCompletionServiceFactory
    {
        ICodeCompletionService CreateService(string provider);
    }
}
