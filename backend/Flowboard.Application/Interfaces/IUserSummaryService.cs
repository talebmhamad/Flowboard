using Flowboard.Application.DTOs;
using System.Threading.Tasks;

namespace Flowboard.Application.Interfaces
{
    public interface IUserSummaryService
    {
        Task<UserSummaryDto> GetSummary();

    }
}