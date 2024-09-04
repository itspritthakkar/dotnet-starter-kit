using System.ComponentModel;
using MediatR;

namespace FSH.Starter.WebApi.Attendance.Features.Create.v1;
public record CreateLeaveCommand(
    [property: DefaultValue("02-09-2024")] string FromDate,
    [property: DefaultValue("02-09-2024")] string ToDate,
    [property: DefaultValue("Important Remarks.")] string Remarks) : IRequest<CreateLeaveResponse>;



