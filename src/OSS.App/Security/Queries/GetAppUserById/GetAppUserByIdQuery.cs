using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OSS.App.Security.Queries.GetAppUsers;
using OSS.Domain.Entities;

namespace OSS.App.Security.Queries.GetAppUserById
{
    public class GetAppUserByIdQuery : IRequest<UserDTO>
    {
        public string Id { get; set; }
        public class GetAppUserByIdQueryHandler : IRequestHandler<GetAppUserByIdQuery, UserDTO>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;

            public GetAppUserByIdQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<UserDTO> Handle(GetAppUserByIdQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                ApplicationUser user = await _userManager.FindByIdAsync(request.Id);
                if (user == null)
                {
                    return null;
                }

                IList<string> existingUserRoles = (await _userManager.GetRolesAsync(user));
                string userRole = SecurityConstants.GuestRoleString;
                if (existingUserRoles.Count > 0)
                {
                    userRole = existingUserRoles.ElementAt(0);
                }
                //UserDTO vm = new UserDTO()
                //{
                //    Email = user.Email,
                //    Username = user.UserName,
                //    UserRole = userRole,
                //    UserId = user.Id
                //};
                UserDTO vm = _mapper.Map<UserDTO>(user);
                vm.UserRole = userRole;
                return vm;
            }
        }
    }
}
