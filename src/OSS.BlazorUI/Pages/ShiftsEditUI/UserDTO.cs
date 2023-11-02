namespace OSS.BlazorUI.Pages.ShiftsEditUI;

public class UserDTO
{
    // TODO get the DTO from App layer
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
}

