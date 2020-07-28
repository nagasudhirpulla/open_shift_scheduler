using FluentValidation;

namespace OSS.App.Security.Commands.CreateAppUser
{
    public class CreateAppUserCommandValidator : AbstractValidator<CreateAppUserCommand>
    {
        public CreateAppUserCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.DisplayName).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.UserRole).NotEmpty();
            RuleFor(x => x.Password)
                .NotEmpty()
                .Equal(x => x.ConfirmPassword).WithMessage("Password and confirmation password do not match.");
        }
    }
}
