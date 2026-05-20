using Flowboard.Intalio.Context;

namespace Flowboard.Intalio.Repositories
{
    public class UserRepository
    {
        private readonly IAMContext _db;

        public UserRepository(IAMContext db)
        {
            _db = db;
        }

        public async Task<List<UserLookupDto>> GetUsersByManagerIdAsync(int managerId)
        {
            var users = _db.Users
                .Where(x => x.ManagerId == managerId)
                .Select(x => new UserLookupDto
                {
                    Id = x.Id,
                    Name = x.FirstName + " " + x.LastName,
                })
                .ToList();

            return await Task.FromResult(users);
        }
    }
}