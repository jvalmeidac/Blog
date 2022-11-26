using Blog.Domain.CrossCutting.Entities;
using Blog.Domain.Repositories;
using Blog.Infrastructure.Data.Interfaces;

namespace Blog.Infrastructure.Data.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAccountRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserAccount> GetById(Guid id)
        {
            return await _unitOfWork.GetById<UserAccount>(id);
        }
    }
}
