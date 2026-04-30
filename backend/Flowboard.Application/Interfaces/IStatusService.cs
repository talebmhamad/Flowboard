using Flowboard.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flowboard.Application.Interfaces
{
    public interface IStatusService
    {
        Task<List<StatusDto>> GetAllAsync();
    }
}
