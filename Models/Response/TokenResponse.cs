using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Automation.Models.Response
{
    public class TokenResponse
    {
        [JsonPropertyName("token_type")]
        public string? TokenType { get; init; }

        [JsonPropertyName("access_token")]
        public string? AccessToken { get; init; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("scope")]
        public string? Scope { get; set; }

        [JsonPropertyName("refresh_token")]
        public int RefreshToken { get; set; }

        [JsonPropertyName("token_scope")]
        public string? TokenScope { get; set; }
    }
}
