using System.Reflection.Emit;
using Finbuckle.MultiTenant;
using FSH.Starter.WebApi.Attendance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Starter.WebApi.Todo.Persistence.Configurations;
internal sealed class LeaveItemConfiguration : IEntityTypeConfiguration<Leave>
{
    public void Configure(EntityTypeBuilder<Leave> builder)
    {
        builder.IsMultiTenant();
        builder.HasKey(x => x.Id);

        builder.HasOne(l => l.User)
        .WithMany()
        .HasForeignKey(l => l.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Remarks).HasMaxLength(250);
    }
}
