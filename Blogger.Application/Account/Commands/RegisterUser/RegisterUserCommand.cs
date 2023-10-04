using MediatR;

namespace Blogger.Application.Account.Commands.RegisterUser
{
    public class RegisterUserCommand : RegisterUserDto, IRequest
    {
    }
}
