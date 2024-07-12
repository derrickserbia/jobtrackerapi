namespace JobTrackerApi.Models;

public record JobApplication
{
    public int Id { get; set; }
    public required string JobTitle { get; set; }
    public required string CompanyName { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime DateApplied { get; set; }
    public string? JobDescription { get; set; }
    public string? Notes { get; set; }
    public decimal? Salary { get; set; }
}