using AutoMapper;
using MediatR;
using OSS.App.Mappings;
using OSS.App.Security.Queries.GetAppUsers;
using OSS.Domain.Entities;

namespace OSS.App.Security.Commands.EditAppUser;

public class EditAppUserCommand : IRequest<List<string>>, IMapFrom<UserDTO>
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string UserRole { get; set; }
    public string OfficeId { get; set; }
    public string Designation { get; set; }
    public int GenderId { get; set; }
    public bool IsActive { get; set; }
    public int ShiftRoleId { get; set; }
    public int ShiftGroupId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ApplicationUser, EditAppUserCommand>()
            .ForMember(d => d.Username, opt => opt.MapFrom(s => s.UserName));
    }
}
