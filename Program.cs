using System.Net;
using Azure;
// using Azure.AI.DocumentIntelligence;
using Azure.AI.FormRecognizer.DocumentAnalysis;
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

app.MapGet("/jobapplications/extract", async (string postingUrl) =>
{
    using WebClient webClient = new WebClient();
    string htmlContent = webClient.DownloadString(postingUrl);

    string azureEndpoint = "";
    string key = "";
    AzureKeyCredential credential = new AzureKeyCredential(key);
    // DocumentIntelligenceClient documentIntelligenceClient = new DocumentIntelligenceClient(new Uri(azureEndpoint), credential);
    DocumentAnalysisClient documentAnalysisClient = new DocumentAnalysisClient(new Uri(azureEndpoint), credential);


    using var stream = File.OpenRead(htmlContent);
    AnalyzeDocumentOperation operation = await documentAnalysisClient.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-layout", stream);
    await operation.WaitForCompletionAsync();
    AnalyzeResult result = operation.Value;

    Dictionary<string, string> extractedFields = new Dictionary<string, string>();

    // Extract Job Title (if present as a heading or in a specific element)
    foreach (DocumentPage page in result.Pages)
    {
        foreach (DocumentLine line in page.Lines)
        {
            if (line.Content.Contains("Job Title") || line.Content.Contains("Position"))
            {
                string jobTitle = line.Content.Replace("Job Title:", "").Replace("Position:", "").Trim();
                extractedFields["jobTitle"] = jobTitle;
                break;  // Assuming only one job title
            }
        }
    }

    // Extract Salary Information (assuming it's in a specific format)
    foreach (DocumentLine line in result.Pages[0].Lines)  // Start from the first page
    {
        if (line.Content.Contains("Salary") || line.Content.Contains("Compensation"))
        {
            string salaryRange = line.Content;
            // Use regular expressions or string manipulation to extract min and max
            // (Adapt this to match your specific salary format)
            string[] parts = salaryRange.Split('-');
            if (parts.Length == 2)
            {
                extractedFields["minSalary"] = parts[0].Trim();
                extractedFields["maxSalary"] = parts[1].Trim();
            }
            break; // Assuming only one salary range
        }
    }

    // Uri fileUri = new Uri (postingUrl);
    // AnalyzeDocumentContent content = new AnalyzeDocumentContent()
    // {
    //     UrlSource= fileUri
    // };
    // Operation<AnalyzeResult> operation = await documentIntelligenceClient.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-layout", content);
    // AnalyzeResult result = operation.Value;

    return Results.Ok(postingUrl);
});

app.Run();
