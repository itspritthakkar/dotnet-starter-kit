using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Attendance.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Attendance.Features.Create.v1;
public sealed class CreateLeaveHandler(
    ILogger<CreateLeaveHandler> logger,
    [FromKeyedServices("leaves")] IRepository<Leave> repository)
    : IRequestHandler<CreateLeaveCommand, CreateLeaveResponse>
{
    public async Task<CreateLeaveResponse> Handle(CreateLeaveCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = Leave.Create(request.FromDate, request.ToDate, request.Remarks);
        await repository.AddAsync(item, cancellationToken).ConfigureAwait(false);
        await repository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        logger.LogInformation("leave item created {LeaveId}", item.Id);
        return new CreateLeaveResponse(item.Id);
    }
}
