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
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public string OfficeId { get; set; }
        public string Designation { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public bool IsActive { get; set; } = true;
        public string ShiftRole { get; set; }
        public string ShiftGroup { get; set; }
        public string PhoneNumber { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, UserDTO>()
                .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Gender.Name))
                .ForMember(d => d.ShiftRole, opt => opt.MapFrom(s => s.ShiftRole.RoleName))
                .ForMember(d => d.ShiftGroup, opt => opt.MapFrom(s => s.ShiftGroup.Name))
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Username, opt => opt.MapFrom(s => s.UserName));
        }
    }

}
