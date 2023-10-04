using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Blogger.Application.Account.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterUserCommandHandler(IAccountRepository accountRepository, IPasswordHasher<User> passwordHasher)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // Mapping: RegisterUserDto to User
            var newUser = new User()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                RoleId = request.RoleId
            };

            var hashedPasword = _passwordHasher.HashPassword(newUser, request.Password);

            newUser.PasswordHash = hashedPasword;

            await _accountRepository.Register(newUser);

            return Unit.Value;
        }
    }
}
