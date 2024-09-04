using FSH.Framework.Core.Paging;
using MediatR;

namespace FSH.Starter.WebApi.Attendance.Features.GetList.v1;
public record GetLeaveListRequest(PaginationFilter filter) : IRequest<PagedList<LeaveDto>>;
