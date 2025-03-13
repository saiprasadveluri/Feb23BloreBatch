using Azure.Core.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace MMVCDemoApp1.Infra
{
    public class ObjectJsonHelper
    {
        public static string ToJson<T>(T obj) {           
            return JsonSerializer.Serialize<T>(obj);
        }

        public static T GetFromJson<T>(string strInp)
        {
            return JsonSerializer.Deserialize<T>(strInp);
        }

        public static void AddToSession<T>(HttpContext current,string Key,T obj)
        {
            string str = ToJson(obj);
            current.Session.SetString(Key,str);
        }

        public static T GetFromSession<T>(HttpContext current, string Key)
        {
            string str=current.Session.GetString(Key);
            if (str != null)
                return GetFromJson<T>(str);
            else
                return default(T);
        }

        public static void RemoveSessionItem(HttpContext current, string Key)
        {
            current.Session.Remove(Key);
        }
    }
}
