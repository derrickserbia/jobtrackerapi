using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobTrackerApi.Models;
public enum JobApplicationStatus
{
    Applied,
    Interviewing,
    Rejected,
    Withdrawn,
    Offered,
    Accepted
}