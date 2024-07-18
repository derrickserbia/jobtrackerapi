using JobTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

class JobApplicationDb : DbContext
{
    public JobApplicationDb(DbContextOptions<JobApplicationDb> options) : base(options) { }
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
}