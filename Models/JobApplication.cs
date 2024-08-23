namespace JobTrackerApi.Models;
public class JobApplication
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string JobTitle { get; set; }
    public required string CompanyName { get; set; }
    public string HiringTeam { get; set; } = string.Empty;
    public string JobPostingUrl { get; set; } = string.Empty;
    public string JobDescription { get; set; } = string.Empty;
    public decimal MinSalary { get; set; } = 0;
    public decimal MaxSalary { get; set; } = 0;
    public int JobApplicationStatusId { get; set; } = (int)JobApplicationStatus.Applied;
    public string Notes { get; set; } = string.Empty;
    public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
}
