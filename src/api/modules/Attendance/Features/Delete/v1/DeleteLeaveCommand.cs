using System.ComponentModel;
using MediatR;

namespace FSH.Starter.WebApi.Attendance.Features.Delete.v1;
public sealed record DeleteLeaveCommand(
    Guid Id) : IRequest;



