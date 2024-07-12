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

app.MapGet("/jobapplication/{id}", async (JobApplicationDb db, int id) => 
{
    var jobApplication = await db.JobApplications.FindAsync(id);
    return jobApplication is null ? Results.NotFound() : Results.Ok(jobApplication);

});

app.MapPost("/jobapplication", async (JobApplicationDb db, JobApplication jobApplication) => 
{
    await db.JobApplications.AddAsync(jobApplication);
    await db.SaveChangesAsync();
    return Results.Created($"/jobapplication/{jobApplication.Id}", jobApplication);
});

app.MapPut("/jobapplication/{id}", async (JobApplicationDb db, JobApplication updatedJobApplication, int id) =>
{
    var jobApplication = await db.JobApplications.FindAsync(id);
    if (jobApplication is null) return Results.NotFound();
    
    jobApplication.JobTitle = updatedJobApplication.JobTitle;
    jobApplication.CompanyName = updatedJobApplication.CompanyName;
    jobApplication.Status = updatedJobApplication.Status;
    jobApplication.DateApplied = updatedJobApplication.DateApplied;
    jobApplication.JobDescription = updatedJobApplication.JobDescription;
    jobApplication.Notes = updatedJobApplication.Notes;
    jobApplication.MinSalary = updatedJobApplication.MinSalary;
    jobApplication.MaxSalary = updatedJobApplication.MaxSalary;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/jobapplication/{id}", async (JobApplicationDb db, int id) =>
{
    var jobApplication = await db.JobApplications.FindAsync(id);
    if (jobApplication is null) return Results.NotFound();
    db.JobApplications.Remove(jobApplication);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
