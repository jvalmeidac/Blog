using Blog.SharedKernel.DTOs;
using MediatR;

namespace Blog.Domain.Queries.Articles
{
    public record GetArticleQuery : IRequest<ValidatedResult<GetArticleQueryResult>>
    {
        public Guid Id { get; set; }

        public GetArticleQuery(Guid id) => Id = id;
    }

    public record GetArticleQueryResult
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
    }
}
