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
    public class APIClientRead : IAPIClientRead, IDisposable
    {
        readonly RestClient client;
        const string BASE_URL = "http://localhost:49000";
        public APIClientRead()
        {
            var apiAuth = new APIAuthenticatorRead();
            var options = new RestClientOptions(BASE_URL)
            {
                Authenticator = apiAuth
            };
            client = new RestClient(options);
            client.AddDefaultHeader(KnownHeaders.Authorization, apiAuth.GetT);
            client = new RestClient();
        }
        #region Get Methods
        public RestResponse GetListofUsers()
        {
            var apiAuth = new APIAuthenticatorRead();
            var options = new RestClientOptions(BASE_URL)
            {
                Authenticator = apiAuth
            };
            var client = new RestClient(options);
            var request = new RestRequest(Endpoints.GET_LIST_OF_USERS, Method.Get);
            //request.AddQueryParameter("olderThan", minAge, false);
            //request.AddQueryParameter("sex", "MALE");      //only male as for now
            //request.AddQueryParameter("youngerThan", maxAge, false);
            return client.Execute(request);
        }

        public RestResponse GetAvailableZipCodes()
        {
            var apiAuth = new APIAuthenticatorRead();
            var options = new RestClientOptions(BASE_URL)
            {
                Authenticator = apiAuth
            };
            var client = new RestClient(options);
            var request = new RestRequest(Endpoints.GET_LIST_OF_ZIP_CODES, Method.Get);
            return client.Execute(request);
        }
        #endregion End of Get Methods


        public void Dispose()
        {
            client?.Dispose();
            GC.SuppressFinalize(this);
        }  
    }
}
