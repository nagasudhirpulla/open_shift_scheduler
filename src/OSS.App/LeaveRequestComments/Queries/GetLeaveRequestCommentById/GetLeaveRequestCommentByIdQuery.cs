using MediatR;
using OSS.Domain.Entities;

namespace OSS.App.LeaveRequestComments.Queries.GetLeaveRequestCommentById;
public class GetLeaveRequestCommentByIdQuery : IRequest<LeaveRequestComment>
{
    public int Id { get; set; }
}