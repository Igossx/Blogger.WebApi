using FluentValidation;

namespace Blogger.Application.Comment.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(c => c.UserName)
                .NotEmpty().WithMessage("Please enter a username.")
                .MinimumLength(2).WithMessage("Username should have atleast 2 characters.")
                .MaximumLength(25).WithMessage("Username should have maximum of 20 characters.");

            RuleFor(c => c.Message)
                .NotEmpty().WithMessage("Please enter a content.")
                .MaximumLength(2500).WithMessage("Content should have maximum of 2500 caracters.");
        }
    }
}

