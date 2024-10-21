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

        public async Task<RestResponse> GetListofUsers(int minAge, int maxAge)
        {
            var request = new RestRequest(Endpoints.GET_LIST_OF_USERS, Method.Get);
            request.AddQueryParameter("olderThan", minAge);
            request.AddQueryParameter("youngerThan", maxAge);
            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> CreateUser<T>(T payload) where T : class
        {
            var request = new RestRequest(Endpoints.CREATE_USER, Method.Post);
            request.AddBody(payload);
            return await client.ExecuteAsync<T>(request);
        }

        public async Task<RestResponse> UpdateUser<T>(T payload) where T : class
        {
            var request = new RestRequest(Endpoints.UPDATE_USER, Method.Put);
            request.AddBody(payload);
            return await client.ExecuteAsync<T>(request);
        }

        public async Task<RestResponse> DeleteUser<T>(T payload) where T : class
        {
            var request = new RestRequest(Endpoints.DELETE_USER, Method.Delete);
            request.AddBody(payload);
            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> PatchUpdateUser<T>(T payload) where T : class
        {
            var request = new RestRequest(Endpoints.PATCH_UPDATE_USER, Method.Patch);
            request.AddBody(payload);
            return await client.ExecuteAsync<T>(request);
        }

        public async Task<RestResponse> UploadUsers(String file, DataFormat dataFormat)
        {
            var request = new RestRequest(Endpoints.UPLOAD_USERS, Method.Post);
            request.AddStringBody(file, dataFormat);
            return await client.ExecuteAsync(request);
        }

        public void Dispose()
        {
            client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
