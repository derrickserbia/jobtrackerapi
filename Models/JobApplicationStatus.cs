using System.ComponentModel;

namespace JobTrackerApi.Models;

public enum ApplicationStatus
{
    [Description("Application is pending review.")]
    Pending,

    [Description("Interview has been scheduled.")]
    InterviewScheduled,

    [Description("Application was rejected by the employer.")]
    RejectedByEmployer,

    [Description("Application was withdrawn by me.")]
    Withdrawn,

    [Description("Job offer has been received.")]
    OfferReceived,

    [Description("Job offer has been accepted.")]
    OfferAccepted
}