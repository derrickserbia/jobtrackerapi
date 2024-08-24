using JobTrackerApi.Models;

public class JobApplicationQueryParameters : QueryParameters
{
    public int? JobApplicationStatusId { get; set; }
    public string? SearchString { get; set; }
}