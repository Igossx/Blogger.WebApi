using Blogger.Application.Post.Commands.CreateUser;
using Domain.Interfaces;
using FluentValidation;

namespace Blogger.Application.Post.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        private readonly IPostRepository _postRepository;

        public CreatePostCommandValidator(IPostRepository postRepository)
        {
            _postRepository = postRepository;

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Please enter a title.")
                .MinimumLength(2).WithMessage("Title should have atleast 2 characters.")
                .MaximumLength(25).WithMessage("Title should have maximum of 20 characters.")
                .Must(_postRepository.IsTitleUnique).WithMessage("This title already exists.");

            RuleFor(p => p.Content)
                .NotEmpty().WithMessage("Please enter a content.")
                .MaximumLength(2500).WithMessage("Content should have maximum of 2500 caracters.");

            RuleFor(p => p.Category)
                .NotEmpty().WithMessage("Please enter a category.");
        }
    }
}
