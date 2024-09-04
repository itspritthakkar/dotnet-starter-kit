using Asp.Versioning;
using FSH.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Attendance.Features.Create.v1;
public static class CreateLeaveEndpoint
{
    internal static RouteHandlerBuilder MapLeaveItemCreationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/", async (CreateLeaveCommand request, ISender mediator) =>
                {
                    var response = await mediator.Send(request);
                    return Results.CreatedAtRoute(nameof(CreateLeaveEndpoint), new { id = response.Id }, response);
                })
                .WithName(nameof(CreateLeaveEndpoint))
                .WithSummary("Creates a leave item")
                .WithDescription("Creates a leave item")
                .Produces<CreateLeaveResponse>(StatusCodes.Status201Created)
                .RequirePermission("Permissions.Todos.Create")
                .MapToApiVersion(new ApiVersion(1, 0));

    }
}
