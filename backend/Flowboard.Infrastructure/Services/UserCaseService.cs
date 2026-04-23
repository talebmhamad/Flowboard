using Flowboard.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Flowboard.Infrastructure.Services
{

    public class UserCaseService : IUserCaseService
    {
        public async Task<object> GetMyCases(string userId)
        {
            return new[]
            {
            new { id = 1, process = "Leave Request", status = "Pending", date = DateTime.Now }
            };
        }
    }
}
