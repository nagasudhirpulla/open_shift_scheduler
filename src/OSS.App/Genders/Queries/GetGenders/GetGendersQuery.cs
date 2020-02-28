using MediatR;
using Microsoft.EntityFrameworkCore;
using OSS.App.Data;
using OSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.Genders.Queries.GetGenders
{
    public class GetGendersQuery : IRequest<List<Gender>>
    {
        public class GetGendersQueryHandler : IRequestHandler<GetGendersQuery, List<Gender>>
        {
            private readonly AppIdentityDbContext _context;

            public GetGendersQueryHandler(AppIdentityDbContext context)
            {
                _context = context;
            }

            public async Task<List<Gender>> Handle(GetGendersQuery request, CancellationToken cancellationToken)
            {
                List<Gender> res = await _context.Genders.ToListAsync();
                return res;
            }
        }
    }
}
