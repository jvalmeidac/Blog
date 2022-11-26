using Blog.Domain.Repositories;
using Blog.Infrastructure.Security.DTOs;

namespace Blog.Infrastructure.Security
{
    public class AccessManager
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public AccessManager(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public bool ValidateCredentials(User user)
        {
            var isValidCredentials = false;

            if((user?.UserId ?? Guid.Empty) == Guid.Empty)
            {
                return isValidCredentials;
            }

            return true;
        }

        private async Task<User> GetUserAccount(Guid userId)
        {
            var userAccount = await _userAccountRepository.GetById(userId);

            return new User
            {
                Password = userAccount.Password,
                UserId = userId
            };
        }
    }
}
