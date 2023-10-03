using MediatR;

namespace Blogger.Application.Account.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public string Email { get; set; } = default!;
    }
}
