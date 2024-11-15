using API.Automation.Auth;
using API.Automation.Models.Request;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Automation
{
    public class APIClientWrite : IAPIClientWrite, IDisposable
    {
        readonly RestClient client;
        const string BASE_URL = "http://localhost:49000";
        public APIClientWrite()
        {
            var apiAuth = new APIAuthenticatorWrite();
            var options = new RestClientOptions(BASE_URL)
            {
                Authenticator = apiAuth
            };
            client = new RestClient(options);
            client.AddDefaultHeader(KnownHeaders.Authorization, apiAuth.GetT);
            client = new RestClient();
        }


        #region Post Methods
        public RestResponse CreateUser<T>(T payload) where T : class
        {
            var apiAuth = new APIAuthenticatorWrite();
            var options = new RestClientOptions(BASE_URL)
            {
                Authenticator = apiAuth
            };
            var client = new RestClient(options);
            var request = new RestRequest(Endpoints.CREATE_USER, Method.Post);
            request.AddBody(payload);
            return client.Execute(request);
        }

        public RestResponse ExpandZipCode<T>(T payload) where T : class
        {
            var apiAuth = new APIAuthenticatorWrite();
            var options = new RestClientOptions(BASE_URL)
            {
                Authenticator = apiAuth
            };
            var client = new RestClient(options);
            var request = new RestRequest(Endpoints.EXPAND_LIST_OF_ZIP_CODES, Method.Post);
            request.AddBody(payload);
            return client.Execute(request);
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
        #endregion End of Post methods

        public void Dispose()
        {
            client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
