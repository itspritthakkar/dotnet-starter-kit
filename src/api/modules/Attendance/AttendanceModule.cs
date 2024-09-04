using Carter;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Infrastructure.Persistence;
using FSH.Starter.WebApi.Attendance.Domain;
using FSH.Starter.WebApi.Attendance.Features.Create.v1;
using FSH.Starter.WebApi.Attendance.Features.Delete.v1;
using FSH.Starter.WebApi.Attendance.Features.Get.v1;
using FSH.Starter.WebApi.Attendance.Features.GetList.v1;
using FSH.Starter.WebApi.Attendance.Features.Update.v1;
using FSH.Starter.WebApi.Attendance.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.Starter.WebApi.Attendance;
public static class AttendanceModule
{
    public class Endpoints : CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            var leaveGroup = app.MapGroup("leaves").WithTags("leaves");
            leaveGroup.MapLeaveItemCreationEndpoint();
            leaveGroup.MapGetLeaveEndpoint();
            leaveGroup.MapGetLeaveListEndpoint();
            leaveGroup.MapLeaveItemUpdationEndpoint();
            leaveGroup.MapLeaveItemDeletionEndpoint();
        }
    }
    public static WebApplicationBuilder RegisterAttendanceServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.BindDbContext<AttendanceDbContext>();
        builder.Services.AddScoped<IDbInitializer, AttendanceDbInitializer>();
        builder.Services.AddKeyedScoped<IRepository<Leave>, AttendanceRepository<Leave>>("leaves");
        builder.Services.AddKeyedScoped<IReadRepository<Leave>, AttendanceRepository<Leave>>("leaves");
        return builder;
    }

    public static WebApplication UseAttendanceModule(this WebApplication app)
    {
        return app;
    }
}
