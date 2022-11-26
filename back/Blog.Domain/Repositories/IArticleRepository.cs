using Blog.Domain.CrossCutting;
using Blog.SharedKernel.Interfaces;

namespace Blog.Domain.Repositories
{
    public interface IArticleRepository : IRepository
    {
        Task<Article> Insert(Article article);
        Task<Article> GetById(Guid id);
    }
}
