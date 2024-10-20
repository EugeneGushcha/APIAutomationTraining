using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Automation.Models.Response
{
    public class TokenResponse
    {
        public string TokenType { get; set; }  //Bearer token
        public string AccessToken { get; set; } //Alpha numeric value
        public string Scope { get; set; }
    }
}
