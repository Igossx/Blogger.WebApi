using FluentValidation;

namespace Blogger.Application.Account.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Please enter a email.")
                .EmailAddress().WithMessage("Please enter a correct form of email.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Please enter a pasword.")
                .MinimumLength(6).WithMessage("Password should have atleast 6 characters");
        }
    }
}
