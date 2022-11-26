using Blog.Domain.CrossCutting;
using Blog.Domain.Queries.Articles;
using Blog.Domain.Repositories;
using Blog.SharedKernel.DTOs;
using MediatR;

namespace Blog.Application.QueryHandlers.Articles
{
    public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, ValidatedResult<GetArticleQueryResult>>
    {
        private readonly IArticleRepository _articleRepository;

        public GetArticleQueryHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<ValidatedResult<GetArticleQueryResult>> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var result = new ValidatedResult<GetArticleQueryResult>();

            var article = await GetArticle(request);
            if (article is null)
            {
                result.NotFound = true;
                result.ErrorMessages.Add("Article was not found!");
                return result;
            }

            return MapResult(article);
        }

        private async Task<Article> GetArticle(GetArticleQuery request)
        {
            return await _articleRepository.GetById(request.Id);
        }

        private ValidatedResult<GetArticleQueryResult> MapResult(Article article)
        {
            return new ValidatedResult<GetArticleQueryResult>
            {
                Data = new GetArticleQueryResult
                {
                    Content = article.Content,
                    Description = article.Description,
                    Title = article.Title
                }
            };
        }
    }
}
