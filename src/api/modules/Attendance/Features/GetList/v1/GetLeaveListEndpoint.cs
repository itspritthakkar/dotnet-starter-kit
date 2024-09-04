using FSH.Framework.Core.Paging;
using FSH.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Attendance.Features.GetList.v1;

public static class GetLeaveListEndpoint
{
    internal static RouteHandlerBuilder MapGetLeaveListEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/search", async (ISender mediator, [FromBody] PaginationFilter filter) =>
        {
            var response = await mediator.Send(new GetLeaveListRequest(filter));
            return Results.Ok(response);
        })
        .WithName(nameof(GetLeaveListEndpoint))
        .WithSummary("Gets a list of leaves with paging support")
        .WithDescription("Gets a list of leaves with paging support")
        .Produces<PagedList<LeaveDto>>()
        .RequirePermission("Permissions.Todos.View")
        .MapToApiVersion(1);
    }
}
