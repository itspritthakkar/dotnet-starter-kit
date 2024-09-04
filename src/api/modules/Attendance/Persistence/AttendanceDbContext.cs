using Finbuckle.MultiTenant.Abstractions;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Infrastructure.Identity.Users;
using FSH.Framework.Infrastructure.Persistence;
using FSH.Framework.Infrastructure.Tenant;
using FSH.Starter.WebApi.Attendance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using FSH.Framework.Infrastructure.Identity;

namespace FSH.Starter.WebApi.Attendance.Persistence;
public sealed class AttendanceDbContext : FshDbContext
{
    public AttendanceDbContext(IMultiTenantContextAccessor<FshTenantInfo> multiTenantContextAccessor, DbContextOptions<AttendanceDbContext> options, IPublisher publisher, IOptions<DatabaseOptions> settings)
        : base(multiTenantContextAccessor, options, publisher, settings)
    {
    }

    public DbSet<Leave> Leaves { get; set; }
    public DbSet<LeaveCategory> LeaveCategory { get; set; }
    public DbSet<LeaveType> LeaveType { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.Entity<FshUser>().ToTable("Users", IdentityConstants.SchemaName).Metadata.SetIsTableExcludedFromMigrations(true);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AttendanceDbContext).Assembly);
        modelBuilder.HasDefaultSchema(SchemaNames.Attendance);
        modelBuilder.Entity<Leave>().Property(e => e.Status).HasConversion<int>();

        modelBuilder.Entity<LeaveCategory>()
        .HasMany(lc => lc.Leaves)
        .WithOne(l => l.LeaveCategory)
        .HasForeignKey(l => l.LeaveCategoryId)
        .IsRequired();

        modelBuilder.Entity<LeaveType>()
        .HasMany(lc => lc.Leaves)
        .WithOne(l => l.LeaveType)
        .HasForeignKey(l => l.LeaveTypeId)
        .IsRequired();
    }
}
