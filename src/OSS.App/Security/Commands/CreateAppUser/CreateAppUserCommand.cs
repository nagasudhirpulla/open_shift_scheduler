using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OSS.App.Mappings;
using OSS.Domain.Entities;

namespace OSS.App.Security.Commands.CreateAppUser
{
    public class CreateAppUserCommand : IRequest<IdentityResult>
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserRole { get; set; } = SecurityConstants.GuestRoleString;
        public string OfficeId { get; set; }
        public string Designation { get; set; }
        public int GenderId { get; set; }
        public bool IsActive { get; set; } = true;
        public int ShiftRoleId { get; set; }
        public int ShiftGroupId { get; set; }
        public string BaseUrl { get; set; }
    }
}
