using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Automation
{
    public class Endpoints
    {
        public static string GET_LIST_OF_USERS = "/users";  //?olderThan={min_age}&youngerThan={max_age}
        public static string CREATE_USER = "/users";        //payload
        public static string UPDATE_USER = "/users";        //payload init user and updated user
        public static string DELETE_USER = "/users";        //payload
        public static string PATCH_UPDATE_USER = "/users";  //payload init user and updated user
        public static string UPLOAD_USERS = "/users/upload";//json file
    }
}
