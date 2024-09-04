using FSH.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Attendance.Features.Get.v1;
public static class GetLeaveEndpoint
{
    internal static RouteHandlerBuilder MapGetLeaveEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/{id:guid}", async (Guid id, ISender mediator) =>
                        {
                            var response = await mediator.Send(new GetLeaveRequest(id));
                            return Results.Ok(response);
                        })
                        .WithName(nameof(GetLeaveEndpoint))
                        .WithSummary("gets leave item by id")
                        .WithDescription("gets leave item by id")
                        .Produces<GetLeaveResponse>()
                        .RequirePermission("Permissions.Todos.View")
                        .MapToApiVersion(1);
    }
}
