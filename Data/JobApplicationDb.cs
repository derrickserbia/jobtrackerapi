using JobTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTrackerApi.Data;

class JobApplicationDb : DbContext
{
    public JobApplicationDb(DbContextOptions<JobApplicationDb> options) : base(options) { }
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
}