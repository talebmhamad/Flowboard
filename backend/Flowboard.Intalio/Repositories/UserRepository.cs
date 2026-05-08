using Flowboard.Intalio.Context;
using Flowboard.Intalio.Helpers;

namespace Flowboard.Intalio.Repositories
{
    public class UserRepository
    {
        private readonly IAMContext _db;

        public UserRepository(IAMContext db)
        {
            _db = db;
        }

        public async Task<List<UserLookup>> GetUsersByManagerIdAsync(int managerId)
        {
            var users = _db.Users
                .Where(x => x.ManagerId == managerId)
                .Select(x => new UserLookup
                {
                    Id = x.Id
                })
                .ToList();

            return await Task.FromResult(users);
        }
    }
}