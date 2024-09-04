using System.ComponentModel;
using MediatR;

namespace FSH.Starter.WebApi.Attendance.Features.Update.v1;
public sealed record UpdateLeaveCommand(
    Guid Id,
    string? FromDate,
    string? ToDate,
    string? Remarks = null): IRequest<UpdateLeaveResponse>;



