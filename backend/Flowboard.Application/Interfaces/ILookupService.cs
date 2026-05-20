using Flowboard.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flowboard.Application.Interfaces
{
    public interface ILookupService
    {
        Task<List<LookupItemDto>> GetLookupItemsByNameAsync(
            string name,
            int language
        );
    }
}