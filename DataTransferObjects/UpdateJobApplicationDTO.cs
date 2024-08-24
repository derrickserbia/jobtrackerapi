using System.ComponentModel.DataAnnotations;
using JobTrackerApi.Models;

namespace JobTrackerApi.DataTransferObjects;
public class UpdateJobApplicationDTO
{
    [Required]
    public int Id { get; set; }
    public string JobTitle { get; set; }
    public string CompanyName { get; set; }
    public string HiringTeam { get; set; }
    public string JobPostingUrl { get; set; }
    public string JobDescription { get; set; }
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }
    public int JobApplicationStatusId { get; set; }
    public string Notes { get; set; }
    public DateTime AppliedDate { get; set; }

    public bool IsValid()
    {
        return
            MinSalary >= 0 && MaxSalary >= 0 && (MinSalary == MaxSalary || MinSalary < MaxSalary) &&
            Enum.IsDefined(typeof(JobApplicationStatus), JobApplicationStatusId);
    }
}
