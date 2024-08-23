using Microsoft.EntityFrameworkCore;

namespace JobTrackerApi.Models;

public class JobApplicationContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public JobApplicationContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("JobTrackerDatabase"));
    }

    public DbSet<JobApplication> JobApplications { get; set; }
}
