using Domain.Interfaces;
using FluentValidation;

namespace Blogger.Application.Account.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public RegisterUserCommandValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Please enter a email.")
                .EmailAddress().WithMessage("Please enter a correct form of email.")
                .Must(_accountRepository.IsEmailUnique).WithMessage("This email already exists.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Please enter a pasword.")
                .MinimumLength(6).WithMessage("Password should have atleast 6 characters");

            RuleFor(u => u.ConfirmPassword)
                .NotEmpty().WithMessage("Please confirm password.")
                .Equal(u => u.Password).WithMessage("The passwords are different.");
        }
    }
}
