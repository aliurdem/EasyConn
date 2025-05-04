namespace EasyConnect.Models.Utilities.Filtering
{
    public class FilterParameters
    {
        public List<Filter> Filters { get; set; } = new List<Filter>();
        public string OrderProp { get; set; } = "";
    }
}
