
using FSH.Framework.Core.Caching;
using FSH.Framework.Core.Domain.Events;
using FSH.Starter.WebApi.Attendance.Features.Get.v1;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Attendance.Domain.Events;
public record LeaveItemUpdated(Leave item) : DomainEvent;

public class LeaveItemUpdatedEventHandler(
    ILogger<LeaveItemUpdatedEventHandler> logger,
    ICacheService cache)
    : INotificationHandler<LeaveItemUpdated>
{
    public async Task Handle(LeaveItemUpdated notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("handling leave item update domain event..");
        var cacheResponse = new GetLeaveResponse(notification.item.Id, notification.item.FromDate, notification.item.ToDate, notification.item.Remarks);
        await cache.SetAsync($"todo:{notification.item.Id}", cacheResponse, cancellationToken: cancellationToken);
    }
}
