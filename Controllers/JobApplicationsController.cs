using JobTrackerApi.Data;
using JobTrackerApi.Pagination;
using JobTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobTrackerApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class JobApplicationsController : ControllerBase
{
    private readonly JobApplicationDbContext _context;

    public JobApplicationsController(JobApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobApplication>>> GetJobApplications([FromQuery] PaginationFilter paginationFilter, string? searchString)
    {
        var filter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            var lower = searchString.ToLower();
            return await _context.JobApplications.Where(j =>
                j.JobTitle.ToLower().Contains(lower)
                || j.CompanyName.ToLower().Contains(lower)
                || (!string.IsNullOrEmpty(j.JobDescription) && j.JobDescription.ToLower().Contains(lower))
                || (!string.IsNullOrEmpty(j.Notes) && j.Notes.ToLower().Contains(lower))
                || (!string.IsNullOrEmpty(j.PostingUrl) && j.PostingUrl.ToLower().Contains(lower))
                || (!string.IsNullOrEmpty(j.HiringTeam) && j.HiringTeam.ToLower().Contains(lower)))
                .Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        }

        return await _context.JobApplications.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<JobApplication>> GetJobApplication(int id)
    {
        var jobApplication = await _context.JobApplications.FindAsync(id);
        return jobApplication is null ? NotFound() : jobApplication;
    }

    [HttpPost]
    public async Task<ActionResult<JobApplication>> PostJobApplication(JobApplication jobApplication)
    {
        _context.JobApplications.Add(jobApplication);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(PostJobApplication), new { id = jobApplication.Id }, jobApplication);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutJobApplication(JobApplication updatedJobApplication, int id)
    {
        if (id != updatedJobApplication.Id) return BadRequest();

        var jobApplication = await _context.JobApplications.FindAsync(id);
        if (jobApplication is null) return NotFound();

        jobApplication.JobTitle = updatedJobApplication.JobTitle != jobApplication.JobTitle ? updatedJobApplication.JobTitle : jobApplication.JobTitle;
        jobApplication.CompanyName = updatedJobApplication.CompanyName != jobApplication.CompanyName ? updatedJobApplication.CompanyName : jobApplication.CompanyName;
        jobApplication.Status = updatedJobApplication.Status != jobApplication.Status ? updatedJobApplication.Status : jobApplication.Status;
        jobApplication.DateApplied = updatedJobApplication.DateApplied != jobApplication.DateApplied ? updatedJobApplication.DateApplied : jobApplication.DateApplied;
        jobApplication.JobDescription = updatedJobApplication.JobDescription != jobApplication.JobDescription ? updatedJobApplication.JobDescription : jobApplication.JobDescription;
        jobApplication.Notes = updatedJobApplication.Notes != jobApplication.Notes ? updatedJobApplication.Notes : jobApplication.Notes;
        jobApplication.MinSalary = updatedJobApplication.MinSalary != jobApplication.MinSalary ? updatedJobApplication.MinSalary : jobApplication.MinSalary;
        jobApplication.MaxSalary = updatedJobApplication.MaxSalary != jobApplication.MaxSalary ? updatedJobApplication.MaxSalary : jobApplication.MaxSalary;
        jobApplication.PostingUrl = updatedJobApplication.PostingUrl != jobApplication.PostingUrl ? updatedJobApplication.PostingUrl : jobApplication.PostingUrl;
        jobApplication.HiringTeam = updatedJobApplication.HiringTeam != jobApplication.HiringTeam ? updatedJobApplication.HiringTeam : jobApplication.HiringTeam;
        jobApplication.TechStack = updatedJobApplication.TechStack != jobApplication.TechStack ? updatedJobApplication.TechStack : jobApplication.TechStack;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!JobApplicationExists(id))
        {

            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteJobApplication(int id)
    {
        var jobApplication = await _context.JobApplications.FindAsync(id);

        if (jobApplication is null) return NotFound();

        _context.JobApplications.Remove(jobApplication);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool JobApplicationExists(int id)
    {
        return _context.JobApplications.Any(jobApplication => id == jobApplication.Id);
    }
}