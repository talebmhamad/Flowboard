using Flowboard.Application.DTOs;
using System.Text.Json;

namespace Flowboard.Application.Interfaces
{
    public interface ILookupService
    {
        Task<List<LookupItemDto>> GetLookupItemsByNameAsync(
            string name,
            int language
        );

        Task<JsonElement> SearchUsersAsync(
    string text,
    bool showOnlyActiveUsers,
    int? language
);
    }
}