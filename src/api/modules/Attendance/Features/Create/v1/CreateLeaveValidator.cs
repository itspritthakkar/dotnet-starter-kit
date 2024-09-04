using FluentValidation;
using FSH.Starter.WebApi.Attendance.Persistence;

namespace FSH.Starter.WebApi.Attendance.Features.Create.v1;
public class CreateLeaveValidator : AbstractValidator<CreateLeaveCommand>
{
    public CreateLeaveValidator(AttendanceDbContext context)
    {
        RuleFor(p => p.FromDate).NotEmpty();
        RuleFor(p => p.ToDate).NotEmpty();
        RuleFor(p => p.Remarks).NotEmpty();
    }
}
