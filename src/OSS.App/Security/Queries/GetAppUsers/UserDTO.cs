using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OSS.App.Mappings;
using OSS.Domain.Entities;

namespace OSS.App.Security.Queries.GetAppUsers
{
    public class UserDTO : IMapFrom<ApplicationUser>
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, UserDTO>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Username, opt => opt.MapFrom(s => s.UserName));
        }
    }

}
