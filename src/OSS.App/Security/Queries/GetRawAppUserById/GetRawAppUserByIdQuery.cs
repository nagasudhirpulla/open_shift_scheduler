using MediatR;
using Microsoft.AspNetCore.Identity;
using OSS.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.App.Security.Queries.GetRawRawAppUserById
{
    public class GetRawAppUserByIdQuery : IRequest<ApplicationUser>
    {
        public string Id { get; set; }
        public class GetRawAppUserByIdQueryHandler : IRequestHandler<GetRawAppUserByIdQuery, ApplicationUser>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public GetRawAppUserByIdQueryHandler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<ApplicationUser> Handle(GetRawAppUserByIdQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }
                ApplicationUser user = await _userManager.FindByIdAsync(request.Id);

                return user;
            }
        }
    }
}
