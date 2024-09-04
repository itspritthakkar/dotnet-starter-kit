using Asp.Versioning;
using FSH.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Attendance.Features.Update.v1;
public static class UpdateLeaveEndpoint
{
    internal static RouteHandlerBuilder MapLeaveItemUpdationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.
            MapPut("/{id:guid}", async (Guid id, UpdateLeaveCommand request, ISender mediator) =>
            {
                if (id != request.Id) return Results.BadRequest();
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(UpdateLeaveEndpoint))
            .WithSummary("Updates a leave item")
            .WithDescription("Updated a leave item")
            .Produces<UpdateLeaveResponse>(StatusCodes.Status200OK)
            .RequirePermission("Permissions.Todos.Update")
            .MapToApiVersion(new ApiVersion(1, 0));

    }
}
