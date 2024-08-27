using JobTrackerApi.Models;

namespace JobTrackerApi.DataTransferObjects;
public class JobApplicationsDTO
{
    public int Id { get; set; }
    public string JobTitle { get; set; }
    public string CompanyName { get; set; }
    public string HiringTeam { get; set; }
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }
    public int JobApplicationStatusId { get; set; }
    public DateTime AppliedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
