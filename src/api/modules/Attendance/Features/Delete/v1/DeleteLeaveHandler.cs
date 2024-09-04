using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Attendance.Domain;
using FSH.Starter.WebApi.Attendance.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Attendance.Features.Delete.v1;
public sealed class DeleteLeaveHandler(
    ILogger<DeleteLeaveHandler> logger,
    [FromKeyedServices("leaves")] IRepository<Leave> repository)
    : IRequestHandler<DeleteLeaveCommand>
{
    public async Task Handle(DeleteLeaveCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var leaveItem = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = leaveItem ?? throw new LeaveItemNotFoundException(request.Id);
        await repository.DeleteAsync(leaveItem, cancellationToken);
        logger.LogInformation("todo with id : {TodoId} deleted", leaveItem.Id);
    }
}
