using JobTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTrackerApi.Data;

public class JobApplicationDbContext : DbContext
{
    public JobApplicationDbContext(DbContextOptions<JobApplicationDbContext> options) : base(options) { }
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
}