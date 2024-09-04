
using FSH.Framework.Core.Caching;
using FSH.Framework.Core.Domain.Events;
using FSH.Starter.WebApi.Attendance.Features.Get.v1;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Attendance.Domain.Events;
public record LeaveItemCreated(Guid Id, DateTime FromDate, DateTime ToDate, string Remarks) : DomainEvent;

public class LeaveItemCreatedEventHandler(
    ILogger<LeaveItemCreatedEventHandler> logger,
    ICacheService cache)
    : INotificationHandler<LeaveItemCreated>
{
    public async Task Handle(LeaveItemCreated notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("handling leave item created domain event..");
        var cacheResponse = new GetLeaveResponse(notification.Id, notification.FromDate, notification.ToDate, notification.Remarks);
        await cache.SetAsync($"todo:{notification.Id}", cacheResponse, cancellationToken: cancellationToken);
    }
}
