using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace JobTrackerApi;

public record JobApplication
{
    public enum ApplicationStatus
    {
        [Description("Application is pending review.")]
        Pending,

        [Description("Interview has been scheduled.")]
        InterviewScheduled,

        [Description("Application was rejected by the employer.")]
        RejectedByEmployer,

        [Description("Application was withdrawn by me.")]
        Withdrawn,

        [Description("Job offer has been received.")]
        OfferReceived,

        [Description("Job offer has been accepted.")]
        OfferAccepted
    }
    public int Id { get; set; }
    public required string JobTitle { get; set; }
    public required string CompanyName { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime DateApplied { get; set; }
    public string? JobDescription { get; set; }
    public string? Notes { get; set; }
    public decimal? Salary { get; set; }
}

class JobApplicationDb : DbContext
{
    public JobApplicationDb(DbContextOptions<JobApplicationDb> options) : base(options) { }
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
}