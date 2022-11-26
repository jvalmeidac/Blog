using Blog.Domain.Commands.Articles;
using Blog.Domain.CrossCutting;
using Blog.Domain.Repositories;
using Blog.SharedKernel.DTOs;
using MediatR;

namespace Blog.Application.CommandHandlers.Articles
{
    public class CreateAnArticleCommandHandler : IRequestHandler<CreateAnArticleCommand, ValidatedResult<CreateAnArticleCommandResult>>
    {
        private readonly IArticleRepository _articleRepository;

        public CreateAnArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<ValidatedResult<CreateAnArticleCommandResult>> Handle(CreateAnArticleCommand request, CancellationToken cancellationToken)
        {
            var result = Validate(request);
            if (!result.Success)
            {
                return result;
            }

            var article = await InsertArticle(request);
            if (article is null)
            {
                result.ErrorMessages.Add("An error ocurred!");
                result.BadRequest = true;
                return result;
            }

            result.Data =  new CreateAnArticleCommandResult
            {
                ArticleId = article.Id
            };

            return result;
        }

        private ValidatedResult<CreateAnArticleCommandResult> Validate(CreateAnArticleCommand request)
        {
            var result = new ValidatedResult<CreateAnArticleCommandResult>();

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                result.BadRequest = true;
                result.ErrorMessages.Add("Title is required!");
            }

            if(string.IsNullOrWhiteSpace(request.Description))
            {
                result.BadRequest = true;
                result.ErrorMessages.Add("Description is required!");
            }

            if (string.IsNullOrWhiteSpace(request.Content))
            {
                result.BadRequest = true;
                result.ErrorMessages.Add("Content is required!");
            }

            return result;
        }

        private async Task<Article> InsertArticle(CreateAnArticleCommand request)
        {
            var article = new Article
            {
                Content= request.Content,
                Description= request.Description,
                Title= request.Title
            };

            return await _articleRepository.Insert(article);
        }
    }
}
