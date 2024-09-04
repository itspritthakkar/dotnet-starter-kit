using Asp.Versioning;
using FSH.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Attendance.Features.Delete.v1;
public static class DeleteLeaveEndpoint
{
    internal static RouteHandlerBuilder MapLeaveItemDeletionEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete("/{id:guid}", async (Guid id, ISender mediator) =>
            {
                await mediator.Send(new DeleteLeaveCommand(id));
                return Results.NoContent();
            })
            .WithName(nameof(DeleteLeaveEndpoint))
            .WithSummary("Deletes a leave item")
            .WithDescription("Deleted a leave item")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.Todos.Delete")
            .MapToApiVersion(new ApiVersion(1, 0));

    }
}
