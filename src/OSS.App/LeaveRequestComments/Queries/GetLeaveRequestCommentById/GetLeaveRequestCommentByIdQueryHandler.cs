using MediatR;
using OSS.App.Data;
using OSS.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.LeaveRequestComments.Queries.GetLeaveRequestCommentById
{
    public class GetLeaveRequestCommentByIdQueryHandler : IRequestHandler<GetLeaveRequestCommentByIdQuery, LeaveRequestComment>
    {
        private readonly AppIdentityDbContext _context;

        public GetLeaveRequestCommentByIdQueryHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<LeaveRequestComment> Handle(GetLeaveRequestCommentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.LeaveRequestComments
                                            .FindAsync(request.Id);
        }
    }
}
