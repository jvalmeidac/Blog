using Blog.SharedKernel.DTOs;
using MediatR;

namespace Blog.Domain.Commands.Articles
{
    public record CreateAnArticleCommand : IRequest<ValidatedResult<CreateAnArticleCommandResult>>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
    }

    public record CreateAnArticleCommandResult
    {
        public Guid ArticleId { get; set; }
    }
}
