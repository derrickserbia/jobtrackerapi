using System.ComponentModel.DataAnnotations;

namespace JobTrackerApi.DataTransferObjects;
public class CreateJobApplicationDTO
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public string JobTitle { get; set; }

    [Required]
    public string CompanyName { get; set; }

    public string HiringTeam { get; set; } = string.Empty;
    public string JobPostingUrl { get; set; } = string.Empty;
    public string JobDescription { get; set; } = string.Empty;
    public decimal MinSalary { get; set; } = 0;
    public decimal MaxSalary { get; set; } = 0;
    public string Notes { get; set; } = string.Empty;
    public DateTime AppliedDate { get; set; } = DateTime.UtcNow;

    public bool IsValid()
    {
        return MinSalary >= 0 && MaxSalary >= 0 && (MinSalary == MaxSalary || MinSalary < MaxSalary);
    }
}
