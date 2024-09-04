namespace FSH.Starter.WebApi.Attendance.Features.Get.v1;
public record GetLeaveResponse(Guid? Id, DateTime FromDate, DateTime ToDate, string? Remarks);
