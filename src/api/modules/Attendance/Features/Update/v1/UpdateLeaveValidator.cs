using FluentValidation;
using FSH.Starter.WebApi.Attendance.Persistence;

namespace FSH.Starter.WebApi.Attendance.Features.Update.v1;
public class UpdateLeaveValidator : AbstractValidator<UpdateLeaveCommand>
{
    public UpdateLeaveValidator(AttendanceDbContext context)
    {
        RuleFor(p => p.FromDate).NotEmpty();
        RuleFor(p => p.ToDate).NotEmpty();
        RuleFor(p => p.Remarks).NotEmpty();
    }
}
