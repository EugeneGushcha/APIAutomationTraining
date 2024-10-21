using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Automation
{
    public interface IAPIClient
    {
        Task<RestResponse> GetListofUsers(int minAge, int maxAge);
        Task<RestResponse> CreateUser<T>(T payload) where T : class;
        Task<RestResponse> UpdateUser<T>(T payload) where T : class;
        Task<RestResponse> DeleteUser<T>(T payload) where T : class;
        Task<RestResponse> PatchUpdateUser<T>(T payload) where T : class;
        Task<RestResponse> UploadUsers(String file, DataFormat dataFormat);
    }
}
