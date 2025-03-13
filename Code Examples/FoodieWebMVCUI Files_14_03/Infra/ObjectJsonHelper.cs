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
    }
}
