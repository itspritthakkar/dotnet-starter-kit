using FSH.Framework.Core.Exceptions;

namespace FSH.Starter.WebApi.Attendance.Exceptions;
internal sealed class LeaveItemNotFoundException : NotFoundException
{
    public LeaveItemNotFoundException(Guid id)
        : base($"leave item with id {id} not found")
    {
    }
}
