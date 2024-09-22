using API.Automation.Models.Response;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace API.Automation.Auth
{
    public sealed class APIAuthenticator : AuthenticatorBase
    {
        readonly string baseUrl;
        readonly string clientId;
        readonly string clientSecret;

        public APIAuthenticator() : base("")
        {
            //TBD
        }
        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            var token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, token);
        }

        private async Task<string> GetToken()
        {
            var options = new RestClientOptions(baseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(clientId, clientSecret),
            }; 
            var client = new RestClient(options);
            var requestRead = new RestRequest("oauth/token").AddParameter("grant_type", "client_credentials").AddParameter("scope", "read", ParameterType.RequestBody);
            var responseRead = await client.PostAsync<TokenResponse>(requestRead);

            var requestWrite = new RestRequest("oauth/token").AddParameter("grant_type", "client_credentials").AddParameter("scope", "write", ParameterType.RequestBody );
            var responseWrite = await client.PostAsync<TokenResponse>(requestWrite);

            return $"{responseRead.TokenType} {responseRead.AccessToken} {responseRead.Scope} \n {responseWrite.TokenType} {responseWrite.AccessToken} {responseWrite.Scope}";
        }
    }
}
