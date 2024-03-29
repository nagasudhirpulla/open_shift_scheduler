﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OSS.Domain.Entities;
using AutoMapper;

namespace OSS.App.Security.Queries.GetAppUsers;

public class GetAppUsersListQuery : IRequest<UserListVM>
{
    public class GetAppUsersListQueryHandler : IRequestHandler<GetAppUsersListQuery, UserListVM>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityInit _identityInit;
        private readonly IMapper _mapper;

        public GetAppUsersListQueryHandler(UserManager<ApplicationUser> userManager, IdentityInit identityInit, IMapper mapper)
        {
            _userManager = userManager;
            _identityInit = identityInit;
            _mapper = mapper;
        }

        public async Task<UserListVM> Handle(GetAppUsersListQuery request, CancellationToken cancellationToken)
        {
            UserListVM vm = new UserListVM();
            vm.Users = new List<UserDTO>();
            // get the list of users
            List<ApplicationUser> users = await _userManager.Users.OrderBy(u => u.UserName).Include(u => u.ShiftGroup).Include(u => u.ShiftRole).Include(u => u.Gender).ToListAsync(cancellationToken: cancellationToken);
            foreach (ApplicationUser user in users)
            {
                // get user is of admin role
                //bool isSuperAdmin = (await _userManager.GetRolesAsync(user)).Any(r => r == SecurityConstants.AdminRoleString);
                // todo make identity init  singleton service like email config so as to avoid raw strings usage
                bool isSuperAdmin = (user.UserName == _identityInit.AdminUserName);
                if (!isSuperAdmin)
                {
                    // add user to vm only if not admin
                    string userRole = "";
                    IList<string> existingRoles = await _userManager.GetRolesAsync(user);
                    if (existingRoles.Count > 0)
                    {
                        userRole = existingRoles.ElementAt(0);
                    }
                    UserDTO uDTO = _mapper.Map<UserDTO>(user);
                    uDTO.UserRole = userRole;
                    vm.Users.Add(uDTO);
                }

            }
            return vm;
        }
    }
}
