namespace EasyConnect.Models.Utilities.Results
{
    public interface IDataResult<out T> : IAppResult
    {
        T Data { get; }
    }
}
