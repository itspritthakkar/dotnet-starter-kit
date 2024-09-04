using System.ComponentModel;

namespace FSH.Starter.WebApi.Attendance.Domain;
public enum LeaveStatus
{
    [Description("Pending")]
    Pending = 0,
    [Description("Approved")]
    Approved = 1,
    [Description("Rejected")]
    Rejected = 3
}
