using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Automation.Models.Response
{
    public  class CreateUserRes
    {
        public string name {  get; set; }
        public int age { get; set; }
        public string sex { get; set; }
        public int code { get; set; }
    }
}
