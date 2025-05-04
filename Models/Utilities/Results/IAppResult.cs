namespace EasyConnect.Models.Utilities.Results
{
    public interface IAppResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
