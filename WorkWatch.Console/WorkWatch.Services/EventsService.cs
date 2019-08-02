using System;
using System.Net.Http;
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

        public async Task<int> GetApplicationId(int userId, string applicationName)
        {
            return await _apiUrl.AppendPathSegment($"events/applications")
                .PostJsonAsync(new Application
                {
                    UserId = userId,
                    Name = applicationName
                })
                .ReceiveJson<int>();
        }

        public async Task<int> StartInput(int userId, int applicationId)
        {
            return await _apiUrl.AppendPathSegment($"events/inputs")
                .PostJsonAsync(new Input
                {
                    UserId = userId,
                    ApplicationId = applicationId
                })
                .ReceiveJson<int>();
        }

        public async Task UpdateInput(int inputId)
        {
            await _apiUrl.AppendPathSegment($"events/inputs")
                .PatchJsonAsync(inputId);
        }




    }
}
