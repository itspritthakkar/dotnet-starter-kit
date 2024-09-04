namespace FSH.Starter.WebApi.Attendance.Features.GetList.v1;
public record LeaveDto(Guid? Id, DateTime FromDate, DateTime ToDate, string Remarks);
