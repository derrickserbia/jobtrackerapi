using JobTrackerApi.Data;
using JobTrackerApi.Models;
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

app.MapGet("/jobapplications/{id}", async (JobApplicationDb db, int id) => 
{
    var jobApplication = await db.JobApplications.FindAsync(id);
    return jobApplication is null ? Results.NotFound() : Results.Ok(jobApplication);

});

app.MapPost("/jobapplications", async (JobApplicationDb db, JobApplication jobApplication) => 
{
    await db.JobApplications.AddAsync(jobApplication);
    await db.SaveChangesAsync();
    return Results.Created($"/jobapplications/{jobApplication.Id}", jobApplication);
});

app.MapPut("/jobapplications/{id}", async (JobApplicationDb db, JobApplication updatedJobApplication, int id) =>
{
    var jobApplication = await db.JobApplications.FindAsync(id);
    if (jobApplication is null) return Results.NotFound();
    
    jobApplication.JobTitle = updatedJobApplication.JobTitle != jobApplication.JobTitle? updatedJobApplication.JobTitle : jobApplication.JobTitle;
    jobApplication.CompanyName = updatedJobApplication.CompanyName != jobApplication.CompanyName? updatedJobApplication.CompanyName : jobApplication.CompanyName;
    jobApplication.Status = updatedJobApplication.Status != jobApplication.Status? updatedJobApplication.Status : jobApplication.Status;
    jobApplication.DateApplied = updatedJobApplication.DateApplied != jobApplication.DateApplied? updatedJobApplication.DateApplied : jobApplication.DateApplied;
    jobApplication.JobDescription = updatedJobApplication.JobDescription != jobApplication.JobDescription? updatedJobApplication.JobDescription : jobApplication.JobDescription;
    jobApplication.Notes = updatedJobApplication.Notes != jobApplication.Notes? updatedJobApplication.Notes : jobApplication.Notes;
    jobApplication.MinSalary = updatedJobApplication.MinSalary != jobApplication.MinSalary? updatedJobApplication.MinSalary : jobApplication.MinSalary;
    jobApplication.MaxSalary = updatedJobApplication.MaxSalary != jobApplication.MaxSalary? updatedJobApplication.MaxSalary : jobApplication.MaxSalary;
    jobApplication.PostingUrl = updatedJobApplication.PostingUrl != jobApplication.PostingUrl ? updatedJobApplication.PostingUrl : jobApplication.PostingUrl;
    jobApplication.HiringTeam = updatedJobApplication.HiringTeam != jobApplication.HiringTeam ? updatedJobApplication.HiringTeam : jobApplication.HiringTeam;
    jobApplication.TechStack = updatedJobApplication.TechStack != jobApplication.TechStack ? updatedJobApplication.TechStack : jobApplication.TechStack;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/jobapplications/{id}", async (JobApplicationDb db, int id) =>
{
    var jobApplication = await db.JobApplications.FindAsync(id);
    if (jobApplication is null) return Results.NotFound();
    db.JobApplications.Remove(jobApplication);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/jobapplications/extract", (string postingUrl) =>
{
    return Results.Ok(postingUrl);
});

app.Run();
