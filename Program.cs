using JobTrackerApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("JobApplications") ?? "Data Source=JobApplications.db";

builder.Services.AddSqlite<JobApplicationDb>(connectionString);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "JobTracker API",
        Description = "Track you job applications",
        Version = "v1"});
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobTracker API V1");
    });
}

app.MapGet("/jobapplications", async (JobApplicationDb db) => await db.JobApplications.ToListAsync());
app.MapPost("/jobapplication", async (JobApplicationDb db, JobApplication jobApplication) => 
{
    await db.JobApplications.AddAsync(jobApplication);
    await db.SaveChangesAsync();
    return Results.Created($"/jobapplication/{jobApplication.Id}", jobApplication);
});


app.Run();
