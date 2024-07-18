using Microsoft.EntityFrameworkCore;

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
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }
    public string? PostingUrl { get; set; }
    public string? HiringTeam { get; set; }
    public List<Skill> TechStack { get; set; } = new();
}

class JobApplicationDb : DbContext
{
    public JobApplicationDb(DbContextOptions<JobApplicationDb> options) : base(options) { }
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
}