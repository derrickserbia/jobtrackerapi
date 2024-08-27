using JobTrackerApi.Models;

public class Resource<T>
{
    public T Data { get; set; }
    public List<Link> Links { get; set; } = [];

    public Resource(T data)
    {
        Data = data;
    }

    public void AddLink(string rel, string href, string method)
    {
        Links.Add(new Link { Rel = rel, Href = href, Method = method });
    }
}