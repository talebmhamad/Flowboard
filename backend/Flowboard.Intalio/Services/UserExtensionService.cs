using Flowboard.Intalio.Helpers;
using Flowboard.Intalio.Interfaces;
using Flowboard.Intalio.Repositories;

namespace Flowboard.Intalio.Services
{
    public class UserExtensionService : IUserExtensionService
    {
        private readonly UserRepository _repository;

        public UserExtensionService(UserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserLookup>> GetEmployeesByManagerIdAsync(int managerId)
        {
            return await _repository.GetUsersByManagerIdAsync(managerId);
        }


    }
}