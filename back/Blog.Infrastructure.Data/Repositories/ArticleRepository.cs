using Blog.Domain.CrossCutting;
using Blog.Domain.Repositories;
using Blog.Infrastructure.Data.Interfaces;

namespace Blog.Infrastructure.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Article> GetById(Guid id)
        {
            return await _unitOfWork.GetById<Article>(id);
        }

        public async Task<Article> Insert(Article article)
        {
            return await _unitOfWork.InsertAsync(article);
        }
    }
}
