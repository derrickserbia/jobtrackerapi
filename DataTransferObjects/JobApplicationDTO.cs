namespace JobTrackerApi.DataTransferObjects;
public class JobApplicationDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
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
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
