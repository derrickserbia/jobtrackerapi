namespace JobTrackerApi.Models;
public class QueryParameters
{
    const int _maxSize = 50;
    private int _size = 25;

    public int Page { get; set; } = 1;
    public int Size
    {
        get { return _size; }
        set { _size = Math.Min(_maxSize, value); }
    }
}