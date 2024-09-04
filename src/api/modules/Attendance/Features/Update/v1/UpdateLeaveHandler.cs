using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Attendance.Domain;
using FSH.Starter.WebApi.Attendance.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Attendance.Features.Update.v1;
public sealed class UpdateLeaveHandler(
    ILogger<UpdateLeaveHandler> logger,
    [FromKeyedServices("leaves")] IRepository<Leave> repository)
    : IRequestHandler<UpdateLeaveCommand, UpdateLeaveResponse>
{
    public async Task<UpdateLeaveResponse> Handle(UpdateLeaveCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var todo = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = todo ?? throw new LeaveItemNotFoundException(request.Id);

        string format = "dd-MM-yyyy";

        DateTime? fromDate = DateTime.ParseExact(request.FromDate!, format, System.Globalization.CultureInfo.InvariantCulture);
        DateTime? toDate = DateTime.ParseExact(request.ToDate!, format, System.Globalization.CultureInfo.InvariantCulture);

        var updatedTodo = todo.Update(fromDate, toDate, request.Remarks);
        await repository.UpdateAsync(updatedTodo, cancellationToken);
        logger.LogInformation("leave item updated {LeaveId}", updatedTodo.Id);
        return new UpdateLeaveResponse(updatedTodo.Id);
    }
}
