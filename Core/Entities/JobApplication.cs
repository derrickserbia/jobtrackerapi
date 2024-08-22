namespace JobTrackerApi.Core.Entities;

public class JobApplication
{
    public int Id { get; set; }
    public required string JobTitle { get; set; }
    public required string CompanyName { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime DateApplied { get; set; }
    public string? JobDescription { get; set; }
    public string? Notes { get; set; }
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }
    public string? PostingUrl { get; set; }
    public string? HiringTeam { get; set; }
    public List<Skill> TechStack { get; set; } = new();

    public bool HasKeyword(string searchString)
    {
        var lower = searchString.ToLower();
        return JobTitle.ToLower().Contains(lower)
            || CompanyName.ToLower().Contains(lower)
            || (!string.IsNullOrEmpty(JobDescription) && JobDescription.ToLower().Contains(lower))
            || (!string.IsNullOrEmpty(Notes) && Notes.ToLower().Contains(lower))
            || (!string.IsNullOrEmpty(PostingUrl) && PostingUrl.ToLower().Contains(lower))
            || (!string.IsNullOrEmpty(HiringTeam) && HiringTeam.ToLower().Contains(lower));
    }
}