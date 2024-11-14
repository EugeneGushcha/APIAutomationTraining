using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Collections;
using System.Xml;

namespace API.Automation.Utility
{
    public class HandleContent
    {
        public static T GetContent<T>(RestResponse response)
        {
            var content = response.Content;
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static T ParseJson<T>(string file)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
        }

        public static string GetFilePath(string name) 
        {
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
            path = string.Format(path + "TestData\\{0}", name);
            return path;
        }

        public static bool DuplicateInArray(string[] array, string value)
        {
            int i = 0;
            foreach (string item in array)
            {
                if (item.Equals(value))
                {
                    i++;
                    if (i==2)
                    return true;
                }
            }
            return false;
        }

        public static bool DuplicateInArray(string[] array)
        {
            bool hasDuplicates = array.GroupBy(x => x).Any(g => g.Count() > 1);
            return hasDuplicates;
        }
    }
}
