using MediatR;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSS.App.LeaveRequestComments.Queries.GetLeaveRequestCommentById
{
    public class GetLeaveRequestCommentByIdQuery : IRequest<LeaveRequestComment>
    {
        public int Id { get; set; }
    }
}
