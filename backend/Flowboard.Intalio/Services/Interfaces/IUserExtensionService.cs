
namespace Flowboard.Intalio.Interfaces
{
    public interface IUserExtensionService
    {
        Task<List<UserLookupDto>> GetEmployeesByManagerIdAsync(int managerId);
    }
}