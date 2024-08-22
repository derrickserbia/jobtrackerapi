using JobTrackerApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobTrackerApi.Infrastructure.Data;

public class JobApplicationDbContext : DbContext
{
    public JobApplicationDbContext(DbContextOptions<JobApplicationDbContext> options) : base(options) { }
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
}