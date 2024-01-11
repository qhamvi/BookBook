namespace Shared.RequestFeatures;

public class ListResponse<T> where T : class
{
    public IEnumerable<T> Result { get; set; }
    public MetaData MetaData { get; set; }
}
