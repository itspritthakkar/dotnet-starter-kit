using System.ComponentModel;
using System.Reflection.Metadata;
using FSH.Framework.Core.Domain;
using FSH.Framework.Core.Domain.Contracts;
using FSH.Framework.Core.Identity.Users.Dtos;
using FSH.Framework.Infrastructure.Identity.Users;
using FSH.Starter.WebApi.Attendance.Domain.Events;

namespace FSH.Starter.WebApi.Attendance.Domain;

public class Leave : AuditableEntity, IAggregateRoot
{
    public DateTime FromDate { get; set; } = DateTime.Now;

    public DateTime ToDate { get; set; } = DateTime.Now;

    public string? Remarks { get; set; }

    public Guid LeaveCategoryId { get; set; }
    public LeaveCategory? LeaveCategory { get; set; }

    public Guid LeaveTypeId { get; set; }
    public LeaveType? LeaveType { get; set; }

    public LeaveStatus Status { get; set; }

    public string UserId { get; set; } = Guid.NewGuid().ToString(); // Required foreign key property
    public FshUser? User { get; set; } // Required reference navigation to principal

    public static Leave Create(string fromDate, string toDate, string remarks)
    {
        var item = new Leave();

        string format = "dd-MM-yyyy";

        item.FromDate = DateTime.ParseExact(fromDate, format, System.Globalization.CultureInfo.InvariantCulture);
        item.ToDate = DateTime.ParseExact(toDate, format, System.Globalization.CultureInfo.InvariantCulture);
        item.Remarks = remarks;

        item.QueueDomainEvent(new LeaveItemCreated(item.Id, item.FromDate, item.ToDate, item.Remarks));

        LeaveMetrics.Created.Add(1);

        return item;
    }

    public Leave Update(DateTime? fromDate, DateTime? toDate, string? remarks)
    {
        if (fromDate is not null && FromDate.Equals(fromDate) is not true) FromDate = fromDate ?? FromDate;
        if (toDate is not null && ToDate.Equals(toDate) is not true) ToDate = toDate ?? ToDate;
        if (remarks is not null && Remarks?.Equals(remarks, StringComparison.OrdinalIgnoreCase) is not true) Remarks = remarks;

        this.QueueDomainEvent(new LeaveItemUpdated(this));

        return this;

    }
}
