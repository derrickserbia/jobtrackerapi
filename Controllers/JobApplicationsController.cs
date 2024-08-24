using JobTrackerApi.Models;
using JobTrackerApi.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobTrackerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobApplicationsController : ControllerBase
{
    private readonly JobApplicationContext _context;

    public JobApplicationsController(JobApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    // public async Task<ActionResult<IEnumerable<JobApplicationDTO>>> GetJobApplications(string? searchString, int? jobApplicationStatusId)
    public async Task<ActionResult<IEnumerable<JobApplicationDTO>>> GetJobApplications([FromQuery] JobApplicationQueryParameters queryParameters)
    {
        var jobApplications = _context.JobApplications.AsQueryable();

        if (queryParameters.JobApplicationStatusId.HasValue)
        {
            if (!Enum.IsDefined(typeof(JobApplicationStatus), queryParameters.JobApplicationStatusId))
            {
                return BadRequest("Invalid jobApplicationStatusId.");
            }
            jobApplications = jobApplications.Where(j => j.JobApplicationStatusId == queryParameters.JobApplicationStatusId);
        }

        if (!string.IsNullOrEmpty(queryParameters.SearchString))
        {
            var searchString = queryParameters.SearchString.Trim().ToLower();
            var searchWords = searchString.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (searchWords != null && searchWords.Length != 0)
            {
                jobApplications = jobApplications.Where(j =>
                    searchWords.Any(word =>
                        j.JobTitle.ToLower().Contains(word)
                        || j.CompanyName.ToLower().Contains(word)
                        || j.HiringTeam.ToLower().Contains(word)
                        || j.JobDescription.ToLower().Contains(word)
                    ));
            }
        }

        var jobApplicationDtos = await jobApplications.Select(j => new JobApplicationDTO
        {
            Id = j.Id,
            UserId = j.UserId,
            JobTitle = j.JobTitle,
            CompanyName = j.CompanyName,
            HiringTeam = j.HiringTeam,
            JobPostingUrl = j.JobPostingUrl,
            JobDescription = j.JobDescription,
            MinSalary = j.MinSalary,
            MaxSalary = j.MaxSalary,
            JobApplicationStatusId = j.JobApplicationStatusId,
            Notes = j.Notes,
            AppliedDate = j.AppliedDate,
            CreatedDate = j.CreatedDate,
            UpdatedDate = j.UpdatedDate
        })
        .Skip(queryParameters.Size * (queryParameters.Page - 1))
        .Take(queryParameters.Size)
        .ToListAsync();

        return Ok(jobApplicationDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<JobApplicationDTO>> GetJobApplication(int id)
    {
        var jobApplication = await _context.JobApplications.FindAsync(id);
        if (jobApplication == null) return NotFound();

        var jobApplicationDto = new JobApplicationDTO
        {
            Id = jobApplication.Id,
            UserId = jobApplication.UserId,
            JobTitle = jobApplication.JobTitle,
            CompanyName = jobApplication.CompanyName,
            HiringTeam = jobApplication.HiringTeam,
            JobPostingUrl = jobApplication.JobPostingUrl,
            JobDescription = jobApplication.JobDescription,
            MinSalary = jobApplication.MinSalary,
            MaxSalary = jobApplication.MaxSalary,
            JobApplicationStatusId = jobApplication.JobApplicationStatusId,
            Notes = jobApplication.Notes,
            AppliedDate = jobApplication.AppliedDate,
            CreatedDate = jobApplication.CreatedDate,
            UpdatedDate = jobApplication.UpdatedDate
        };

        return Ok(jobApplicationDto);
    }

    [HttpPost]
    public async Task<ActionResult> PostJobApplication(CreateJobApplicationDTO jobApplicationDto)
    {
        if (!jobApplicationDto.IsValid()) return BadRequest();

        var newJobApplication = new JobApplication()
        {
            JobTitle = jobApplicationDto.JobTitle,
            CompanyName = jobApplicationDto.CompanyName,
            HiringTeam = jobApplicationDto.HiringTeam,
            JobPostingUrl = jobApplicationDto.JobPostingUrl,
            JobDescription = jobApplicationDto.JobDescription,
            MinSalary = jobApplicationDto.MinSalary,
            MaxSalary = jobApplicationDto.MaxSalary,
            Notes = jobApplicationDto.Notes,
            AppliedDate = jobApplicationDto.AppliedDate
        };

        _context.Add(newJobApplication);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetJobApplication), new { id = newJobApplication.Id }, newJobApplication);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteJobApplication(int id)
    {
        var jobApplication = await _context.JobApplications.FindAsync(id);

        if (jobApplication == null) return NotFound();

        _context.JobApplications.Remove(jobApplication);
        await _context.SaveChangesAsync();

        return Ok(jobApplication);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutJobApplication(int id, UpdateJobApplicationDTO updateJobApplicationDto)
    {
        if (id != updateJobApplicationDto.Id || !updateJobApplicationDto.IsValid()) return BadRequest();

        var jobApplication = await _context.JobApplications.FindAsync(id);
        if (jobApplication == null) return NotFound();

        jobApplication.Id = updateJobApplicationDto.Id;
        jobApplication.Id = updateJobApplicationDto.Id;
        jobApplication.JobTitle = updateJobApplicationDto.JobTitle;
        jobApplication.CompanyName = updateJobApplicationDto.CompanyName;
        jobApplication.HiringTeam = updateJobApplicationDto.HiringTeam;
        jobApplication.JobPostingUrl = updateJobApplicationDto.JobPostingUrl;
        jobApplication.JobDescription = updateJobApplicationDto.JobDescription;
        jobApplication.MinSalary = updateJobApplicationDto.MinSalary;
        jobApplication.MaxSalary = updateJobApplicationDto.MaxSalary;
        jobApplication.JobApplicationStatusId = updateJobApplicationDto.JobApplicationStatusId;
        jobApplication.Notes = updateJobApplicationDto.Notes;
        jobApplication.AppliedDate = updateJobApplicationDto.AppliedDate;
        jobApplication.UpdatedDate = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.JobApplications.Any(j => j.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }

        }

        return NoContent();
    }

}