using System.Threading.Tasks;

namespace Flowboard.Application.Interfaces
{
    public interface IUserCaseService
    {
        Task<object> GetMyCases(string userId);
    }
}
