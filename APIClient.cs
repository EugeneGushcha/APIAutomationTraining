using API.Automation.Auth;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Automation
{
    public class APIClient : IAPIClient, IDisposable
    {
        readonly RestClient client;
        public APIClient(string baseUrl)
        {
            var options = new RestClientOptions(baseUrl)
            {
                Authenticator = new APIAuthenticator()
            };
            client = new RestClient(options);
            
        }

        public async Task<RestResponse> CreateUser<T>(T payload) where T : class
        {
            var request = new RestRequest(Endpoints.CREATE_USER, Method.Post);
            request.AddBody(payload);
            return await client.ExecuteAsync<T>(request);
        }

        public async Task<RestResponse> DeleteUser(string id)
        {
            var request = new RestRequest(Endpoints.DELETE_USER, Method.Delete);
            request.AddUrlSegment(id, id);
            return await client.ExecuteAsync(request);
        }

        public void Dispose()
        {
            client?.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task<RestResponse> GetListofUsers(int minAge, int maxAge)
        {
            throw new NotImplementedException();
        }

        public Task<RestResponse> PatchUpdateUser<T>(T payload, string id) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<RestResponse> UpdateUser<T>(T payload, string id) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<RestResponse> UploadUsers<T>(T payload) where T : class
        {
            throw new NotImplementedException();
        }

        Task<RestResponse> IAPIClient.DeleteUser<T>(string id)
        {
            throw new NotImplementedException();
        }
    }
}
