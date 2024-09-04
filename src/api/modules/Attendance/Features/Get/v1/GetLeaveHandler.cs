using FSH.Framework.Core.Caching;
using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Attendance.Domain;
using FSH.Starter.WebApi.Attendance.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.Starter.WebApi.Attendance.Features.Get.v1;
public sealed class GetLeaveHandler(
    [FromKeyedServices("leaves")] IReadRepository<Leave> repository,
    ICacheService cache)
    : IRequestHandler<GetLeaveRequest, GetLeaveResponse>
{
    public async Task<GetLeaveResponse> Handle(GetLeaveRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"leave:{request.Id}",
            async () =>
            {
                var leaveItem = await repository.GetByIdAsync(request.Id, cancellationToken);
                if (leaveItem == null) throw new LeaveItemNotFoundException(request.Id);
                return new GetLeaveResponse(leaveItem.Id, leaveItem.FromDate, leaveItem.ToDate, leaveItem.Remarks!);
            },
            cancellationToken: cancellationToken);
        return item!;
    }
}
