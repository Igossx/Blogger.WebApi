using Domain.Interfaces;
using MediatR;

namespace Blogger.Application.Account.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public DeleteUserCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _accountRepository.GetByEmail(request.Email);

            await _accountRepository.Delete(user);

            return Unit.Value;
        }
    }
}
