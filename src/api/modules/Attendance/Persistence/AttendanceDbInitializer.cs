using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Attendance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.WebApi.Attendance.Persistence;
internal sealed class AttendanceDbInitializer(
    ILogger<AttendanceDbInitializer> logger,
    AttendanceDbContext context) : IDbInitializer
{
    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        if ((await context.Database.GetPendingMigrationsAsync(cancellationToken).ConfigureAwait(false)).Any())
        {
            await context.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
            logger.LogInformation("[{Tenant}] applied database migrations for leave module", context.TenantInfo!.Identifier);
        }
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        string format = "dd-MM-yyyy";

        const string fromDateString = "03-09-2024";
        const string toDateString = "03-09-2024";

        DateTime? fromDate = DateTime.ParseExact(fromDateString, format, System.Globalization.CultureInfo.InvariantCulture);
        DateTime? toDate = DateTime.ParseExact(toDateString, format, System.Globalization.CultureInfo.InvariantCulture);

        const string remarks = "Leave remark";
        if (await context.Leaves.FirstOrDefaultAsync(t => t.FromDate.Date == fromDate.Value.Date && t.ToDate.Date == toDate.Value.Date, cancellationToken).ConfigureAwait(false) is null)
        {
            var leave = Leave.Create(fromDateString, toDateString, remarks);
            await context.Leaves.AddAsync(leave, cancellationToken);
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            logger.LogInformation("[{Tenant}] seeding default leave data", context.TenantInfo!.Identifier);
        }
    }
}
