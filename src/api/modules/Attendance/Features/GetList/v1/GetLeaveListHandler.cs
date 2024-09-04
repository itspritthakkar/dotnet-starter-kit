using FSH.Framework.Core.Paging;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Core.Specifications;
using FSH.Starter.WebApi.Attendance.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.Starter.WebApi.Attendance.Features.GetList.v1;

public sealed class GetLeaveListHandler(
    [FromKeyedServices("leaves")] IReadRepository<Leave> repository)
    : IRequestHandler<GetLeaveListRequest, PagedList<LeaveDto>>
{
    public async Task<PagedList<LeaveDto>> Handle(GetLeaveListRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new EntitiesByPaginationFilterSpec<Leave, LeaveDto>(request.filter);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedList<LeaveDto>(items, request.filter.PageNumber, request.filter.PageSize, totalCount);
    }
}
