using API.Automation.Models.Response;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace API.Automation.Auth
{
    public sealed class APIAuthenticatorWrite : AuthenticatorBase
    {
        readonly string baseUrl;
        const string BASE_URL = "http://localhost:49000/";
        readonly string clientId = "0oa157tvtugfFXEhU4x7";
        readonly string clientSecret = "X7eBCXqlFC7x-mjxG5H91IRv_Bqe1oq7ZwXNA8aq";

        public RestClientOptions Options { get; }

        private static RestClient? client;
        public string GetT => GetToken().Result; 


        public APIAuthenticatorWrite() : base("")
        {
            Options = new RestClientOptions(BASE_URL)
            {
                Authenticator = new HttpBasicAuthenticator(clientId, clientSecret)
            };
            client = new RestClient(Options);
        }
        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            Token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, Token);
        }

        private async Task<string> GetToken()
        {
            var request = new RestRequest("oauth/token");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("scope", "write");
            var response = await client!.PostAsync<TokenResponse>(request);

            return $"{response!.TokenType} {response!.AccessToken}";
            //var requestRead = new RestRequest("oauth/token").AddParameter("grant_type", "client_credentials").AddParameter("scope", "read", ParameterType.RequestBody);
            //var responseRead = await client.PostAsync<TokenResponse>(requestRead);

            //var requestWrite = new RestRequest("oauth/token").AddParameter("grant_type", "client_credentials").AddParameter("scope", "write", ParameterType.RequestBody );
            //var responseWrite = await client.PostAsync<TokenResponse>(requestWrite);

            //return $"{responseRead.TokenType} {responseRead.AccessToken} {responseRead.Scope} \n {responseWrite.TokenType} {responseWrite.AccessToken} {responseWrite.Scope}";
        }
    }
}
