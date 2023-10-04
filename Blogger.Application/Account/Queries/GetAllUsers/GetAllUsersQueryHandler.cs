using Domain.Interfaces;
using MediatR;

namespace Blogger.Application.Account.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAllUsersQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _accountRepository.GetAll();

            // Mapping: User to UserDto
            var usersDto = users.Select(u => new UserDto
            {
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                Name = u.FirstName + " " + u.LastName,
                DateOfBirth = u.DateOfBirth,
                RoleName = u.Role.Name
            });

            return usersDto;
        }
    }
}
