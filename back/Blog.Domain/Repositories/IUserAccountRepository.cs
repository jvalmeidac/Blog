using Blog.Domain.CrossCutting.Entities;
using Blog.SharedKernel.Interfaces;

namespace Blog.Domain.Repositories
{
    public interface IUserAccountRepository : IRepository
    {
        Task<UserAccount> GetById(Guid id);
    }
}
