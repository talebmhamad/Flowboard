using Flowboard.Intalio.Helpers;

namespace Flowboard.Intalio.Interfaces
{
    public interface IUserExtensionService
    {
        Task<List<UserLookup>> GetEmployeesByManagerIdAsync(int managerId);

    }
}