using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using WorkWatch.Services.Models;

namespace WorkWatch.Services
{
    public class EventsService
    {
        private readonly string _apiUrl;

        public EventsService(string apiUrl)
        {
            _apiUrl = apiUrl;
        }
        public async Task<int> GetUserId(string username, string machineName)
        {
            return await _apiUrl.AppendPathSegment($"events/users")
                .PostJsonAsync(new User
                {
                    Username = username,
                    MachineName = machineName
                })
                .ReceiveJson<int>();
        }
    }
}
