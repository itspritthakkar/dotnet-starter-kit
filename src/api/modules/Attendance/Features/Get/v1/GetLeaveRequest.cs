using MediatR;

namespace FSH.Starter.WebApi.Attendance.Features.Get.v1;
public class GetLeaveRequest : IRequest<GetLeaveResponse>
{
    public Guid Id { get; set; }
    public GetLeaveRequest(Guid id) => Id = id;
}
